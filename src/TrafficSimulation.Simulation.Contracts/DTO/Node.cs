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
    /// Standard Constructor
    /// </summary>
    public Node(int id) : base(id)
    {
    }
  }
}