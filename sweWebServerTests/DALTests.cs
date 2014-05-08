using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using sweWebServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sweWebServer.Tests
{
    [TestClass()]
    public class DALTests
    {
        [TestMethod()]
        public void DALTest()
        {
            DAL dal = new DAL();

            Assert.IsNotNull(dal);
        }

        [TestMethod()]
        public void openConnectionTest()
        {
            DAL dal = new DAL();
            Assert.IsTrue(dal.openConnection());
        }

        [TestMethod()]
        public void testtestTest()
        {
            DAL dal = new DAL();
            Assert.IsNotNull(dal.testtest("TEST"));
        }

        [TestMethod()]
        public void updateTest()
        {
            SqlCommand cmd = new SqlCommand();
            DAL dal = new DAL();
            Assert.IsFalse(dal.update(cmd));
        }
    }
}
