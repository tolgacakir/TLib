using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.ManualTest.Entities;

namespace TLib.ManualTest.DataAccessLayer.Abstract
{
    public interface IDummyPlcClient : IPlcClient
    {
        bool GetPlcRequest(out PlcRequestDataBlock plcRequest);
        bool SetPcResponse(in PcResponseDataBlock pcResponse);
    }
}
