using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Simulation.Engine.Environment;
using TrafficSimulation.Simulation.Engine.PathCalculation;
using TrafficSimulation.Simulation.Engine.Xml;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// The DataModel initializer that uses the DrawML document
  /// </summary>
  class XmlDataModelInitializer : IDataModelInitializer
  {
    private DataModel dataModel;

    /// <inheritdoc />
    public void Initialize(DataModel dataModel)
    {
      this.dataModel = dataModel;
      XmlGraphReader reader = new XmlGraphReader();
      reader.Read("Strassennetz.graphml");

      dataModel.Nodes.AddRange(reader.Nodes);
      dataModel.NodeConnections.AddRange(reader.NodeConnections);

      IShortestPath shortestPath = new TobisShortestPath();


      foreach (var startNode in dataModel.Nodes.Where(x=>x.NodeType == NodeType.StartNode))
      {
        foreach (var endNode in dataModel.Nodes.Where(x=>x.NodeType == NodeType.EndNode))
        {
          dataModel.Routes.Add(shortestPath.GetRoute(dataModel.Nodes, dataModel.NodeConnections, startNode, endNode));
        }
      }


      //InitializeRoutes();

      var verticalDistance = dataModel.Routes.First().Legth;
      var horizontalDistance = dataModel.Routes.Last().Legth;
      var distanceBetweenTwoLanesSameDirection = GetConnectionByNodeIds(26, 30).Length;
      var distanceBetweenTwoLanesDifferentDirection = GetConnectionByNodeIds(42, 44).Length;



    }

    private void InitializeRoutes()
    {
      dataModel.Routes.Add(GetRouteByIds(18,26,30,34,38,42,44,50,54,56,19));
      dataModel.Routes.Add(GetRouteByIds(17,29,28,27,26,16));
    }

    private Route GetRouteByIds(params int[] ids)
    {
      List<INodeConnection> nodeConnections = new List<INodeConnection>();
      for (var i = 0; i < ids.Length - 1; i++)
      {
        nodeConnections.Add(GetConnectionByNodeIds(ids[i], ids[i+1]));
      }
      return new Route(nodeConnections.ToArray());
    }

    private INodeConnection GetConnectionByNodeIds(int startId, int endId)
    {
      return dataModel.NodeConnections.First(x => x.StartNode.Id == startId && x.EndNode.Id == endId);
    }
  }
}