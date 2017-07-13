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
    /// Gets or sets the debug information.
    /// </summary>
    [DataMember]
    public string DebugInfo { get; set; }

    /// <summary>
    /// Signals if we have a foreign car here.
    /// </summary>
    [DataMember]
    public bool IsForeignCar { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public Vehicle(int id, VehicleType type, int currentNodeConnectionId, double positionOnConnection,bool isForeignVehicle, string debugInfo = null) : base(id)
    {
      Type = type;
      CurrentNodeConnectionId = currentNodeConnectionId;
      PositionOnConnection = positionOnConnection;
      DebugInfo = debugInfo;
      IsForeignCar = isForeignVehicle;
    }
  }
}