using System.ServiceModel;
using TrafficSimulation.TrafficLightControl.Contracts;
using TrafficSimulation.TrafficLightControl.Engine;

namespace TrafficSimulation.TrafficLightControl.WebService
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

  internal class TrafficLightControlService : ITrafficLightService
  {
    private readonly TrafficLightControlEngine _engine;
    public TrafficLightControlService()
    {
      _engine = new TrafficLightControlEngine();
      _engine.Init();
    }

    public void Start()
    {
      _engine.Start();
    }

    public void Stop()
    {
      _engine.Stop();
    }

    public void Step()
    {
      _engine.Step();
    }
  }
}