namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// Represents a start node of a route, that can create vehicles.
  /// </summary>
  public interface IStartNode : INode
  {
    /// <summary>
    /// Creates a vehicle
    /// </summary>
    /// <returns>A new vehicle</returns>
    IVehicle CreateVehicle();
  }
}