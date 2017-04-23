using NLog;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.SimulationObjects
{
  /// <summary>
  /// Represents a vehicle in the simulation
  /// </summary>
  public class Vehicle : SimulationBase, IVehicle
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public Vehicle(VehicleType type)
    {
      VehicleType = type;
    }
    public void Tick(double timespan)
    {
      Logger.Trace($"Tick: {this}");
    }

    public VehicleType VehicleType { get; set; }
  }
}
