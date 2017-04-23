using NLog;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.SimulationObjects
{
  /// <summary>
  /// Represents a vehicle in the simulation
  /// </summary>
  public class Vehicle : IVehicle
  {
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    public Vehicle(VehicleType type)
    {
      VehicleType = type;
    }
    public void Tick(double timespan)
    {
      _logger.Trace($"Tick: {this}");
    }

    public VehicleType VehicleType { get; set; }
  }
}
