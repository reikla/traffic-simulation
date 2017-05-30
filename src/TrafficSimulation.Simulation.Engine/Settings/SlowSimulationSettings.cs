namespace TrafficSimulation.Simulation.Engine.Settings
{
  /// <summary>
  /// Quite slow simulation settings for debugging
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Settings.SimulationSettings" />
  public class SlowSimulationSettings : SimulationSettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SlowSimulationSettings"/> class.
    /// </summary>
    public SlowSimulationSettings()
    {
      TickRate = 100;
      TickStepSize = 0.1;
      TargetVehicleCount = 500;
    }
  }
}