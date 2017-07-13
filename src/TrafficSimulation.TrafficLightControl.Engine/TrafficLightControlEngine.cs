using System.Timers;
using NLog;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.TrafficLightControl.Engine
{
  /// <summary>
  /// 
  /// </summary>
  public class TrafficLightControlEngine : IEngine
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Timer _simulationTimer;
    private ISimulationService _simulationService;
    private int _secondsSinceStart = 0;

    /// <summary>
    /// Starts the simulation.
    /// </summary>
    public void Start()
    {
      Logger.Debug("Start");
      if (_simulationService == null)
      {
        _simulationService = ChannelFactoryBuilder.GetChannelFactory<ISimulationService>("net.pipe://localhost/Simulation/Engine").CreateChannel();
      }

      if (_simulationTimer == null)
      {
        _simulationTimer = new Timer(1000);
        _simulationTimer.Elapsed += SimulationTimer_Elapsed;
        _simulationTimer.Start();
      }

      _simulationService.ToggleTrafficLight(2);
      _simulationService.ToggleTrafficLight(3);
    }


    /// <summary>
    /// Stops the simulation.
    /// </summary>
    public void Stop()
    {
      _simulationTimer.Elapsed -= SimulationTimer_Elapsed;
      _simulationTimer.Stop();
      _simulationTimer = null;
      _simulationService = null;
    }

    /// <summary>
    /// Simulates a single step.
    /// </summary>
    public void Step()
    {
      DoStep();
    }

    /// <summary>
    /// Initalizes the Simulation.
    /// </summary>
    public void Init()
    {
    }

    private void SimulationTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      DoStep();
    }
    private void DoStep()
    {
      _secondsSinceStart++;

      if (_secondsSinceStart % Constants.TrafficLightToggleInterval == 0)
      {
        for (var trafficLightIndex = 0; trafficLightIndex < 4; trafficLightIndex++)
        {
          _simulationService.ToggleTrafficLight(trafficLightIndex);
        }
      }
    }
  }

}
