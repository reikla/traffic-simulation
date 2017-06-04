namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// Strategy to encapsulate the acceleration of a vehicle
  /// </summary>
  public interface IAccelerationStrategy
  {
    /// <summary>
    /// Calculates the acceleration of a vehicle
    /// </summary>
    void CalculateAcceleration();
  }
}