using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TLib.Device.BarcodeScanner.Abstract;
using TLib.Device.Plc.Exceptions;

namespace TLib.Device.BarcodeScanner.Concrete.SckBarcodeScanner
{
    public class SckBarcodeScanner : IBarcodeScanner
    {
        public string Name { get; }
        public bool Connected { get; private set; }

        public string IpAddress { get; }
        public int Port { get; }


        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private readonly byte[] _heartbeatRequest;
        private Timer _readTimer;
        private Timer _writeTimer;
        private readonly AutoConnector _autoConnector;
        private readonly ILogger _logger;

        public event BarcodeScannedEventHandler BarcodeScanned;
        public event BarcodeCouldNotScannedEventHandler BarcodeCouldNotScanned;

        private const string HEARTBEAT_REQUEST = "sMN Run";
        private const string HEARTBEAT_RESPONSE = "sAN Run 1";
        private const string NOREAD_RESPONSE = "NoRead";
        private const char STX = (char)0x2;
        private const char ETX = (char)0x3;


        public SckBarcodeScanner(string name, string ipAddress, int port, ISynchronizeInvoke syncInvoke=null, ILogger logger = null, double interval = 500, bool autoReconnect = true)
        {
            Name = name;
            IpAddress = ipAddress;
            Port = port;
            _logger = logger;
            if (autoReconnect)
            {
                _autoConnector = new AutoConnector(this, 10000);
            }

            BuildTimers(syncInvoke,interval);
            _heartbeatRequest = BuildHeartbeatRequest();
        }

        public bool Connect()
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(new IPEndPoint(IPAddress.Parse(IpAddress), Port));
                _stream = _tcpClient.GetStream();
                _stream.ReadTimeout = 30;
                _stream.WriteTimeout = 30;
                _tcpClient.ReceiveTimeout = 30;
                _tcpClient.SendTimeout = 30;

                if (_tcpClient.Connected)
                {
                    Connected = true;
                    _writeTimer.Start();
                    _readTimer.Start();
                    _logger?.Information($"Barcode scanner connection successfully. Ip: {IpAddress}");
                    return true;
                }
                else
                {
                    Disconnect();
                    return false;
                }
            }
            catch
            {
                Disconnect();
                return false;
            }
        }

        public void Disconnect()
        {
            _logger?.Information($"Barcode Scannner connection lost. Ip: {IpAddress}");
            Connected = false;
            _readTimer.Stop();
            _writeTimer.Stop();
            _stream?.Close();
            _stream?.Dispose();
            _tcpClient?.Close();
            _tcpClient?.Dispose();
        }

        private void BuildTimers(ISynchronizeInvoke syncInvoke, double interval)
        {
            _readTimer = new Timer(interval);
            _readTimer.SynchronizingObject = syncInvoke;
            _readTimer.Elapsed += _readTimer_Elapsed;
            _writeTimer = new Timer(interval * 2);
            _writeTimer.SynchronizingObject = syncInvoke;
            _writeTimer.Elapsed += _writeTimer_Elapsed;
        }
        private byte[] BuildHeartbeatRequest()
        {
            List<byte> sendMsg = Encoding.UTF8.GetBytes(HEARTBEAT_REQUEST).ToList();
            sendMsg.Insert(0, (byte)STX);
            sendMsg.Add((byte)ETX);
            return sendMsg.ToArray();
        }

        private async void _writeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _writeTimer.Stop();
            try
            {
                await WriteAsync();
                _writeTimer.Start();
            }
            catch
            {
                Disconnect();
            }
        }

        private async Task WriteAsync()
        {
            if (Connected)
            {
                await _stream.WriteAsync(_heartbeatRequest, 0, _heartbeatRequest.Length);
            }
            else
            {
                throw new DeviceNoConnectionException();
            }
        }

        private async void _readTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _readTimer.Stop();
            
            try
            {
                string data = await ReadAsync();

                var datas = data.Split(new char[] {STX, ETX}, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var d in datas)
                {
                    if (d.Contains(HEARTBEAT_RESPONSE))
                    {
                        //Heartbeat
                    }
                    else if (d.Contains(NOREAD_RESPONSE))
                    {
                        _logger.Verbose($"Barcode could not scan. Ip: {IpAddress}");
                        BarcodeCouldNotScanned?.Invoke(this, null);
                    }
                    else
                    {
                        string barcode = d.Replace(NOREAD_RESPONSE, "")
                            .Replace("!", "").Replace(HEARTBEAT_RESPONSE, "")
                            .Replace((STX).ToString(), "")
                            .Replace((ETX).ToString(), "");
                        _logger.Verbose($"Barcode scanned. Barcode: {barcode}, Ip: {IpAddress}");
                        BarcodeScanned?.Invoke(this, new BarcodeEventArgs(barcode, Name));
                    }
                }
                
                _readTimer.Start();
            }
            catch
            {
                Disconnect();
            }
        }

        private async Task<string> ReadAsync()
        {
            if (Connected)
            {
                var data = new byte[256];
                var bytes = await _stream.ReadAsync(data, 0, data.Length);
                var readed = Encoding.ASCII.GetString(data, 0, bytes);
                return readed;
            }
            else
            {
                throw new DeviceNoConnectionException();
            }
        }
    }
}
