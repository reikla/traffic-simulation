﻿using System.Linq;
using TrafficSimulation.Simulation.Engine.Environment;
using TrafficSimulation.Simulation.Engine.PathCalculation;
using TrafficSimulation.Simulation.Engine.Xml;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// The DataModel initializer that uses the DrawML document
  /// </summary>
  internal class XmlDataModelInitializer : IDataModelInitializer
  {
    /// <inheritdoc />
    public void Initialize(DataModel dataModel)
    {
      var reader = new XmlGraphReader();
      reader.Read("Strassennetz.graphml");

      dataModel.Nodes.AddRange(reader.Nodes);
      dataModel.NodeConnections.AddRange(reader.NodeConnections);
      dataModel.TrafficLights.AddRange(reader.TrafficLights);

      CalculateRoutes(dataModel);
    }

    private void CalculateRoutes(DataModel dataModel)
    {
      IShortestPath shortestPath = new QuickShortestPath();

      foreach (var startNode in dataModel.Nodes.Where(x => x.NodeType == NodeType.StartNode))
      {
        foreach (var endNode in dataModel.Nodes.Where(x => x.NodeType == NodeType.EndNode))
        {
          dataModel.Routes.Add(shortestPath.GetRoute(dataModel.Nodes, dataModel.NodeConnections, startNode, endNode));
        }
      }
    }
  }
}