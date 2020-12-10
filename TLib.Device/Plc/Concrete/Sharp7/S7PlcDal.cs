using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;

namespace TLib.Device.Plc.Concrete.Sharp7
{
    public abstract class S7PlcDal : IPlcDal
    {
        public string IpAddress { get; }
        public int Rack { get; }
        public int Slot { get; }
        public bool ConnectionStatus => _s7Client.Connected;

        protected const int NO_ERROR = 0;
        protected readonly S7Client _s7Client;
        protected S7PlcDal(string ipAddress, int rack, int slot)
        {
            IpAddress = ipAddress;
            Rack = rack;
            Slot = slot;

            _s7Client = new S7Client();
            _s7Client.ConnTimeout = 1000;
            _s7Client.RecvTimeout = 500;
            _s7Client.SendTimeout = 500;
        }

        public bool Connect()
        {
            return _s7Client.Connect() == NO_ERROR;
        }

        public void Disconnect()
        {
            _s7Client.Disconnect();
        }

        protected bool Get(int dbNumber, int start, ref byte[] buffer)
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

        protected bool Set(int dbNumber, int start, in byte[] buffer)
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
    }
}
