using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  /// <summary>
  /// A Connection of two nodes
  /// </summary>
  [DataContract]
  public class NodeConnection : DtoBase
  {
    /// <summary>
    /// Id of the start node
    /// </summary>
    [DataMember]
    public int StartNodeId { get; set; }

    /// <summary>
    /// Id of the end node
    /// </summary>
    [DataMember]
    public int EndNodeId { get; set; }

    /// <summary>
    /// The length of the connection
    /// </summary>
    [DataMember]
    public double Length { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="startNodeId">Id of start node</param>
    /// <param name="endNodeId">Id of end node</param>
    /// <param name="length">Length of connection</param>
    public NodeConnection(int id, int startNodeId, int endNodeId, double length) : base(id)
    {
      StartNodeId = startNodeId;
      EndNodeId = endNodeId;
      Length = length;
    }
  }
}