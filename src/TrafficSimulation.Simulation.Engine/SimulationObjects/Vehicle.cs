using System.Diagnostics;
using NLog;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.SimulationObjects
{
  /// <summary>
  /// Represents a vehicle in the simulation
  /// </summary>
  public class Vehicle : SimulationBase, IVehicle
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly VehiclePhysics _physics;
    private double _currentVelocity;


    /// <summary>
    /// Initializes a new instance of the <see cref="Vehicle"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="route">The route.</param>
    public Vehicle(VehicleType type, IRoute route)
    {
      VehicleType = type;
      IsConnectionBlocking = true;
      _currentVelocity = 0;
      _physics = new VehiclePhysics();
      SetRoute(route);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vehicle"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    public Vehicle(VehicleType type)
    {
      VehicleType = type;
      _physics = new VehiclePhysics();
    }

    ///<inheritdoc />
    public void Tick(double timespan)
    {
      Logger.Trace($"Tick: {this}");
      Debug.Assert(Route != null, "Route not set. Can't simulate this!");
      Placer.Place(this, Route, GetVelocity(timespan) * timespan);
    }

    private double GetAcceleration()
    {
      if (Route.GetNextPlaceable(this).NextPlaceable != null)
      {
        if (Route.GetNextPlaceable(this).DistanceInMeters < 5)
        {
          return 0;
        }
      }
      return _currentVelocity > _physics.MaxVelocity ? 0 : 1;
    }

    private double GetVelocity(double deltaT)
    {
      _currentVelocity = _currentVelocity + GetAcceleration() * deltaT;
      return _currentVelocity;
    }

    ///<inheritdoc />
    public VehicleType VehicleType { get; set; }

    ///<inheritdoc />
    public IPosition Position { get; set; }

    ///<inheritdoc />
    public bool IsConnectionBlocking { get; set; }

    /// <summary>
    /// Gets the route.
    /// </summary>
    public IRoute Route { get; private set; }

    /// <summary>
    /// Sets the route. And the Position to the startposition of the route.
    /// </summary>
    public void SetRoute(IRoute route)
    {
      Route = route;
      Position = new Position(Route.NodesConnections[0]);
    }
  }
}
