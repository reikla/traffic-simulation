using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSimulation.Simulation.Engine;
using TrafficSimulation.Simulation.Contracts.DTO;
using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Tests
{
  [TestClass]
  public class ShortestPathTests
  {
    [TestMethod]
    public void OutputShortestPath()
    {
      List<Node> AllNodes = new List<Node>();
      List<NodeConnection> AllNodeConnections = new List<NodeConnection>();
      AllNodes.Add(new Node(1));
      AllNodes.Add(new Node(2));
      AllNodes.Add(new Node(3));
      AllNodes.Add(new Node(4));
      AllNodes.Add(new Node(5));

      AllNodeConnections.Add(new NodeConnection(1, 1, 2, 10d));
      AllNodeConnections.Add(new NodeConnection(2, 1, 3, 30d));
      AllNodeConnections.Add(new NodeConnection(3, 2, 4, 3d));
      AllNodeConnections.Add(new NodeConnection(4, 3, 5, 5d));
      AllNodeConnections.Add(new NodeConnection(5, 5, 4, 30d));

      int StartNodeId = 1;
      int EndNodeId = 4;


      var sp = new ShortestPath(AllNodeConnections,AllNodes,StartNodeId,EndNodeId);
    }
  }
}
