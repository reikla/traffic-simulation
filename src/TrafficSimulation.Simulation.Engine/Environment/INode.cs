using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a node in the simulation.
  /// </summary>
  public interface INode : ISimulationBase
  {
    /// <summary>
    /// X coordinate of a node
    /// </summary>
    double X { get; set; }
    /// <summary>
    /// Y coordinate of a node
    /// </summary>
    double Y { get; set; }

    /// <summary>
    /// The type of the node.
    /// </summary>
    NodeType NodeType { get; set; }

    /// <summary>
    /// Gets or sets the adjacent node connections.
    /// </summary>
    List<INodeConnection> NodeConnections { get; set; }

    /// <summary>
    /// Adds a node connection where this node is either start or end node.
    /// </summary>
    /// <param name="connection">The connection.</param>
    void AddNodeConnection(INodeConnection connection);
  }
}