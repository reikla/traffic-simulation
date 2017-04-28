using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  [DataContract]
  public class Vehicle : DtoBase
  {
    [DataMember]
    public VehicleType Type { get; set; }

    [DataMember]
    public int CurrentNodeConnectionId { get; set; }

    [DataMember]
    public double PositionOnConnection { get; set; }

    public Vehicle(int id, VehicleType type, int currentNodeConnectionId, double positionOnConnection) : base(id)
    {
      Type = type;
      CurrentNodeConnectionId = currentNodeConnectionId;
      PositionOnConnection = positionOnConnection;
    }
  }
}