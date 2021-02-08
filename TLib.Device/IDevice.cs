using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device
{
    public interface IDevice
    {
        string Name { get; }
        bool Connected { get; }
        bool Connect();
        void Disconnect();
    }
}
