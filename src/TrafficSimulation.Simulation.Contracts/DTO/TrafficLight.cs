using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  /// <summary>
  /// A traffic light.
  /// </summary>
  [DataContract]
  public partial class TrafficLight : DtoBase
  {
    /// <summary>
    /// c'tor
    /// </summary>
    /// <param name="id"></param>
    public TrafficLight(int id) : base(id)
    {
    }

    /// <summary>
    /// The State of the traffic light
    /// </summary>
    [DataMember]
    public TrafficLightState State { get; set; }

    /// <summary>
    /// The id of the node where the traffic light is.
    /// </summary>
    [DataMember]
    public int NodeId { get; set; }
    

    /// <summary>
    /// The id of the node connection that is controlled of the traffic light.
    /// </summary>
    [DataMember]
    public int ConnectionId { get; set; }
  }
}