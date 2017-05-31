namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class NodeConnectionForSp
  {
    internal NodeForSP Target { get; private set; }
    internal double Distance { get; private set; }

    internal NodeConnectionForSp(NodeForSP target, double distance)
    {
      Target = target;
      Distance = distance;
    }
  }
}