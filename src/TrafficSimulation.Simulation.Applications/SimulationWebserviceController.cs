using System.ServiceModel;
using TrafficSimulation.Simulation.Engine;

namespace TrafficSimulation.Simulation.Applications
{
  public class SimulationWebserviceController : IController
  {
    private ServiceHost _serviceHost;

    public void Start()
    {
      _serviceHost = new ServiceHost(typeof(SimulationService));
      _serviceHost.Open();
    }

    public void Shutdown()
    {
      if (_serviceHost != null)
      {
        _serviceHost.Close();
        _serviceHost = null;
      }
    }
  }
}