using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebService.Controllers;

namespace SimpleWebService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test get method which find value for key "name", which is expeceted to be "Gideon".
        /// </summary>
        [TestMethod]
        public void TestGetMethod()
        {
            var controller = new ValuesController();
            controller.file.ReadFromFile();
            var result = controller.Get();
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("Gideon", result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Test put method which insert "name":"Gideon", which is expected to fail because it already exists.
        /// </summary>
        [TestMethod]
        public void TestPutMethod()
        {
            var controller = new ValuesController();
            controller.file.ReadFromFile();
            var result = controller.Put();
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
