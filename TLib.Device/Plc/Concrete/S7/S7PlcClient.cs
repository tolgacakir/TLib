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
        public bool Connected { get; private set; } = false;

        public string Name { get; }

        protected const int NO_ERROR = 0;
        protected S7Client _s7Client;
        protected readonly PlcAutoConnector _plcReconnector;

        //TODO: logger kullanılabilir
        protected S7PlcClient(string ipAddress, int rack = 0, int slot = 1, bool autoReconnect = false, string name = "")
        {
            IpAddress = ipAddress;
            Rack = rack;
            Slot = slot;
            Name = name;

            

            if (autoReconnect)
            {
                _plcReconnector = new PlcAutoConnector(this);
            }
        }

        //protected S7PlcClient(S7Client s7Client, bool autoReconnect = true)
        //{
        //    //TODO: The ip address, rack and slot are missing when the object creating from here.
        //    _s7Client = s7Client;
        //    if (autoReconnect)
        //    {
        //        _plcReconnector = new PlcAutoConnector(this);
        //    }
        //}

        public bool Connect()
        {
            try
            {
                _s7Client = new S7Client(Name);
                _s7Client.ConnTimeout = 1000;
                _s7Client.RecvTimeout = 1000;
                _s7Client.SendTimeout = 1000;

                if (_s7Client.ConnectTo(IpAddress, Rack, Slot) == S7Consts.ResultOK)
                {
                    Connected = true;
                    return true;
                }
                else
                {
                    Disconnect();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Disconnect();
                return false;
            }
            
        }

        public void Disconnect()
        {
            Connected = false;
            _s7Client?.Disconnect();
            _s7Client = null;
        }

        protected bool Get(int dbNumber, int start, ref byte[] buffer)
        {
            if (Connected)
            {
                var result = _s7Client.DBRead(dbNumber, start, buffer.Length, buffer);
                if (result == NO_ERROR)
                {
                    return true;
                }
                else if (result == S7Consts.errTCPNotConnected)
                {
                    //TODO: no connection hatası. bu hatadan sonra tekrar bağlanma mekanizması çalıştırılmalı.
                    Disconnect();
                    throw new NotImplementedException(_s7Client?.ErrorText(result));
                }
                else
                {
                    Disconnect();
                    //TODO: burada okuma hatası.
                    throw new NotImplementedException(_s7Client?.ErrorText(result));
                }
            }
            else
            {
                Disconnect();
                throw new DeviceNoConnectionException($"Data reading is not completed. S7 Plc has not connection. Ip: {IpAddress}");
            }

        }

        protected bool Set(int dbNumber, int start, in byte[] buffer)
        {
            if (Connected)
            {
                var result = _s7Client.DBWrite(dbNumber, start, buffer.Length, buffer);
                if (result == NO_ERROR)
                {
                    return true;
                }
                else if (result == S7Consts.errTCPNotConnected)
                {
                    //TODO: no connection hatası. bu hatadan sonra tekrar bağlanma mekanizması çalıştırılmalı.
                    Disconnect();
                    throw new NotImplementedException(_s7Client?.ErrorText(result));
                }
                else
                {
                    Disconnect();
                    //TODO: burada okuma hatası.
                    throw new NotImplementedException(_s7Client?.ErrorText(result));
                }
            }
            else
            {
                Disconnect();
                throw new DeviceNoConnectionException($"Data writing is not completed. S7 Plc has not connection. Ip: {IpAddress}");
            }

        }

        protected S7Client GetS7PlcClient()
        {
            return _s7Client;
        }

        protected bool GetConnectionStatus()
        {
            int status = -1;
            var result = _s7Client.PlcGetStatus(ref status);
            //return result == S7Consts.ResultOK && status == S7Consts.S7CpuStatusRun;
            return result == 0 && status == S7Consts.S7CpuStatusRun;
        }
    }
}
