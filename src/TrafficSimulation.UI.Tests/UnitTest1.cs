using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TrafficSimulation.UI.Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      using(ShimsContext.Create())
    }
  }
}
