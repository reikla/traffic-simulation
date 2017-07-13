using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSimulation.Simulation.Engine;

namespace TrafficSimulation.Simulation.Tests
{
  [TestClass]
  public class EnumToggleTest
  {
    [TestMethod]
    public void Engine_Toggle_OK()
    {
      Assert.AreEqual(TrafficLightState.Red, TrafficLightState.Green.Toggle());
      Assert.AreEqual(TrafficLightState.Green, TrafficLightState.Red.Toggle());
    }

  }
}