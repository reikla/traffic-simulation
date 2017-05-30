namespace TrafficSimulation.Simulation.Engine
{
  internal class NodeConnectionForSP
  {
    internal NodeForSP Target { get; private set; }
    internal double Distance { get; private set; }

    internal NodeConnectionForSP(NodeForSP target, double distance)
    {
      Target = target;
      Distance = distance;
    }
  }



}
