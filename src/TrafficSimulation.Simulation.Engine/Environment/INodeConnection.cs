using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// A node connection connects two nodes. 
  /// </summary>
  public interface INodeConnection : ISimulationBase
  {
    List<IPlaceable> Placeables { get; set; }
      /// <summary>
    /// The start node of the connection.
    /// </summary>
    INode StartNode { get; set; }

    /// <summary>
    /// The end node of the connection.
    /// </summary>
    INode EndNode { get; set; }
    
    /// <summary>
    /// The length of the connection.
    /// </summary>
    double Length { get; }
  }
}