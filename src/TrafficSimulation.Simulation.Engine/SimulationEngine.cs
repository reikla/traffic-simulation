using System.Timers;
using NLog;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.Exceptions;
using TrafficSimulation.Simulation.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  public class SimulationEngine : IEngine
  {
    private bool _isInitialized;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private DataModel _dataModel = new DataModel();
    private Timer SimulationTimer;

    public void Start()
    {
      CheckOrThrowInitialization("Start()");
      if (SimulationTimer != null)
      {
        throw new EngineStateException("Simulation already started.");
      }
      SimulationTimer = new Timer();
      Logger.Trace("Starting Simulation");
    }

    public void Stop()
    {
      CheckOrThrowInitialization("Stop()");
      if (SimulationTimer == null)
      {
        throw new EngineStateException("Simulation not running.");
      }
      Logger.Trace("Stopping Simulation");
    }

    public void Step()
    {
      CheckOrThrowInitialization("Step()");
      if (SimulationTimer != null)
      {
        throw new EngineStateException("Can not single step. Simulation is running");
      }
      Logger.Trace("Simulating a step.");
    }

    public void Init()
    {
      if (_isInitialized)
      {
        throw new EngineInitializationException("Engine is alread initialized.");
      }
      Logger.Trace("Init Simulation Engine");
      CreateNodes();

      _isInitialized = true;
    }

    private void CheckOrThrowInitialization(string method)
    {
      if (!_isInitialized)
      {
        throw new EngineInitializationException($"Can not execute {method}. Engine is not initialzied.");
      }
    }

    private void CreateNodes()
    {
      Logger.Trace("Creating Nodes");
      var startNode = new StartNode();
      var endNode = new EndNode();
      var node = new Node();

      var connection1 = new NodeConnection(startNode, node, 100);
      var connection2 = new NodeConnection(node, endNode, 100);

      _dataModel.Nodes.Add(startNode);
      _dataModel.Nodes.Add(node);
      _dataModel.Nodes.Add(endNode);

      _dataModel.NodeConnections.Add(connection1);
      _dataModel.NodeConnections.Add(connection2);
    }
  }
}