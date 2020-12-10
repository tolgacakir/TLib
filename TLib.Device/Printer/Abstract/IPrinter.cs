using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Printer.Abstract
{
    public interface IPrinter
    {
        string Name { get; }
        bool Print(string data);
    }
}
