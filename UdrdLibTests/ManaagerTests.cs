using Microsoft.VisualStudio.TestTools.UnitTesting;
using UdrdLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdrdLibTests;

namespace UdrdLib.Tests
{
    [TestClass()]
    public class ManaagerTests
    {
        [TestMethod()]
        public void AddObserveItemTest()
        {
            var item = new Test();
            var mng= new ExecuteHistory();
            mng.AddObserveItem(item);
            item.Id = 10;

            Assert.Fail();
        }
    }
}