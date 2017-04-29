using System.ServiceModel;

namespace TrafficSimulation.TrafficLightControl.Contracts
{
  [ServiceContract]
  public interface ITrafficLightService
  {
    [OperationContract]
    void Foo();

  }
}