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
            var arrayItem = new Test() { Name = "array" };
            item.Children.Add(arrayItem);
            arrayItem.Name = "change";
            item.TestId = 10;
            System.Diagnostics.Debug.WriteLine(item.ToString());
            item.TestId = 9;
            System.Diagnostics.Debug.WriteLine(item.ToString());
            item.TestId = 8;
            System.Diagnostics.Debug.WriteLine(item.ToString());
            item.TestId = 7;
            System.Diagnostics.Debug.WriteLine(item.ToString());
            item.TestId = 6;
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//7
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//8
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//9
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//10
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//array change
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//array add
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//array init
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToPrev();//init
            System.Diagnostics.Debug.WriteLine(item.ToString());
            var result=mng.ToPrev();//false
            System.Diagnostics.Debug.WriteLine(item.ToString());
            result = mng.ToPrev();//false
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//arra init
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//array add
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//array change
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//10
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//9
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//8
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//7
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//6
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//false
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//false
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//false
            System.Diagnostics.Debug.WriteLine(item.ToString());
            item.TestId = 99;
            System.Diagnostics.Debug.WriteLine(item.ToString());
            mng.ToNext();//false
            System.Diagnostics.Debug.WriteLine(item.ToString());
            //Assert.Fail();
        }
    }
}