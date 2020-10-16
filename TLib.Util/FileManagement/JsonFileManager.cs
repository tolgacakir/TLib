using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Util.FileManagement
{
    public class JsonFileManager
    {
        public static void WriteJson(string path, object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            FileManager.Write(path, json);
        }

        public static T ReadJson<T>(string path)
        {
            var json = FileManager.Read(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
