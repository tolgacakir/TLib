using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Device.Plc.Exceptions;
using TLib.Util.FileManagement;

namespace TLib.Device.Plc.Concrete.Json
{
    public class JsonPlcClient<TRead, TWrite> : IPlcClient
        where TRead : new()
        where TWrite : new()
    {
        public bool Connected { get; private set; }

        public string Name { get; }

        private readonly string _readFilePath;
        private readonly string _writeFilePath;


        public JsonPlcClient(string name="1")
        {
            Name = name;

            string folder = "JsonPlc";
            _readFilePath = $"{folder}\\{name}_Plc_To_Pc.json";
            _writeFilePath = $"{folder}\\{name}_Pc_To_Plc.json";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists(_readFilePath) && !File.Exists(_writeFilePath))
            {
                WriteDefault();
            }
        }

        public bool Connect()
        {
            Connected = true;
            return true;
        }

        public void Disconnect()
        {
            Connected = false;
        }

        protected void WriteDefault()
        {
            var tRead = new TRead();
            var jsonRead = JsonConvert.SerializeObject(tRead, Formatting.Indented);
            File.WriteAllText(_readFilePath, jsonRead);

            var tWrite = new TWrite();
            var jsonWrite = JsonConvert.SerializeObject(tWrite, Formatting.Indented);
            File.WriteAllText(_writeFilePath, jsonWrite);
        }

        protected bool Get(out TRead data)
        {
            if (Connected)
            {
                string json = File.ReadAllText(_readFilePath);
                data = JsonConvert.DeserializeObject<TRead>(json);
                return true;
            }
            else
            {
                throw new DeviceNoConnectionException($"JsonPlc has not connection. Id: {Name}");
            }
        }

        protected bool Set(in TWrite data)
        {
            if (Connected)
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(_writeFilePath, json);
                return true;
            }
            else
            {
                throw new DeviceNoConnectionException($"JsonPlc has not connection. Id: {Name}");
            }
        }
    }
}
