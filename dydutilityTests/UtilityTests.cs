using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dydutility;

namespace dydutility.Tests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void TestUtility()
        {
            Utility test = new Utility();
            Assert.AreEqual<string>(test.RemoveColorModifiers("^0Te^2st^1^0 test^22 ^ 2"), "Test test2 ^ 2");
        }
    }
}
