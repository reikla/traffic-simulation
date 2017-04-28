namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a connections between two nodes.
  /// </summary>
  public class NodeConnection : SimulationBase, INodeConnection
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public NodeConnection(INode startNode, INode endNode, double length)
    {
      StartNode = startNode;
      EndNode = endNode;
      Length = length;
    }
    /// <summary>
    /// The start node
    /// </summary>
    public INode StartNode { get; set; }

    /// <summary>
    /// The end node
    /// </summary>
    public INode EndNode { get; set; }

    /// <summary>
    /// The length of the connection in m
    /// </summary>
    public double Length { get; set; }
  }
}