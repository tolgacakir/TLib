using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;
using TLib.Device.Plc.Concrete.Sharp7;

namespace TLib.Device.Test
{
    [TestClass]
    public class S7DataMapperTests
    {

        [TestMethod]
        public void Should_Get_Data_From_Buffer()
        {
            var readData = new TestReadData();
            List<DataMap> dataMaps = new List<DataMap>();
            dataMaps.Add(new DataMap
            {
                PropertyName = nameof(readData.PlcRun),
                Address = 0,
                ByteSize = 1,
            });
            dataMaps.Add(new DataMap
            {
                PropertyName = nameof(readData.ProductNumber),
                Address = 1,
                ByteSize = 5
            });


            var dataMapper = new S7DataMapper<TestReadData, TestWriteData>(0,dataMaps, new List<DataMap>(),0,0);

            var buffer = new byte[6];
            buffer[0] = 17;
            buffer[1] = 99;
            buffer[2] = 97;
            buffer[3] = 107;
            buffer[4] = 105;
            buffer[5] = 114;
            dataMapper.GetDataFromBuffer(buffer,ref readData);

            Assert.AreEqual("cakir", readData.ProductNumber);
        }       
    }

    public class TestReadData : IPlcData
    {
        public byte PlcRun { get; set; }
        public string ProductNumber { get; set; }
    }

    public class TestWriteData : IPlcData
    {
        public byte PcRun { get; set; }
        public byte PcResponse { get; set; }
    }

}
