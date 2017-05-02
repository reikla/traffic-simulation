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
        throw new System.NotImplementedException();
      }

      public void Stop()
      {
        throw new System.NotImplementedException();
      }

      public void Step()
      {
        throw new System.NotImplementedException();
      }

      public void Init()
      {
        throw new System.NotImplementedException();
      }
    }

}
