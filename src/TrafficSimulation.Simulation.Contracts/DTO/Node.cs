using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  /// <summary>
  /// Node for DataTransfer
  /// </summary>
  [DataContract]
  public class Node : DtoBase
  {
    /// <summary>
    /// The x coordinate of a node
    /// </summary>
    [DataMember]
    public double X { get; set; }

    /// <summary>
    /// The y coordinate of a node.
    /// </summary>
    [DataMember]
    public double Y { get; set; }

    /// <summary>
    /// Standard Constructor
    /// </summary>
    public Node(int id) : base(id)
    {
    }

  }
}