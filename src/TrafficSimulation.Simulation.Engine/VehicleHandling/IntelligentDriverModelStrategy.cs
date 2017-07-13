using System;
using System.Linq;
using NLog;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// An implementation of the Intelligent Driver Model (IDM)
  /// 
  /// </summary>
  public class IntelligentDriverModelStrategy : IAccelerationStrategy
  {
    private static readonly double Lookahead_Time = 5;
    private static readonly double Minimal_Distance = 2;
    private static readonly double Security_Time_Distance = 1.4;
    private static readonly double Car_Length = 4;
    private static readonly double Intersection_Width = 4;

    private readonly Vehicle _vehicle;

    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
      var nextPlacable = _vehicle.Route.GetNextPlaceable(_vehicle);
      double distanceToNextPlacable = nextPlacable?.DistanceInMeters ?? 999;

      var nextIntersection = _vehicle.Position.NodeConnection.EndNode;

      var distanceToIntersection = _vehicle.Position.NodeConnection.Length - _vehicle.Position.PositionOnConnection;

      double wantedDistance, sAlpha;
      bool debugDeleteME = false;
      if (_vehicle.Id == 332)
      {

        Logger.Info($"Velocity: {_vehicle.CurrentVelocity}");
      }

      if (distanceToIntersection < distanceToNextPlacable && IsBlockedForVehicle(nextIntersection, _vehicle))
      {
        var intersectionDistanceObject = Distance<INode>.CreateDistance(nextIntersection, distanceToIntersection);
        wantedDistance = CalculateWantedDistance(intersectionDistanceObject);
        sAlpha = GetSAlpha(intersectionDistanceObject);
        debugDeleteME = _vehicle.Id == 332;
      }
      
      //the distance to the next car is smaller than the distance to the next intersection -> we must handle the next car
      else
      {
        wantedDistance = CalculateWantedDistance(nextPlacable);
        sAlpha = GetSAlpha(nextPlacable);
      }

      _vehicle.Acceleration = _vehicle.Physics.MaxAccelelration *
                              (1 - Math.Pow(_vehicle.CurrentVelocity / _vehicle.Physics.MaxVelocity, 2) -
                               (wantedDistance / sAlpha));

    }

    private bool IsBlockedForVehicle(INode nextIntersection, Vehicle vehicle)
    {
      return IsPriorityCarToNote(nextIntersection, vehicle);






//      var distanceToIntersection = vehicle.Position.NodeConnection.Length - vehicle.Position.PositionOnConnection;
//
//      double distanceUntilBlockIntersection, distanceUntilIntersectionUnblocked;
//      double timeToEnterIntersection, timeToLeaveIntersection;
//      if (vehicle.Acceleration == 0)
//      {
//        
//      }



    }

    private bool IsPriorityCarToNote(INode nextIntersection, Vehicle vehicle)
    {
      var priorityLane = nextIntersection.NodeConnections.FirstOrDefault(
        connection => connection.ConnectionOrientation == vehicle.Position.NodeConnection.ConnectionOrientation
                        .GetPriorityOrientation());
      if (priorityLane == null)
      {
        return false;
      }
      var priorityCar = priorityLane.Placeables.OrderBy(placable => placable.Position.PositionOnConnection)
        .LastOrDefault() as IVehicle;
      if (priorityCar == null)
      {
        return false;
      }

      if (CalculateTimeToIntersection(nextIntersection, priorityCar) > Lookahead_Time)
      {
        return false;
      }

      var timeToEnterIntersection = CalculateTimeToIntersection(nextIntersection, vehicle, Minimal_Distance + (Car_Length/2));
      var timeToExitIntersection = CalculateTimeToIntersection(nextIntersection, vehicle, Minimal_Distance + (Car_Length / 2) + Intersection_Width);

      var timeForPriorityCarToEnterIntersection =
        CalculateTimeToIntersection(nextIntersection, priorityCar, Minimal_Distance + (Car_Length / 2));

      var timeForPriorityCarToExitIntersection =
        CalculateTimeToIntersection(nextIntersection, priorityCar, Minimal_Distance + (Car_Length / 2) + Intersection_Width);

      return (timeToExitIntersection < timeForPriorityCarToEnterIntersection ||
              timeToEnterIntersection > timeForPriorityCarToExitIntersection);


    }

    private double CalculateTimeToIntersection(INode intersectionNode, IVehicle vehicle, double deltaS = 0)
    {
      return CalculateTimeToTraveDistance(vehicle.Acceleration, vehicle.CurrentVelocity,
        vehicle.Position.NodeConnection.Length - vehicle.Position.PositionOnConnection);
    }


    /// <summary>
    /// Calculates the time to trave distance.
    /// </summary>
    /// <param name="a">acceleration</param>
    /// <param name="v">velocity</param>
    /// <param name="s">distance</param>
    /// <param name="deltaS">distanceOffset will get added to s</param>
    /// <returns></returns>
    private double CalculateTimeToTraveDistance(double a, double v, double s, double deltaS = 0)
    {
      if (Math.Abs(a) < 0.01) // we don't have a acceleration
      {
        return (s+deltaS) / v;
      }
      else
      {
        return (Math.Sqrt(2 * a * (s+deltaS) + Math.Pow(v, 2)) - v) / a;
      }
    }

    private double GetSAlpha<T>(IDistance<T> nextPlacable) where T : class 
    {
      var sAlpha = nextPlacable?.DistanceInMeters ?? 999;
      return sAlpha;
    }

    private double CalculateWantedDistance<T>(IDistance<T> nextPlacable) where T : class 
    {
      var wantedDistance = Car_Length + Minimal_Distance + Security_Time_Distance * _vehicle.CurrentVelocity +
             (_vehicle.CurrentVelocity * GetVelocityDifference(nextPlacable)) /
             (2 * Math.Pow(_vehicle.Physics.MaxAccelelration * _vehicle.Physics.MaxDeceleration, 2));
      return wantedDistance;
    }

    private double GetVelocityDifference<T>(IDistance<T> nextPlacable) where T : class
    {
      double nextItemSpeed = 0;
      if (nextPlacable?.NextPlaceable is IVehicle)
      {
        var nextVehicle = (IVehicle)nextPlacable.NextPlaceable;
        nextItemSpeed = nextVehicle.CurrentVelocity;
      }
      else // we found a traffic light 
      {

      }
      var velocityDifference = _vehicle.CurrentVelocity - nextItemSpeed;
      return velocityDifference;
    }
  }
}