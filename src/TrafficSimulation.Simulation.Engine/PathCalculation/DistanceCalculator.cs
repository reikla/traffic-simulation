using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class DistanceCalculator
  {
    internal IDictionary<string, double> CalculateDistances(Graph graph, string startingNode)
    {
      if (graph.Nodes.All(n => n.Key != startingNode))
      {
        throw new ArgumentException("Starting INode must be in graph.");
      }
      InitialiseGraph(graph, startingNode);
      ProcessGraph(graph, startingNode);
      return ExtractDistances(graph);

    }
    private void InitialiseGraph(Graph graph, string startingNode)
    {
      foreach (var node in graph.Nodes.Values)
      {
        node.DistanceFromStart = double.PositiveInfinity;
      }
      graph.Nodes[startingNode].DistanceFromStart = 0;
    }

    private void ProcessGraph(Graph graph, string startingNode)
    {
      var finished = false;
      var queue = graph.Nodes.Values.ToList();
      while (!finished)
      {
        var nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(
          n => !double.IsPositiveInfinity(n.DistanceFromStart));
        if (nextNode != null)
        {
          ProcessNode(nextNode, queue);
          queue.Remove(nextNode);
        }
        else
        {
          finished = true;
        }
      }
    }

    private void ProcessNode(NodeForSp node, List<NodeForSp> queue)
    {
      var connections = node.Connections.Where(c => queue.Contains(c.Target));
      foreach (var connection in connections)
      {
        var distance = node.DistanceFromStart + connection.Distance;
        if (distance < connection.Target.DistanceFromStart)
        {
          connection.Target.DistanceFromStart = distance;
        }
      }
    }

    private IDictionary<string, double> ExtractDistances(Graph graph)
    {
      return graph.Nodes.ToDictionary(n => n.Key, n => n.Value.DistanceFromStart);
    }
  }
}