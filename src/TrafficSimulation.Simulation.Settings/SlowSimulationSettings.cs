namespace TrafficSimulation.Simulation.Settings
{
  public class SlowSimulationSettings : SimulationSettings
  {
    public SlowSimulationSettings()
    {
      TickRate = 10;
      TickStepSize = 100;
    }
  }
}