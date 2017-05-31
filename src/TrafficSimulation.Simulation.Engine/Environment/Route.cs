using System;
using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Engine.SimulationObjects;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a route in the simulation. A route cant be modified once created
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.SimulationBase" />
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.IRoute" />
  public class Route : SimulationBase, IRoute
  {
    private readonly List<INodeConnection> _nodeConnections;
    private readonly List<IVehicle> _vehicles;

    /// <summary>
    /// Initializes a new instance of the <see cref="Route"/> class.
    /// </summary>
    /// <param name="nodeConnections">The node connections.</param>
    public Route(params INodeConnection[] nodeConnections)
    {
      _nodeConnections = new List<INodeConnection>();
      _vehicles = new List<IVehicle>();
      foreach (var connection in nodeConnections)
      {
        _nodeConnections.Add(connection);
      }
    }
    /// <summary>
    /// The list of nodes the route contains of.
    /// </summary>
    public IReadOnlyList<INodeConnection> NodesConnections => _nodeConnections;
    /// <summary>
    /// A list of all vehicles existing on this route.
    /// </summary>
    public List<IVehicle> Vehicles => _vehicles;
    /// <summary>
    /// the length of the route.
    /// </summary>
    public double Legth => _nodeConnections.Sum(x => x.Length);
    
    /// <summary>
    /// Returns the next node on this connection
    /// </summary>
    /// <param name="currentConnection"></param>
    /// <returns>
    /// null if that was the last node
    /// </returns>
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

    /// <summary>
    /// Gets the next placeable.
    /// </summary>
    /// <param name="placable">The placable.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException"></exception>
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

    /// <summary>
    /// Creates a new vehicle
    /// </summary>
    /// <returns>
    /// The newly created vehicle
    /// </returns>
    public virtual IVehicle CreateVehicle()
    {
      var vehicle = new Vehicle(VehicleType.Car, this);
      vehicle.Position.NodeConnection.Placeables.Add(vehicle);
      _vehicles.Add(vehicle);
      return vehicle;
    }

    /// <summary>
    /// Destroys a vehicle
    /// </summary>
    /// <param name="vehicle">The vehicle to destroy</param>
    public virtual void DestoryVehicle(IVehicle vehicle)
    {
      vehicle.Position.NodeConnection.Placeables.Remove(vehicle);
      _vehicles.Remove(vehicle);
    }
  }
}