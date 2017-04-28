using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// The Simulation Webservice
  /// </summary>
  [ServiceContract]
  public interface ISimulationService
  {
    /// <summary>
    /// Starts the Simulation
    /// </summary>
    [OperationContract]
    void Start();

    /// <summary>
    /// Stops the Simulation
    /// </summary>
    [OperationContract]
    void Stop();

    /// <summary>
    /// Performs a single Step
    /// </summary>
    [OperationContract]
    void Step();

    /// <summary>
    /// Get the list of nodes.
    /// </summary>
    [OperationContract]
    ReadOnlyCollection<Node> GetNodes();

    /// <summary>
    /// Retursn a list of Vehicles
    /// </summary>
    [OperationContract]
    List<Vehicle> GetVehicles();

    /// <summary>
    /// Returns a list of NodeConnections
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    ReadOnlyCollection<NodeConnection> GetNodeConnections();
  }
}