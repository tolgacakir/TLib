using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Plc.Abstract
{
    public interface IPlcDal
    {
        bool Connect();
        void Disconnect();
        bool ConnectionStatus { get; }
    }
}
