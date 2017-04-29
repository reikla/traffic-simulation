using System.ServiceModel;
using TrafficSimulation.TrafficLightControl.Contracts;

namespace TrafficSimulation.TrafficLightControl.WebService
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

  public class TrafficLightControlService : ITrafficLightService
  {
    public void Foo()
    {
      throw new System.NotImplementedException();
    }
  }
}