using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Util.FileManagement
{
    public class FileManager
    {
        public static void Write(string path, string value)
        {
            File.AppendAllText(path, value);
        }

        public static string Read(string path)
        {
            return File.ReadAllText(path);
        }

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
