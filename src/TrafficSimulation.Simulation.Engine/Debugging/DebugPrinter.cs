namespace TrafficSimulation.Simulation.Engine.Debugging
{
  /// <summary>
  /// Class used to print debug information
  /// </summary>
  internal static class DebugPrinter
  {
    public static string PrintDebug(IVehicle vehicle)
    {
      return $"Id:{vehicle.Id} v:{vehicle.CurrentVelocity:F2}m/s a:{vehicle.Acceleration:F2}m/s².";
    }
  }
}