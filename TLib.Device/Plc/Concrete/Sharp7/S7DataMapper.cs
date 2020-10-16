using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;

namespace TLib.Device.Plc.Concrete.Sharp7
{
    public class S7DataMapper<Tout,Tin>
        where Tout : class, IPlcData, new()
        where Tin : class, IPlcData, new()
    {
        public int DbNumber { get; }
        public int ReadTotalByteSize => _readDataMaps.Sum(d => d.ByteSize);
        public int WriteTotalByteSize => _writeDataMaps.Sum(d => d.ByteSize);
        public int ReadStartingAddress { get; }
        public int WrtiteStartingAddress { get; }

        private readonly List<DataMap> _readDataMaps;
        private readonly List<DataMap> _writeDataMaps;


        public S7DataMapper(int dbNumber, List<DataMap> readDataMaps, List<DataMap> writeDataMaps, int readStartingAddress, int wrtiteStartingAddress)
        {
            DbNumber = dbNumber;
            _readDataMaps = readDataMaps;
            _writeDataMaps = writeDataMaps;
            ReadStartingAddress = readStartingAddress;
            WrtiteStartingAddress = wrtiteStartingAddress;
        }

        public void GetDataFromBuffer(in byte[] buffer, ref Tout data)
        {
            var properties = data.GetType().GetProperties();
            foreach (var p in properties)
            {
                int address = _readDataMaps.FirstOrDefault(d=>d.PropertyName == p.Name).Address;
                object value = null;

                if (p.PropertyType == typeof(string))
                {
                    int byteSize = _readDataMaps.FirstOrDefault(d => d.PropertyName == p.Name).ByteSize;
                    value = S7.GetCharsAt(buffer, address, byteSize);
                }
                else if (p.PropertyType == typeof(byte))
                {
                    value = S7.GetByteAt(buffer, address);
                }
                else
                {
                    throw new Exception($"Invalid property type: {p.PropertyType}");
                }

                p.SetValue(data,value);
            }
        }

        public void SetDataToBuffer(ref byte[] buffer, ref Tin data)
        {
            //TODO: eksik
            throw new NotImplementedException();
        }
    }
}
