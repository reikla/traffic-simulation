using NLog;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.SimulationObjects
{
  public static class Placer
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    public static void Place(IVehicle vehicle, IRoute route, double deltaS)
    {
      var pos = vehicle.Position;
      if (vehicle.Position.PositionOnConnection + deltaS > pos.NodeConnection.Length)
      {
        var rest = vehicle.Position.NodeConnection.Length - vehicle.Position.PositionOnConnection;
        var nextNode = route.NextConnection(vehicle.Position.NodeConnection);

        Logger.Debug($"Vehicle {vehicle} has reached end of connection. Place it on the next connection. {rest}m left");
        //we move until the begin of the next segment
        if (nextNode == null)
        {
          Logger.Debug($"Vehicle {vehicle} has reaced end of route. Destroying vehicle.");
          //we have reached the end of our route
          var endNode = vehicle.Position.NodeConnection.EndNode as IEndNode;
          endNode.DestroyVehicle(vehicle);
          return;
        }
        vehicle.Position.NodeConnection = nextNode;
        vehicle.Position.PositionOnConnection = 0;
        Place(vehicle, route, rest);
      }
      else
      {
        Logger.Debug($"Moving vehicle {vehicle} on connection {vehicle.Position.NodeConnection} for {deltaS}m.");
        vehicle.Position.PositionOnConnection += deltaS;
      }
    }
  }
}