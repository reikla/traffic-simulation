using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSimulation.Simulation.Contracts.Exceptions;
using TrafficSimulation.Simulation.Engine;

namespace TrafficSimulation.Simulation.Tests
{
  [TestClass]
  public class EngineTests
  {
    [TestMethod]
    [ExpectedException(typeof(EngineInitializationException))]
    public void Engine_InitTwice_Throws()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Init();
    }
    [TestMethod]
    public void Engine_TestStartInitialized_OK()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Start();
    }
    [TestMethod]
    public void Engine_TestStopInitialized_OK()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Start();
      engine.Stop();
    }
    [TestMethod]
    public void Engine_TestStepInitialized_OK()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Step();
    }

    [TestMethod]
    [ExpectedException(typeof(EngineInitializationException))]
    public void Engine_TestStartNotInit_Throws()
    {
      var engine = new SimulationEngine();
      engine.Start();
    }
    [TestMethod]
    [ExpectedException(typeof(EngineInitializationException))]
    public void Engine_TestStopNotInit_Throws()
    {
      var engine = new SimulationEngine();
      engine.Stop();
    }
    [TestMethod]
    [ExpectedException(typeof(EngineInitializationException))]
    public void Engine_TestStepNotInit_Throws()
    {
      var engine = new SimulationEngine();
      engine.Step();
    }

    [TestMethod]
    [ExpectedException(typeof(EngineStateException))]
    public void Engine_StartAlreadyRunning_Throws()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Start();
      engine.Start();
    }

    [TestMethod]
    [ExpectedException(typeof(EngineStateException))]
    public void Engine_StopNotRunning_Throws()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Stop();
    }

    [TestMethod]
    [ExpectedException(typeof(EngineStateException))]
    public void Engine_StepIsRunning_Throws()
    {
      var engine = new SimulationEngine();
      engine.Init();
      engine.Start();
      engine.Step();
    }
  }
}
