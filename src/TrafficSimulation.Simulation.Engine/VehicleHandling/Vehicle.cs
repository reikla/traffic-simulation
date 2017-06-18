using System;
using NLog;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Engine.Environment;
using TrafficSimulation.Simulation.Engine.SimulationObjects;

namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// Represents a vehicle in the simulation
  /// </summary>
  public class Vehicle : SimulationBase, IVehicle
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly IAccelerationStrategy _accelerationStrategy;
    private readonly ILaneChangeStrategy _laneChangeStrategy;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vehicle"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="route">The route.</param>
    public Vehicle(VehicleType type, IRoute route) : this(type)
    {
      SetRoute(route);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vehicle"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    public Vehicle(VehicleType type)
    {
      VehicleType = type;
      IsConnectionBlocking = true;
      Physics = new VehiclePhysics();
      _accelerationStrategy = new IntelligentDriverModelStrategy(this);
      _laneChangeStrategy = new NoLaneChangeStrategy();
      Physics = new VehiclePhysics();
    }

    ///<inheritdoc />
    public void Tick(double timespan)
    {
      Logger.Trace($"Tick: {this}");
      if (_laneChangeStrategy.ShouldChangeLange())
      {
        _laneChangeStrategy.ChangeLane();
      }
      _accelerationStrategy.CalculateAcceleration();

      System.Diagnostics.Debug.Assert(Route != null, "Route not set. Can't simulate this!");
      Placer.Place(this, Route, GetVelocity(timespan) * timespan);
    }

    private double GetVelocity(double deltaT)
    {
      var calculatedNewVelocety = CurrentVelocity + Acceleration * deltaT;
      CurrentVelocity = calculatedNewVelocety;/*calculatedNewVelocety < 0 ? 0 : calculatedNewVelocety*/;
      return CurrentVelocity;
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
      Route.Vehicles.Add(this);
    }

    internal VehiclePhysics Physics { get; }

    /// <summary>
    /// Gets or sets the acceleration.
    /// </summary>
    public double Acceleration { get; set; }

    /// <summary>
    /// Sets a car as defective.
    /// </summary>
    /// <param name="isDefect"></param>
    public void SetDefect(bool isDefect)
    {
      IsDefect = isDefect;
      Physics.MaxVelocity = isDefect ? 0 : new Random().Next(22, 33);
    }

    /// <summary>
    /// The current velocity of the vehicle
    /// </summary>
    public double CurrentVelocity { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is a foreign vehicle.
    /// </summary>
    public bool IsForeignVehicle { get; set; } = false;

    /// <summary>
    /// Signals if a vehicle is defect.
    /// </summary>
    public bool IsDefect { get; set; } = false;

  }
}
