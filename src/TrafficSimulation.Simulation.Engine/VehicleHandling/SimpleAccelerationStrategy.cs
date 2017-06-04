namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// A very simple implementation of a <see cref="IAccelerationStrategy"/>
  /// </summary>
  /// <seealso cref="IAccelerationStrategy" />
  internal class SimpleAccelerationStrategy : IAccelerationStrategy
  {
    private readonly Vehicle _vehicle;
    internal SimpleAccelerationStrategy(Vehicle vehicle)
    {
      _vehicle = vehicle;
    }

    /// <summary>
    /// Calculates the acceleration of a vehicle
    /// </summary>
    public void CalculateAcceleration()
    {
      if (_vehicle.Route.GetNextPlaceable(_vehicle).NextPlaceable != null)
      {
        if (_vehicle.Route.GetNextPlaceable(_vehicle).DistanceInMeters < 5)
        {
          _vehicle.Acceleration = 0;
        }
      }
      _vehicle.CurrentVelocity = _vehicle.CurrentVelocity > _vehicle.Physics.MaxVelocity ? 0 : 1;
    }
  }
}