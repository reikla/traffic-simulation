using System.Linq;
using TrafficSimulation.Simulation.Engine.VehicleExchange;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// A route that is used to exchange Vehicles with other groups
  /// </summary>
  public class VehicleExchangeRoute : Route
  {
    private readonly IVehicleExchange _vehicleExchange;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleExchangeRoute"/> class.
    /// </summary>
    /// <param name="vehicleExchange">The vehicle exchange.</param>
    /// <param name="route">The normal route.</param>
    public VehicleExchangeRoute(IVehicleExchange vehicleExchange, IRoute route) : base(route.NodesConnections.ToArray())
    {
      _vehicleExchange = vehicleExchange;
    }

    /// <summary>
    /// Destroys a vehicle and sends it to another group.
    /// </summary>
    /// <param name="vehicle">The vehicle to destroy</param>
    public override void DestoryVehicle(IVehicle vehicle)
    {
      _vehicleExchange.SendVehicleAway(vehicle);
      base.DestoryVehicle(vehicle);
    }
  }
}