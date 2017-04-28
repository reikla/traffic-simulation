using NLog;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Environment;
using TrafficSimulation.Simulation.SimulationObjects;

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
      var startNode = new StartNode();
      var endNode = new EndNode();
      var node = new Node();

      var connection1 = new NodeConnection(startNode, node, 100);
      var connection2 = new NodeConnection(node, endNode, 100000);

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
      var position = new Position(route.NodesConnections[0]);

      var vehicle = new Vehicle(VehicleType.Car, position, route);
      dataModel.Vehicles.Add(vehicle);

    }
  }
}