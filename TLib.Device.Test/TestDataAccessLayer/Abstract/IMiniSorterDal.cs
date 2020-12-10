using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Device.Test.TestEntities;

namespace TLib.Device.Test.TestDataAccessLayer.Abstract
{
    public interface IMiniSorterDal
    {
        bool GetPlcRequests(ref PlcRequestDB requestDb);
        bool SetPcResponse(in PcResponse response);
    }
}
