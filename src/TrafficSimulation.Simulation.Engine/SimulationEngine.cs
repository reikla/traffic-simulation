using NLog;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Engine
{
  public class SimulationEngine : IEngine
  {
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    public void Start()
    {
      _logger.Trace("Starting Simulation");
    }

    public void Stop()
    {
      _logger.Trace("Stopping Simulation");
    }

    public void Step()
    {
      _logger.Trace("Simulating a step.");
    }

    public void Init()
    {
      _logger.Trace("Init Simulation Engine");
    }
  }
}
