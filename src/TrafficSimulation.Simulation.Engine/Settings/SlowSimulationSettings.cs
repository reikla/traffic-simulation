namespace TrafficSimulation.Simulation.Engine.Settings
{
  public class SlowSimulationSettings : SimulationSettings
  {
    public SlowSimulationSettings()
    {
      TickRate = 250;
      TickStepSize = 1;
      TargetVehicleCount = 5;
    }
  }
}