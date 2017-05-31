namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class NodeConnectionForSp
  {
    internal NodeForSp Target { get; private set; }
    internal double Distance { get; private set; }

    internal NodeConnectionForSp(NodeForSp target, double distance)
    {
      Target = target;
      Distance = distance;
    }
  }
}