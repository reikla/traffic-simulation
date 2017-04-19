namespace TrafficSimulation.Simulation.Contracts
{
  public class TrafficLight : ISimulationObject
  {
    public Position Position { get; set; }

    public void Tick(double time)
    {
      throw new System.NotImplementedException();
    }
  }
}