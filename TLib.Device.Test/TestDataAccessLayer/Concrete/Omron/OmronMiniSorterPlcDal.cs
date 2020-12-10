using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Device.Test.TestDataAccessLayer.Abstract;
using TLib.Device.Test.TestEntities;

namespace TLib.Device.Test.TestDataAccessLayer.Concrete.Omron
{
    public class OmronMiniSorterPlcDal : IMiniSorterDal, IPlcDal
    {
        public bool ConnectionStatus => throw new NotImplementedException();

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool GetPlcRequests(ref PlcRequestDB requestDb)
        {
            throw new NotImplementedException();
        }

        public bool SetPcResponse(in PcResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
