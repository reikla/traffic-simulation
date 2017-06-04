
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// Represents a vehicle in the simulation
  /// </summary>
  public interface IVehicle : ITickable, IPlaceable, ISimulationBase
  {
    /// <summary>
    /// the type of the vehicle
    /// </summary>
    VehicleType VehicleType { get; set; }

    /// <summary>
    /// Gets or sets the route.
    /// </summary>
    IRoute Route { get; }

    /// <summary>
    /// Sets the route. And the Position to the startposition of the route.
    /// </summary>
    void SetRoute(IRoute route);

    /// <summary>
    /// Gets or sets a value indicating whether this instance is a foreign vehicle.
    /// </summary>
    bool IsForeignVehicle { get; set; }

    /// <summary>
    /// The current velocity of the vehicle
    /// </summary>
    double CurrentVelocity { get; set; }

    /// <summary>
    /// Gets or sets the acceleration.
    /// </summary>
    double Acceleration { get; set; }
  }
}