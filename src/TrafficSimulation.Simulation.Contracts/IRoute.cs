using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// Represents a route in the simulation. A route cant be modified once created
  /// </summary>
  public interface IRoute
  {
    /// <summary>
    /// The list of nodes the route contains of.
    /// </summary>
    IReadOnlyList<INodeConnection> NodesConnections { get;}
    
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
  }
}