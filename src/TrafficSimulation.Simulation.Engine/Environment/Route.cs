using System;
using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Engine.SimulationObjects;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  public class Route : SimulationBase, IRoute
  {
    private readonly List<INodeConnection> _nodeConnections;
    private readonly List<IVehicle> _vehicles;

    public Route(params INodeConnection[] nodeConnections)
    {
      _nodeConnections = new List<INodeConnection>();
      _vehicles = new List<IVehicle>();
      foreach (var connection in nodeConnections)
      {
        _nodeConnections.Add(connection);
      }
    }
    public IReadOnlyList<INodeConnection> NodesConnections => _nodeConnections;
    public List<IVehicle> Vehicles => _vehicles;
    public double Legth => _nodeConnections.Sum(x => x.Length);
    public INodeConnection NextConnection(INodeConnection currentConnection)
    {
      for (var i = 0; i < _nodeConnections.Count; i++)
      {
        if (currentConnection == _nodeConnections[i] && i + 1 < _nodeConnections.Count)
        {
          return _nodeConnections[i + 1];
        }
      }
      return null;
    }

    public IDistance GetNextPlaceable(IPlaceable placable)
    {

      var position = placable.Position;

      if (!NodesConnections.Contains(position.NodeConnection))
      {
        throw new ArgumentException(String.Format(Strings.Exception_Placable_Not_On_Route, position));
      }

      var totalDistance = 0.0d;
      var startIndex = _nodeConnections.IndexOf(position.NodeConnection);
      for (int i = startIndex; i < _nodeConnections.Count; i++)
      {
        var connection = _nodeConnections[i];
        if (position.NodeConnection == connection) // we are on the same connection
        {
          var placablesFurtherOnConnection = connection.Placeables
            .Where(x => x.Position.PositionOnConnection > position.PositionOnConnection)
            .OrderBy(x => x.Position.PositionOnConnection);
          if (placablesFurtherOnConnection.Count() != 0) // we found a placable on same connection that is further away
          {
            var nextPlacable = placablesFurtherOnConnection.First();
            totalDistance = nextPlacable.Position.PositionOnConnection - position.PositionOnConnection;
            return new Distance() {DistanceInMeters = totalDistance, NextPlaceable = nextPlacable};
          }
          //we found no placable on this connection so we have to add the rest of the connection to the length
          totalDistance += position.NodeConnection.Length - position.PositionOnConnection;
        }
        else
        {
          var placablesFurtherOnConnection = connection.Placeables
            .OrderBy(x => x.Position.PositionOnConnection);
          if (placablesFurtherOnConnection.Count() != 0)
          {
            var nextPlacable = placablesFurtherOnConnection.First();
            totalDistance += nextPlacable.Position.PositionOnConnection;
            return new Distance() { DistanceInMeters = totalDistance, NextPlaceable = nextPlacable };
          }
          //we found no placable on this connection so we have to add this connection lenght
          totalDistance += connection.Length;
        }
      }
      //we found no placable
      return new Distance() { DistanceInMeters = double.PositiveInfinity, NextPlaceable = null };

    }

    public IVehicle CreateVehicle()
    {
      var position = new Position(_nodeConnections[0]);
      var vehicle = new Vehicle(VehicleType.Car, position, this);
      vehicle.Position.NodeConnection.Placeables.Add(vehicle);
      _vehicles.Add(vehicle);
      return vehicle;
    }

    public void DestoryVehicle(IVehicle vehicle)
    {
      vehicle.Position.NodeConnection.Placeables.Remove(vehicle);
      _vehicles.Remove(vehicle);
    }
  }
}