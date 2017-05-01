namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Manages the lifetime of vehicles
  /// </summary>
  public interface IVehicleLifetimeManager
  {
    /// <summary>
    /// Creates a new vehicle
    /// </summary>
    /// <returns>The newly created vehicle</returns>
    IVehicle CreateVehicle();

    /// <summary>
    /// Destroys a vehicle
    /// </summary>
    /// <param name="vehicle">The vehicle to destroy</param>
    void DestoryVehicle(IVehicle vehicle);
  }
}