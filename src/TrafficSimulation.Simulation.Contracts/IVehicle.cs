namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// Represents a vehicle in the simulation
  /// </summary>
  public interface IVehicle : ITickable, IPlaceable, ISimulationBase
  {
    /// <summary>
    /// the type of the vehicle
    /// </summary>
    VehicleType VehicleType { get; set; }
  }
}