using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Device.Test.TestDataAccessLayer.Abstract;
using TLib.Device.Test.TestEntities;
using Sharp7;
using TLib.Device.Plc.Concrete.Sharp7;

namespace TLib.Device.Test.TestDataAccessLayer.Concrete.Sharp7
{
    public class S7MiniSorterDal : S7PlcDal, IMiniSorterDal
    {
        public S7MiniSorterDal(string ipAddress, int rack=0, int slot=1) : base(ipAddress, rack, slot)
        {
        }

        public bool GetPlcRequests(ref PlcRequestDB requestDb)
        {
            //TODO: refactor
            byte[] buffer = new byte[66];
            int dbNumber = 5; //TODO: dışarıdan verilebilir!
            int start = 0;
            var result = Get(dbNumber, start, ref buffer);
            if (result)
            {
                requestDb.PlcRun = buffer.GetByteAt(0);
                for (int i = 0; i < 3; i++)
                {
                    int offset = (30 * i) + (2 * (i + 1));
                    requestDb.PlcRequests[i] = new PlcRequest
                    {
                        OrderId = buffer.GetByteAt(offset),
                        CellId = buffer.GetByteAt(offset + 1),
                        Barcode = buffer.GetCharsAt(offset + 2, 30)
                    };
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetPcResponse(in PcResponse response)
        {
            //TODO: refactor
            byte[] buffer = new byte[10];
            int dbNumber = 5; //TODO: dışarıdan verilebilir!
            int start = 66;
            
            buffer.SetByteAt(0, response.OrderId);
            buffer.SetByteAt(1, response.CellId);
            buffer.SetByteAt(2, response.Response);
            buffer.SetCharsAt(3, response.Barcode);

            var result = Set(dbNumber, start, buffer);
            return result;
        }
    }
}
