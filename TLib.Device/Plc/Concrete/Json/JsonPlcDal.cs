using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Util.FileManagement;

namespace TLib.Device.Plc.Concrete.Json
{
    public class JsonPlcDal<Tin,Tout> : IPlcDal
        where Tin: class, new()
        where Tout : class, new()
    {
        public bool ConnectionStatus { get; private set; }
        private readonly int _uniqueId;
        private readonly string _readFilePath;
        private readonly string _writeFilePath;


        //TODO: 
        public JsonPlcDal(int uniqueId)
        {
            _uniqueId = uniqueId;
            _readFilePath = $"JsonPlc\\{uniqueId}_Plc_To_Pc.json";
            _writeFilePath = $"JsonPlc\\{uniqueId}_Pc_To_Plc.json";

            if (!Directory.Exists("JsonPlc"))
            {
                Directory.CreateDirectory("JsonPlc");
            }
        }

        public bool Connect()
        {
            if (ConnectionStatus)
            {
                throw new Exception("The JsonPlc client is already connected.");
            }
            else
            {
                ConnectionStatus = true;
                return ConnectionStatus;
            }
        }

        public void Disconnect()
        {
            if (ConnectionStatus)
            {
                ConnectionStatus = false;
            }
            else
            {
                throw new Exception("The JsonPlc client is already disconnected.");
            }
        }

        //TODO: readData'yı mapleyerek vermeli, new()'leyerek değil
        public bool Read(ref Tout readData)
        {
            if (ConnectionStatus)
            {
                if (!FileManager.Exists(_readFilePath))
                {
                    JsonFileManager.WriteJson(_readFilePath, new Tout());
                }
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
