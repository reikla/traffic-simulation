using System.Timers;
using NLog;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.TrafficLightControl.Engine.Settings;

namespace TrafficSimulation.TrafficLightControl.Engine
{
  public class TrafficLightControlEngine : IEngine
  {
    private bool _isInitialized;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Timer SimulationTimer;
    private SimulationSettings _settings;
    private ISimulationService SimulationService;
    private int _secondsSinceStart = 0;
    private static readonly int TOGGLING_INTERVALL = 5;

    public void Start()
    {
      Logger.Debug("Start");
      if (SimulationService == null)
      {
        SimulationService = ChannelFactoryBuilder.GetChannelFactory<ISimulationService>("net.pipe://localhost/Simulation/Engine").CreateChannel();
      }

      if (SimulationTimer == null)
      {
        SimulationTimer = new Timer(1000);
        SimulationTimer.Elapsed += SimulationTimer_Elapsed;
        SimulationTimer.Start();
      }

      SimulationService.ToggleTrafficLight(2);
      SimulationService.ToggleTrafficLight(3);
    }

    private void SimulationTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      DoStep();
    }

    private void DoStep()
    {
      _secondsSinceStart++;

      if (_secondsSinceStart % TOGGLING_INTERVALL == 0)
      {
        for (var trafficLightIndex = 0; trafficLightIndex < 4; trafficLightIndex++)
        {
          SimulationService.ToggleTrafficLight(trafficLightIndex);
        }
      }
    }

    public void Stop()
    {
      SimulationTimer.Elapsed -= SimulationTimer_Elapsed;
      SimulationTimer.Stop();
      SimulationTimer = null;
      SimulationService = null;
    }

    public void Step()
    {
      DoStep();
    }

    public void Init()
    {
    }
  }

}
