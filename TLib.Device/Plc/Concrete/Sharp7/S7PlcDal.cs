using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;

namespace TLib.Device.Plc.Concrete.Sharp7
{
    public class S7PlcDal<Tout,Tin> : IPlcDal<Tout,Tin>
        where Tout : class, IPlcData, new()
        where Tin : class, IPlcData, new()
    {
        private readonly S7DataMapper<Tout,Tin> _dataMapper;
        private readonly string _ipAddress;
        private readonly int _rack;
        private readonly int _slot;
        private readonly S7Client _s7Client;
        private const int NO_ERROR = 0;


        public S7PlcDal(S7DataMapper<Tout,Tin> dataMapper, string ipAddress, int rack, int slot)
        {
            _s7Client = new S7Client();
            _dataMapper = dataMapper;
            _ipAddress = ipAddress;
            _rack = rack;
            _slot = slot;
        }

        public S7PlcDal(S7Client s7Client, S7DataMapper<Tout,Tin> dataMapper, string ipAddress, int rack, int slot)
        {
            _s7Client = s7Client;
            _dataMapper = dataMapper;
            _ipAddress = ipAddress;
            _rack = rack;
            _slot = slot;
        }


        public bool ConnectionStatus
        {
            get
            {
                int status=-1;
                _s7Client.PlcGetStatus(ref status);
                return status == 8;
            }       
        }

        public bool Connect()
        {
            //TODO: bağlı değilse bağlanmalı...
            return _s7Client.ConnectTo(_ipAddress, _rack, _slot) == NO_ERROR;
        }

        public void Disconnect()
        {
            _s7Client.Disconnect();
        }

        public bool Read(ref Tout readData)
        {
            var properties = readData.GetType().GetProperties();
            byte[] buffer = new byte[_dataMapper.ReadTotalByteSize];
            var result = _s7Client.DBRead(_dataMapper.DbNumber, _dataMapper.ReadStartingAddress, buffer.Length, buffer);
            if (result == NO_ERROR)
            {
                _dataMapper.GetDataFromBuffer(in buffer, ref readData);
                return true;
            }
            else
            {
                throw new Exception("read error!");
            }
        }

        public bool Write(Tin writeData)
        {
            //TODO: eksik
            throw new NotImplementedException();
        }
    }
}
