using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Form.Logic;
using MyLib;

namespace Form.Tests
{
    [TestFixture]
    public class FormLogicTest
    {
        [SetUp]
        public void Setup()
        {
            DataAccess.Instance("BizDB").ExecuteNonQuery("update WF_PROCESS set HASFORM='0'");
        }
        
        [Test]
        public void CreateFormTest()
        {
            FormLogic logic = new FormLogic();
            logic.CreateForm("BusinessTrip");
            Assert.AreEqual(1,1);
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1,1);
        }
    }
}
