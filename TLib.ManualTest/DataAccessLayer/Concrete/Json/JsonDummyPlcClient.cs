using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Concrete.Json;
using TLib.ManualTest.DataAccessLayer.Abstract;
using TLib.ManualTest.Entities;

namespace TLib.ManualTest.DataAccessLayer.Concrete.Json
{
    public class JsonDummyPlcClient : JsonPlcClient<PlcRequestDataBlock,PcResponseDataBlock>, IDummyPlcClient
    {
        public JsonDummyPlcClient(int id = 1) : base(id)
        {

        }

        public bool GetPlcRequest(out PlcRequestDataBlock plcRequest)
        {
            return Get(out plcRequest);
        }

        public bool SetPcResponse(in PcResponseDataBlock pcResponse)
        {
            return Set(in pcResponse);
        }
    }
}
