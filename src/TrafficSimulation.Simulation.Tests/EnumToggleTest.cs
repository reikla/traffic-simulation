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
      Assert.AreEqual(TrafficLightState.Red, TrafficLightState.Disabled.Toggle());
      Assert.AreEqual(TrafficLightState.Green, TrafficLightState.Red.Toggle());
      Assert.AreEqual(TrafficLightState.Disabled, TrafficLightState.Green.Toggle());
    }

  }
}