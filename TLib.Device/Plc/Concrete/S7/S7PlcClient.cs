using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Device.Plc.Concrete.AutoConnector;
using TLib.Device.Plc.Exceptions;

namespace TLib.Device.Plc.Concrete.S7
{
    public abstract class S7PlcClient : IPlcClient
    {
        public string IpAddress { get; }
        public int Rack { get; }
        public int Slot { get; }
        public bool ConnectionStatus => _s7Client.Connected;

        protected const int NO_ERROR = 0;
        protected readonly S7Client _s7Client;
        protected readonly PlcAutoConnector _plcReconnector;
        protected S7PlcClient(string ipAddress, int rack = 0, int slot = 1, bool autoReconnect = true)
        {
            IpAddress = ipAddress;
            Rack = rack;
            Slot = slot;

            _s7Client = new S7Client
            {
                ConnTimeout = 1000,
                RecvTimeout = 500,
                SendTimeout = 500
            };

            if (autoReconnect)
            {
                _plcReconnector = new PlcAutoConnector(this);
            }
        }

        protected S7PlcClient(S7Client s7Client, bool autoReconnect = true)
        {
            //TODO: The ip address, rack and slot are missing when the object creating from here.
            _s7Client = s7Client;
            if (autoReconnect)
            {
                _plcReconnector = new PlcAutoConnector(this);
            }
        }

        public bool Connect()
        {
            return !_s7Client.Connected
                || _s7Client.ConnectTo(IpAddress, Rack, Slot) == NO_ERROR;
        }

        public void Disconnect()
        {
            _s7Client.Disconnect();
        }

        protected bool Get(int dbNumber, int start, ref byte[] buffer)
        {
            if (ConnectionStatus)
            {
                var result = _s7Client.DBRead(dbNumber, start, buffer.Length, buffer);
                if (result == NO_ERROR)
                {
                    return true;
                }
                else if (result == S7Consts.errTCPNotConnected)
                {
                    //TODO: no connection hatası. bu hatadan sonra tekrar bağlanma mekanizması çalıştırılmalı.
                    throw new NotImplementedException();
                }
                else
                {
                    //TODO: burada okuma hatası.
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new DeviceNoConnectionException($"Data reading is not completed. S7 Plc has not connection. Ip: {IpAddress}");
            }

        }

        protected bool Set(int dbNumber, int start, in byte[] buffer)
        {
            if (ConnectionStatus)
            {
                var result = _s7Client.DBWrite(dbNumber, start, buffer.Length, buffer);
                if (result == NO_ERROR)
                {
                    return true;
                }
                else if (result == S7Consts.errTCPNotConnected)
                {
                    //TODO: no connection hatası. bu hatadan sonra tekrar bağlanma mekanizması çalıştırılmalı.
                    throw new NotImplementedException();
                }
                else
                {
                    //TODO: burada okuma hatası.
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new DeviceNoConnectionException($"Data writing is not completed. S7 Plc has not connection. Ip: {IpAddress}");
            }

        }

        protected S7Client GetS7PlcClient()
        {
            return _s7Client;
        }
    }
}
