using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Concrete.S7;
using TLib.ManualTest.DataAccessLayer.Abstract;
using TLib.ManualTest.Entities;

namespace TLib.ManualTest.DataAccessLayer.Concrete.S7
{
    public class S7DummyPlcClient : S7PlcClient, IDummyPlcClient
    {
        public int DbNumber { get; }
        public S7DummyPlcClient(int dbNumber, string ipAddress, int rack = 0, int slot = 1, bool autoReconnect = true) : 
            base(ipAddress, rack, slot, autoReconnect)
        {
            DbNumber = dbNumber;
        }

        //This ctor is for working with single tcp connection multiple S7PlcClient, 
        //public S7DummyPlcClient(Sharp7.S7Client s7Client, bool autoReconnect = true) 
        //    : base(s7Client,autoReconnect)
        //{

        //}

        public bool GetPlcRequest(out PlcRequestDataBlock plcRequest)
        {
            int startingAddress = 0;
            var buffer = new byte[20];
            var result = Get(DbNumber, startingAddress, ref buffer);

            //TODO: mapping...
            //plcRequest = new PlcRequestDataBlock
            //{
            //    PlcRun = 
            //}
            throw new NotImplementedException();
        }

        public bool SetPcResponse(in PcResponseDataBlock pcResponse)
        {
            int startingAddress = 30;
            var buffer = new byte[10];
            //TODO: mapping...
            var result = Set(DbNumber, startingAddress, buffer);
            throw new NotImplementedException();
        }
    }
}
