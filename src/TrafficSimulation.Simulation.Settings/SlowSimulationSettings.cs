namespace TrafficSimulation.Simulation.Settings
{
  public class SlowSimulationSettings : SimulationSettings
  {
    public SlowSimulationSettings()
    {
      TickRate = 250;
      TickStepSize = 1;
    }
  }
}