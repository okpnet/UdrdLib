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
            item.TestId = 10;
            item.TestId = 9;
            item.TestId = 8;
            item.TestId = 7;
            item.TestId = 6;
            mng.ToPrev();
            mng.ToPrev();
            mng.ToPrev();
            mng.ToNext();
            mng.ToNext();
            item.TestId = 99;
            Assert.Fail();
        }
    }
}