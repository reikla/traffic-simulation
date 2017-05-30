using System;
using System.Collections.Generic;
using System.Diagnostics;
using TrafficSimulation.Common;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a connections between two nodes.
  /// </summary>
  [DebuggerDisplay("NC {StartNode.Id} - {EndNode.Id}")]
  public class NodeConnection : SimulationBase, INodeConnection
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public NodeConnection(INode startNode, INode endNode)
    {
      StartNode = startNode;
      EndNode = endNode;
      StartNode.AddNodeConnection(this);
      EndNode.AddNodeConnection(this);
      Placeables = new List<IPlaceable>();
    }

    /// <summary>
    /// List of placebles currently placed on this connection
    /// </summary>
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
        var dX = Math.Max(StartNode.X, EndNode.X) - Math.Min(StartNode.X, EndNode.X);
        var dY = Math.Max(StartNode.Y, EndNode.Y) - Math.Min(StartNode.Y, EndNode.Y);
        return Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2)) * Constants.SimulationSizeFactor;
      }
    }

    /// <summary>
    /// Gets the orientation of a connection.
    /// </summary>
    public Orientation ConnectionOrientation
    {
      get
      {
        var dX = Math.Max(StartNode.X, EndNode.X) - Math.Min(StartNode.X, EndNode.X);
        if (dX < 0.01)
        {
          return StartNode.Y < EndNode.Y ? Orientation.South : Orientation.North;
        }
        return StartNode.X < EndNode.X ? Orientation.East : Orientation.West;
      }
    }

    /// <summary>
    /// Returns true if a connection is parallel to this connection
    /// </summary>
    /// <param name="connection">the connection to compare</param>
    public bool IsParallelTo(INodeConnection connection)
    {
      return connection.ConnectionOrientation == ConnectionOrientation;
    }

    /// <summary>
    /// Gets the distance to a parallel connection.
    /// </summary>
    /// <param name="connection">The connection.</param>
    /// <returns></returns>
    public double GetParallelDistance(INodeConnection connection)
    {
      if (!IsParallelTo(connection))
      {
        return double.MaxValue;
      }
      if (ConnectionOrientation == Orientation.North || ConnectionOrientation == Orientation.South)
      {
        return Math.Abs(StartNode.X - connection.StartNode.X) * Constants.SimulationSizeFactor;
      }
      return Math.Abs(StartNode.Y - connection.StartNode.Y) * Constants.SimulationSizeFactor;
    }
  }
}