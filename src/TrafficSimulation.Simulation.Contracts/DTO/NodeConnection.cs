using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  [DataContract]
  public class NodeConnection : DtoBase
  {
    [DataMember]
    public int StartNodeId { get; set; }

    [DataMember]
    public int EndNodeId { get; set; }

    [DataMember]
    public double Length { get; set; }

    public NodeConnection(int id, int startNodeId, int endNodeId, double length) : base(id)
    {
      StartNodeId = startNodeId;
      EndNodeId = endNodeId;
      Length = length;
    }
  }
}