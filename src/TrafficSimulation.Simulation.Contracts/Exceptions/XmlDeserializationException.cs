using System;

namespace TrafficSimulation.Simulation.Contracts.Exceptions
{
  /// <summary>
  /// Exceptions for signaling there is a problem with the Xml document where the streets graph ist stored.
  /// </summary>
  [Serializable]
  public class XmlDeserializationException : SimulationException
  {
    /// <summary>
    /// c'tor
    /// </summary>
    public XmlDeserializationException() : base() { }

    /// <summary>
    /// c'tor
    /// </summary>
    public XmlDeserializationException(string message) : base(message) { }
    /// <summary>
    /// c'tor
    /// </summary>
    public XmlDeserializationException(string message, System.Exception inner) : base(message, inner) { }


    /// <summary>
    ///A constructor is needed for serialization when an
    ///exception propagates from a remoting server to the client.
    /// </summary>
    protected XmlDeserializationException(System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
    { }
  }
}