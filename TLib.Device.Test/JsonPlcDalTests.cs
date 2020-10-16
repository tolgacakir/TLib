using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLib.Device.Plc.Abstract;
using TLib.Device.Plc.Concrete.Json;

namespace TLib.Device.Test
{
    [TestClass]
    public class JsonPlcDalTests
    {
        [TestMethod]
        public void Should_Read_When_Connected()
        {
            IPlcDal<Tr,Tr> plcDal = new JsonPlcDal<Tr,Tr>();

            plcDal.Connect();

            Tr readData = new Tr();
            readData.Age = 25;
            plcDal.Read(ref readData);

            Assert.AreEqual("tolga", readData.FirstName);
        }
    }


    internal class Tr : IPlcData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

}
