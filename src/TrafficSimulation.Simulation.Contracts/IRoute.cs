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
  }
}