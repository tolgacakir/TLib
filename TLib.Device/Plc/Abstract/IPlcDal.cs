using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Plc.Abstract
{

    //TODO: bunun bir concrete'sini oluşturmak için, factory gerekebilir.
    public interface IPlcDal<Tout,Tin>
        where Tout: class,  IPlcData, new() //plc'den okunan data
        where Tin : class, IPlcData, new()  //plc'ye yazılan data
    {
        bool Connect();
        void Disconnect();
        bool Read(ref Tout readData);
        bool Write(Tin writeData);
        bool ConnectionStatus { get; }
    }
}
