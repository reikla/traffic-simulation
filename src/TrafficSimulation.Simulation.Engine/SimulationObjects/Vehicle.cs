﻿using NLog;
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
    private readonly IRoute _route;
    private double _currentVelocity;

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
      if (_route.GetNextPlaceable(this).NextPlaceable != null)
      {
        if (_route.GetNextPlaceable(this).DistanceInMeters < 150)
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
    public VehicleType VehicleType { get; set; }
    public IPosition Position { get; set; }
    public bool IsConnectionBlocking { get; set; }
  }
}