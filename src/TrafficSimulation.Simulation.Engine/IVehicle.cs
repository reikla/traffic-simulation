
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
  }
}