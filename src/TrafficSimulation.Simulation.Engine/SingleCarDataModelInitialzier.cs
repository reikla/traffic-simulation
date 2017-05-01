using NLog;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  internal class SingleCarDataModelInitialzier : IDataModelInitializer
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    public void Initialize(DataModel dataModel)
    {
      CreateNodes(dataModel);
      CreateRoutes(dataModel);
      CreateVehicle(dataModel);
    }

    private void CreateRoutes(DataModel dataModel)
    {
      Route route = new Route(dataModel.NodeConnections[0], dataModel.NodeConnections[1]);
      dataModel.Routes.Add(route);
    }

    private void CreateNodes(DataModel dataModel)
    {
      Logger.Trace("Creating Nodes");
      var startNode = new StartNode(0, 0.5);
      var endNode = new EndNode(1, 0.5);
      var node = new Node(0.5, 0.5);

      var connection1 = new NodeConnection(startNode, node);
      var connection2 = new NodeConnection(node, endNode);

      dataModel.Nodes.Add(startNode);
      dataModel.Nodes.Add(node);
      dataModel.Nodes.Add(endNode);

      dataModel.NodeConnections.Add(connection1);
      dataModel.NodeConnections.Add(connection2);
    }

    private void CreateVehicle(DataModel dataModel)
    {
      Logger.Trace("Creating Vehicles");

      var route = dataModel.Routes[0];

      var vehicle = route.CreateVehicle();
      dataModel.Vehicles.Add(vehicle);

    }
  }
}