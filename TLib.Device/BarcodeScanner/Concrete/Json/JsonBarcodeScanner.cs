using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TLib.Device.BarcodeScanner.Abstract;
using TLib.Device.Plc.Exceptions;

namespace TLib.Device.BarcodeScanner.Concrete.Json
{
    public class JsonBarcodeScanner : IBarcodeScanner
    {
        public int Id { get; }
        public bool ConnectionStatus { get; private set; }

        private string _barcode;
        public string Barcode 
        {
            get => _barcode;

            private set
            {
                if (!string.IsNullOrEmpty(value) && _barcode != value && value != _couldNotScannedCode)
                {
                    _barcode = value;
                    BarcodeScanned?.Invoke(this, new BarcodeEventArgs(value, Id.ToString()));
                }
                else if (_barcode != value && value == _couldNotScannedCode)
                {
                    _barcode = "";
                    WriteToFile(_barcode);
                    BarcodeCouldNotScanned?.Invoke(this, new BarcodeEventArgs(value, Id.ToString()));
                }
                else
                {
                    //TODO: invalid barcode scanned case. belki fluentValidation kullanılabilir.
                }
            }
        }

        private readonly string _readFilePath;
        private readonly Timer _timer;
        private readonly string _couldNotScannedCode;

        public event BarcodeScannedEventHandler BarcodeScanned;
        public event BarcodeCouldNotScannedEventHandler BarcodeCouldNotScanned;

        public JsonBarcodeScanner(int id = 1, int interval = 500, string couldNotScannedCode="ReadError")
        {
            Id = id;
            _couldNotScannedCode = couldNotScannedCode;
            _timer = new Timer(interval);
            _timer.Elapsed += _timer_Elapsed;

            var folder = "JsonBarcodeScanner";
            _readFilePath = $"{folder}\\{id}_BarcodeScanner.txt";

            
            CreateRoot(folder);
        }

        private void CreateRoot(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists(_readFilePath))
            {
                WriteToFile("");
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            Barcode = ScanBarcode();
            _timer.Start();
        }

        private string ScanBarcode()
        {
            if (ConnectionStatus)
            {
                return File.ReadAllText(_readFilePath);
            }
            else
            {
                throw new DeviceNoConnectionException($"The scanner connection is NOT OK. Scanner Id: {Id}");
            }
        }

        private void WriteToFile(string barcode)
        {
            File.WriteAllText(_readFilePath, barcode);
        }

        public bool Connect()
        {
            ConnectionStatus = true;
            _timer.Start();
            return true;
        }

        public void Disconnect()
        {
            _timer.Stop();
            ConnectionStatus = false;
        }

    }
}