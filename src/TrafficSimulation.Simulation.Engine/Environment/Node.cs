using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// A Node in the Simulation is a point that is connected with other points with <see cref="INodeConnection"/>
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.SimulationBase" />
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.INode" />
  public class Node : SimulationBase, INode
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    /// <param name="x">The x coordinate</param>
    /// <param name="y">The y coordinate</param>
    /// <param name="type">The type of the node. Optional Parameter/></param>
    public Node(double x, double y, NodeType type = NodeType.Standard)
    {
      X = x;
      Y = y;
      NodeType = type;
      NodeConnections = new List<INodeConnection>();
    }

    /// <summary>
    /// X coordinate of a node
    /// </summary>
    public double X { get; set; }
    /// <summary>
    /// Y coordinate of a node
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// The type of the node.
    /// </summary>
    public NodeType NodeType { get; set; }

    /// <summary>
    /// Gets or sets the adjacent node connections.
    /// </summary>
    public List<INodeConnection> NodeConnections { get; set; }

    /// <summary>
    /// Adds a node connection where this node is either start or end node.
    /// </summary>
    /// <param name="connection">The connection.</param>
    public void AddNodeConnection(INodeConnection connection)
    {
      if (!NodeConnections.Contains(connection))
      {
        NodeConnections.Add(connection);
      }
    }
  }
}
