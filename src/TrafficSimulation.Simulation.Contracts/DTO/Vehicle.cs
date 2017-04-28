using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  /// <summary>
  /// A Vehicle
  /// </summary>
  [DataContract]
  public class Vehicle : DtoBase
  {
    /// <summary>
    /// The type of the vehicle
    /// </summary>
    [DataMember]
    public VehicleType Type { get; set; }

    /// <summary>
    /// The node connection where the Vehilce is currently placed on
    /// </summary>
    [DataMember]
    public int CurrentNodeConnectionId { get; set; }

    /// <summary>
    /// The position on this connection in meters.
    /// </summary>
    [DataMember]
    public double PositionOnConnection { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public Vehicle(int id, VehicleType type, int currentNodeConnectionId, double positionOnConnection) : base(id)
    {
      Type = type;
      CurrentNodeConnectionId = currentNodeConnectionId;
      PositionOnConnection = positionOnConnection;
    }
  }
}