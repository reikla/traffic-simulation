using System.ServiceModel;

namespace TrafficSimulation.TrafficLightControl.Contracts
{
  [ServiceContract]
  public interface ITrafficLightService
  {

    ///<summary>
    /// Start traffic light control
    /// </summary>

    [OperationContract]
    void Start();

    ///<summary>
    /// Stop traffic light control
    /// </summary>

    [OperationContract]
    void Stop();

    ///<summary>
    /// Performs a single Step
    /// </summary>

    [OperationContract]
    void Step();

  }
}