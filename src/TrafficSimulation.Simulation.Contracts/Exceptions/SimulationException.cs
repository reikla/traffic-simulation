using System;

namespace TrafficSimulation.Simulation.Contracts.Exceptions
{
  /// <summary>
  /// Base Excpetion for the Simulation
  /// </summary>
  [Serializable]
  public class SimulationException : Exception
  {
    /// <summary>
    /// c'tor
    /// </summary>
    public SimulationException() : base() { }

    /// <summary>
    /// c'tor
    /// </summary>
    public SimulationException(string message) : base(message) { }
    /// <summary>
    /// c'tor
    /// </summary>
    public SimulationException(string message, System.Exception inner) : base(message, inner) { }

    /// <summary>
    ///A constructor is needed for serialization when an
    ///exception propagates from a remoting server to the client.
    /// </summary>
    protected SimulationException(System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
    { }
  }
}