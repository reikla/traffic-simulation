namespace TrafficSimulation.Simulation.Contracts
{
  public class Edge
  {
    public Node StartNode { get; set; }
    public Node EndNode { get; set; }
    public double Length { get; set; }
  }
}