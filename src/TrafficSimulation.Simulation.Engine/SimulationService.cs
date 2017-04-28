﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.Simulation.Engine
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  public class SimulationService : ISimulationService
  {
    private readonly SimulationEngine _engine;
    public SimulationService()
    {
      _engine = new SimulationEngine();
      _engine.Init();
    }


    public void Start()
    {
      _engine.Start();
    }

    public void Stop()
    {
      _engine.Stop();
    }

    public void Step()
    {
      _engine.Step();
    }

    public ReadOnlyCollection<Node> GetNodes()
    {
      var nodes = new List<Node>();
      _engine.DataModel.Nodes.ForEach(x=>nodes.Add(new Node(x.Id)));
      return new ReadOnlyCollection<Node>(nodes);
    }

    public List<Vehicle> GetVehicles()
    {
      var vehicles = new List<Vehicle>();
      _engine.DataModel.Vehicles.ForEach(x=>vehicles.Add(new Vehicle(x.Id, x.VehicleType, x.Position.NodeConnection.Id, x.Position.PositionOnConnection)));
      return vehicles;
    }

    public ReadOnlyCollection<NodeConnection> GetNodeConnections()
    {
      var connections = new List<NodeConnection>();
      _engine.DataModel.NodeConnections.ForEach(
        x => connections.Add(new NodeConnection(x.Id, x.StartNode.Id, x.EndNode.Id, x.Length)));
      return new ReadOnlyCollection<NodeConnection>(connections);
    }
  }
}