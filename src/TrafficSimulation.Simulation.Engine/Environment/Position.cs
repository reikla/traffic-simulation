namespace TrafficSimulation.Simulation.Engine.Environment
{
  public class Position : IPosition
  {
    public Position(INodeConnection connection)
    {
      NodeConnection = connection;
      PositionOnConnection = 0;
    }
    public INodeConnection NodeConnection { get; set; }
    public double PositionOnConnection { get; set; }
  }
}