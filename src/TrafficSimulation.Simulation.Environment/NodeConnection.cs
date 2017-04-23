using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Environment
{
  public class NodeConnection : INodeConnection
  {
    public INode StartNode { get; set; }
    public INode EndNode { get; set; }
    public double Length { get; set; }
  }
}