using System;

namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// An implementation of the Intelligent Driver Model (IDM)
  /// 
  /// </summary>
  public class IntelligentDriverModelStrategy : IAccelerationStrategy
  {

    private static readonly double Minimal_Distance = 2;
    private static readonly double Security_Time_Distance = 1.4;

    private readonly Vehicle _vehicle;
    /// <summary>
    /// ctor
    /// </summary>
    public IntelligentDriverModelStrategy(Vehicle vehicle)
    {
      _vehicle = vehicle;
    }

    /// <summary>
    /// Calculates the acceleration of a vehicle
    /// </summary>
    public void CalculateAcceleration()
    {
      _vehicle.Acceleration = _vehicle.Physics.MaxAccelelration *
                              (1 - Math.Pow(_vehicle.CurrentVelocity / _vehicle.Physics.MaxVelocity, 2) -
                               (CalculateWantedDistance() / GetSAlpha()));
    }

    private double GetSAlpha()
    {
      var nextPlacable = _vehicle.Route.GetNextPlaceable(_vehicle);
      var sAlpha =  nextPlacable?.DistanceInMeters ?? double.PositiveInfinity;
      return sAlpha;
    }

    private double CalculateWantedDistance()
    {
      var wantedDistance =  Minimal_Distance + Security_Time_Distance * _vehicle.CurrentVelocity +
             (_vehicle.CurrentVelocity * GetVelocityDifference()) /
             (2 * Math.Pow(_vehicle.Physics.MaxAccelelration * _vehicle.Physics.MaxDeceleration, 2));
      return wantedDistance;
    }

    private double GetVelocityDifference()
    {
      var nextPlacable = _vehicle.Route.GetNextPlaceable(_vehicle);
      double nextItemSpeed = 0;
      if (nextPlacable?.NextPlaceable is IVehicle)
      {
        var nextVehicle = (IVehicle) nextPlacable.NextPlaceable;
        nextItemSpeed = nextVehicle.CurrentVelocity;
      }
      var velocityDifference =  _vehicle.CurrentVelocity - nextItemSpeed;
      return velocityDifference;
    }
  }
}