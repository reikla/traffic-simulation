using System;
using System.Collections.Generic;
using TrafficSimulation.Common;

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
    public NodeConnection(INode startNode, INode endNode)
    {
      StartNode = startNode;
      EndNode = endNode;
      Placeables = new List<IPlaceable>();
    }

    public List<IPlaceable> Placeables { get; set; }

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
    public double Length
    {
      get
      {
        var dX = Math.Max(StartNode.X, EndNode.X) -  Math.Min(StartNode.X, EndNode.X);
        var dY = Math.Max(StartNode.Y, EndNode.Y) -  Math.Min(StartNode.Y, EndNode.Y);
        return Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2)) * Constants.SimulationSizeFactor;
      }
    }
  }
}