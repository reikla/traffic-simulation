using System.Timers;
using NLog;
using TrafficSimulation.TrafficLightControl.Engine.Settings;

namespace TrafficSimulation.TrafficLightControl.Engine
{
  public class TrafficLightControlEngine : IEngine
  {

    private bool _isInitialized;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Timer SimulationTimer;
    private SimulationSettings _settings;

    public void Start()
    {
      Logger.Debug("Start");
    }

    public void Stop()
    {
      Logger.Debug("Stop");
    }

    public void Step()
    {
      Logger.Debug("Step");
    }

    public void Init()
    {
      Logger.Debug("Start");
    }
  }

}
