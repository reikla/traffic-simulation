using System;

namespace TrafficSimulation.Simulation.Contracts.Exceptions
{
  /// <summary>
  /// Exceptions for signaling there is a problem with the initialization of the enine
  /// </summary>
  [Serializable]
  public class EngineInitializationException : SimulationException
  {
    /// <summary>
    /// c'tor
    /// </summary>
    public EngineInitializationException() : base() { }

    /// <summary>
    /// c'tor
    /// </summary>
    public EngineInitializationException(string message) : base(message) { }
    /// <summary>
    /// c'tor
    /// </summary>
    public EngineInitializationException(string message, System.Exception inner) : base(message, inner) { }


    /// <summary>
    ///A constructor is needed for serialization when an
    ///exception propagates from a remoting server to the client.
    /// </summary>
    protected EngineInitializationException(System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
    { }
  }
}