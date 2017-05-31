using System;
using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class NodeForSP
  {
    IList<NodeConnectionForSp> _connections;

    internal string Name { get; private set; }

    internal double DistanceFromStart { get; set; }

    internal IEnumerable<NodeConnectionForSp> Connections
    {
      get { return _connections; }
    }

    internal NodeForSP(string name)
    {
      Name = name;
      _connections = new List<NodeConnectionForSp>();
    }

    internal void AddConnection(NodeForSP targetNode, double distance, bool twoWay)
    {
      if (targetNode == null)
      {
        throw new ArgumentNullException("targetNode");
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
      if (twoWay)
      {
        targetNode.AddConnection(this, distance, false);
      }
    }
  }
}