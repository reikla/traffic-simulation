using System.Runtime.Serialization;

namespace TrafficSimulation.Simulation.Contracts.DTO
{
  /// <summary>
  /// Base Class for Data Transfer
  /// </summary>
  [DataContract]
  public class DtoBase
  {

    /// <summary>
    /// Identifier
    /// </summary>
    [DataMember]
    public int Id { get; set; }

    /// <summary>
    /// Normal Constructor
    /// </summary>
    /// <param name="id"></param>
    public DtoBase(int id)
    {
      Id = id;
    }

 
    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      if (!(obj is DtoBase))
      {
        return false;
      }
      return this.ToString() == obj.ToString();
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return ToString().GetHashCode();
    }
  }
}