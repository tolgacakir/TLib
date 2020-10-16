using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Util.FileManagement;

namespace TLib.Device.Plc.Concrete.Json
{
    public class JsonPlcDal<Tout,Tin> : IPlcDal<Tout,Tin>
        where Tout : class, IPlcData, new()
        where Tin : class, IPlcData, new()
    {
        public bool ConnectionStatus { get; private set; }
        private readonly string _readFilePath;
        private readonly string _writeFilePath;

        public JsonPlcDal(bool connectionStatus = false, string readFilePath= "R_PlcData.json", string writeFilePath = "W_PlcData.json")
        {
            ConnectionStatus = connectionStatus;
            _readFilePath = readFilePath;
            _writeFilePath = writeFilePath;
        }

        public bool Connect()
        {
            //JsonFileManager.WriteJson(_readFilePath, new TRead());
            ConnectionStatus = true;
            return ConnectionStatus;
        }

        public void Disconnect()
        {
            ConnectionStatus = false;
        }

        public bool Read(ref Tout readData)
        {
            if (ConnectionStatus)
            {
                readData = JsonFileManager.ReadJson<Tout>(_readFilePath);
                return true;
            }
            else
            {
                throw new Exception("No connection!");
            }
        }

        public bool Write(Tin writeData)
        {
            if (ConnectionStatus)
            {
                JsonFileManager.WriteJson(_writeFilePath, writeData);
                return true;
            }
            else
            {
                throw new Exception("No connection!");
            }
        }
    }
}
