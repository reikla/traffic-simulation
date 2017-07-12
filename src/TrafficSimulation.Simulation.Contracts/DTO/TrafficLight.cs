using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  /// <summary>
  /// A traffic light.
  /// </summary>
  [DataContract]
  public class TrafficLight : DtoBase
  {
    /// <summary>
    /// c'tor
    /// </summary>
    /// <param name="id">the id</param>
    /// <param name="state">the traffic light state</param>
    /// <param name="connectionId">the connection where the traffic light is placed</param>
    /// <param name="positionOnConnection">the position on the connection</param>
    public TrafficLight(int id, TrafficLightState state, int connectionId, double positionOnConnection) : base(id)
    {
      State = state;
      ConnectionId = connectionId;
      PositionOnConnection = positionOnConnection;
    }

    /// <summary>
    /// Gets or sets the position on connection.
    /// </summary>
    public double PositionOnConnection { get; set; }

    /// <summary>
    /// The State of the traffic light
    /// </summary>
    [DataMember]
    public TrafficLightState State { get; set; }

    /// <summary>
    /// The id of the node connection that is controlled of the traffic light.
    /// </summary>
    [DataMember]
    public int ConnectionId { get; set; }
  }
}