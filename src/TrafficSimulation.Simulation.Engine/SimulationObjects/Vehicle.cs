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
    private IRoute _route;

    public Vehicle(VehicleType type, IPosition position, IRoute route)
    {
      VehicleType = type;
      IsConnectionBlocking = true;
      _currentVelocity = 0;
      _physics = new VehiclePhysics();
      _route = route;
      Position = position;
    }
    public void Tick(double timespan)
    {
      Logger.Trace($"Tick: {this}");
      Placer.Place(this, _route, GetVelocity(timespan) * timespan);
    }

    private double GetAcceleration()
    {
      return _currentVelocity > _physics.MaxVelocity ? 0 : 1;
    }

    private double GetVelocity(double deltaT)
    {
      _currentVelocity = _currentVelocity + GetAcceleration() * deltaT;
      return _currentVelocity;
    }
    public VehicleType VehicleType { get; set; }
    public IPosition Position { get; set; }
    public bool IsConnectionBlocking { get; set; }
  }
}
