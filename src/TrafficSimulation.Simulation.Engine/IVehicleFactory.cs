
namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// Factory to create new vehicles
  /// </summary>
  public interface IVehicleFactory
  {
    IVehicle CreateVehicle();
  }
}