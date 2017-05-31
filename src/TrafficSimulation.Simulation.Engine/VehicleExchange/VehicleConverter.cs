using VehicleHandoverLibrary;
using VehicleType = TrafficSimulation.Simulation.Contracts.VehicleType;

namespace TrafficSimulation.Simulation.Engine.VehicleExchange
{
    internal static class VehicleConverter
    {
      internal static IVehicle Convert(Vehicle vehicle)
      {
        return new SimulationObjects.Vehicle(VehicleType.Car);
      }

      internal static Vehicle Convert(IVehicle vehicle)
      {
        return new Vehicle
        {
          Length = 5,
          Width = 2.3,
          MaxAcceleration = 9.81,
          MaxDeceleration = 12.3,
          MaxVelocity = 300,
          Type = VehicleHandoverLibrary.VehicleType.CAR
        };
      }
  }
}