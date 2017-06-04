using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a route in the simulation. A route cant be modified once created
  /// </summary>
  public interface IRoute : IVehicleLifetimeManager
  {
    /// <summary>
    /// The list of nodes the route contains of.
    /// </summary>
    IReadOnlyList<INodeConnection> NodesConnections { get;}
    /// <summary>
    /// A list of all vehicles existing on this route. 
    /// </summary>
    List<IVehicle> Vehicles { get; }
    
    /// <summary>
    /// the length of the route.
    /// </summary>
    double Legth { get; }

    /// <summary>
    /// Returns the next node on this connection
    /// </summary>
    /// <param name="currentConnection"></param>
    /// <returns>null if that was the last node</returns>
    INodeConnection NextConnection(INodeConnection currentConnection);

    /// <summary>
    /// Gets the next Placable on the connection and the distance 
    /// </summary>
    /// <param name="position">The placable we are looking ahead of.</param>
    IDistance GetNextPlaceable(IPlaceable position);

    
    /// <summary>
    /// Gets the next Placeable on the connection of a given type.
    /// </summary>
    /// <param name="position"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IDistance GetNextPlaceable<T>(IPlaceable position) where T : IPlaceable;
  }
}