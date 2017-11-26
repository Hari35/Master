
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
namespace MsTestRowTestExample
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }
 
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"Data\Data.csv", "Data#csv", DataAccessMethod.Sequential)]
        public void TestMethod1()
        {
            // Arrange
            int a = Convert.ToInt32(TestContext.DataRow[0]);
            int b = Convert.ToInt32(TestContext.DataRow[1]);
            int expected = Convert.ToInt32(TestContext.DataRow[2]);
            string message = TestContext.DataRow[3].ToString();
 
            // Act
            var actual = Math.Min(a, b);
 
            // Assert
            Assert.AreEqual(expected, actual, message);
        }
    }
}