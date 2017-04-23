namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// Represents an endnode of a route. Can destroy vehicles because they reached their target.
  /// </summary>
  public interface IEndNode : INode
  {
    /// <summary>
    /// Destroys a vehicle
    /// </summary>
    /// <param name="vehicle"></param>
    void DestroyVehicle(IVehicle vehicle);
  }
}