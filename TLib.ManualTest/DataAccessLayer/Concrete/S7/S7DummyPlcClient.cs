using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Concrete.S7;
using TLib.ManualTest.DataAccessLayer.Abstract;
using TLib.ManualTest.Entities;
using Sharp7;

namespace TLib.ManualTest.DataAccessLayer.Concrete.S7
{
    public class S7DummyPlcClient : S7PlcClient, IDummyPlcClient
    {
        private readonly int _startForPlcRequest = 0;
        private readonly int _startForPcResponse = 44;

        public int DbNumber { get; }
        
        //TODO: klonlanabilir olmalı. bu sayede dbNumber değişecek ancak s7Client aynı kalacak... 
        //belki de S7PlcClient klonlanabilir özellikleri içermeli, bu class devralmalı
        public S7DummyPlcClient(int dbNumber, string ipAddress, bool autoReconnect = false, int rack = 0, int slot = 1,string name ="") : 
            base(ipAddress, rack, slot, autoReconnect,name)
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
            var buffer = new byte[44];
            var result = Get(DbNumber, _startForPlcRequest, ref buffer);

            plcRequest = new PlcRequestDataBlock
            {
                PlcRun = buffer.GetByteAt(0),
                PlcRequests = new PlcRequest[3],
            };
            for (int i = 0; i < 3; i++)
            {
                plcRequest.PlcRequests[i] = new PlcRequest
                {
                    Order = buffer.GetIntAt(2 + i * 14),
                    BoxId = buffer.GetByteAt(4 + i * 14),
                    Barcode = buffer.GetCharsAt(6 + i * 14,10),
                };
            }

            return result;
        }

        public bool SetPcResponse(in PcResponseDataBlock pcResponse)
        {
            var buffer = new byte[15];

            buffer.SetIntAt(0, pcResponse.Order);
            buffer.SetByteAt(2, pcResponse.BoxId);
            buffer.SetCharsAt(4, pcResponse.Barcode);
            buffer.SetByteAt(14, pcResponse.Response);

            var result = Set(DbNumber, _startForPcResponse, buffer);
            return result;
        }
    }
}
