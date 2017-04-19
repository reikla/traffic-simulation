using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Contracts
{
  public class Vehicle : ISimulationObject
  {
    public VehicleType VehicleType { get; set; }
    public List<Node> Route { get; set; }

    public void Tick(double time)
    {
      throw new System.NotImplementedException();
    }
  }
}