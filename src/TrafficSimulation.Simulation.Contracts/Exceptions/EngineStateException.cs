using System;

namespace TrafficSimulation.Simulation.Contracts.Exceptions
{
  /// <summary>
  /// Base Excpetion for the Simulation
  /// </summary>
  [Serializable]
  public class EngineStateException : SimulationException
  {
    /// <summary>
    /// c'tor
    /// </summary>
    public EngineStateException() : base() { }

    /// <summary>
    /// c'tor
    /// </summary>
    public EngineStateException(string message) : base(message) { }
    /// <summary>
    /// c'tor
    /// </summary>
    public EngineStateException(string message, System.Exception inner) : base(message, inner) { }

    /// <summary>
    ///A constructor is needed for serialization when an
    ///exception propagates from a remoting server to the client.
    /// </summary>
    protected EngineStateException(System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
    { }
  }
}