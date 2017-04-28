namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// Represents a base item of the simulation. This is used to exactly identify each object.
  /// </summary>
  public class SimulationBase : ISimulationBase
  {
    private static int _id = 0;
    /// <summary>
    /// The id of the Object
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Constructor where the Id gets set automatically
    /// </summary>
    public SimulationBase()
    {
      this.Id = _id++;
    }

    /// <summary>
    /// Constructor to set the id manually.
    /// </summary>
    /// <param name="id"></param>
    public SimulationBase(int id)
    {
      this.Id = id;
    }

    /// <summary>
    /// ToString Implementation
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"{GetType().Name}-{Id}";
    }

    /// <summary>
    /// Equals Implementation
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      if (!(obj is SimulationBase))
      {
        return false;
      }
      var that = obj as SimulationBase;
      return that.ToString() == this.ToString();
    }
    /// <summary>
    /// GetHashCode impelementation
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return ToString().GetHashCode();
    }
  }
}