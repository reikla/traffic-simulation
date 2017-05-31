using System;
using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class NodeForSp
  {
    private readonly IList<NodeConnectionForSp> _connections;

    internal string Name { get; private set; }

    internal double DistanceFromStart { get; set; }

    internal IEnumerable<NodeConnectionForSp> Connections => _connections;

    internal NodeForSp(string name)
    {
      Name = name;
      _connections = new List<NodeConnectionForSp>();
    }

    internal void AddConnection(NodeForSp targetNode, double distance)
    {
      if (targetNode == null)
      {
        throw new ArgumentNullException(nameof(targetNode));
      }
      if (targetNode == this)
      {
        throw new ArgumentException("INode may not connect to itself.");
      }
      if (distance <= 0)
      {
        throw new ArgumentException("Distance must be positive.");
      }

      _connections.Add(new NodeConnectionForSp(targetNode, distance));
    }
  }
}