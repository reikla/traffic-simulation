namespace TrafficSimulation.Simulation.Engine.VehicleExchange
{
  /// <summary>
  /// The Interface for exchanging vehicles with other groups
  /// </summary>
  public interface IVehicleExchange
  {

    /// <summary>
    /// Sends the vehicle away to another group
    /// </summary>
    /// <param name="vehicle">The vehicle.</param>
    void SendVehicleAway(IVehicle vehicle);

    /// <summary>
    /// Receives the vehicle.
    /// </summary>
    /// <returns>A vehicle if one is available. Otherwise null</returns>
    IVehicle ReceiveVehicle();

    /// <summary>
    /// Enables the receiving of vehicles.
    /// </summary>
    void Enable();

    /// <summary>
    /// Disables the receiving of vehicles.
    /// </summary>
    void Disable();
  }
}