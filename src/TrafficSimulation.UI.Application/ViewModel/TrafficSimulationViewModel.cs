using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Shapes;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;
using TrafficSimulation.Simulation.WebService;
using System.Threading;

namespace TrafficSimulation.UI.Application.ViewModel
{
  /// <summary>
  /// Implements Prism.Mvvm.BindableBase, which is the implementation of System.ComponentModel.INotifyPropertyChanged to simplify models.
  /// </summary>
  class TrafficSimulationViewModel : BindableBase
  {
    static Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Holds the ISimulationService (overridden by the Bootstrapper)
    /// </summary>
    public ISimulationService SimulationService { get; set; }

    /// <summary>
    /// Contains the vehicles for the simulation (received via WCF)
    /// </summary>
    public List<Vehicle> Vehicles { get; set; }

    /// <summary>
    /// Contains the nodes for the simulation (received via WCF)
    /// </summary>
    public List<Node> Nodes { get; set; }

    /// <summary>
    /// Contains the NodeConnections for the simulation (received via WCF)
    /// </summary>
    public List<NodeConnection> NodeConnections { get; set; }

    /// <summary>
    /// Command to start the simulation.
    /// </summary>
    public DelegateCommand CmdStartSimulation { get; set; }

    /// <summary>
    /// Command to stop the simulation.
    /// </summary>
    public DelegateCommand CmdStopSimulation { get; set; }

    /// <summary>
    /// Command to step through the simulation.
    /// </summary>
    public DelegateCommand CmdStepSimulation { get; set; }

    /// <summary>
    /// Command to disconnect from/connect to the WCF-Server.
    /// </summary>
    public DelegateCommand CmdDisConnect { get; set; }

    /// <summary>
    /// Contains the ConstructionSides (rectangles) placed on the MainCanvas; also their position
    /// </summary>
    public List<KeyValuePair<Rectangle, Point>> ConstructionSides { get; set; }
   

    /// <summary>
    /// Contains the Timer which updates the viewmodel (overwritten by Bootstrapper)
    /// </summary>
    public System.Timers.Timer ServiceUpdateTimer;

    /// <summary>
    /// Contains the Timer which re-draws the view (overwritten by Bootstrapper)
    /// </summary>
    public System.Timers.Timer DrawTimer;
    /// <summary>
    /// A factory that creates channels of the type ISimulationService.
    /// </summary>
    public ChannelFactory<ISimulationService> cf;

    /// <summary>
    /// Constructor for the TrafficSimulationViewModel - initializes the Lists Vehicles, Nodes and NodeConnections
    /// </summary>
    public TrafficSimulationViewModel()
    {
      
      Vehicles = new List<Vehicle>();
      Nodes = new List<Node>();
      NodeConnections = new List<NodeConnection>();
      ConstructionSides = new List<KeyValuePair<Rectangle, Point>>();
      CmdStartSimulation = new DelegateCommand(StartSimulation);
      CmdStopSimulation = new DelegateCommand(StopSimulation);
      CmdStepSimulation = new DelegateCommand(StepSimulation);
      CmdDisConnect = new DelegateCommand(() => DisConnect());
      ServiceUpdateTimer = new System.Timers.Timer(Constants.SimulationUpdateSpeed);
      ServiceUpdateTimer.Elapsed += ServiceUpdateTimer_Elapsed;
     
    }

    /// <summary>
    /// Sets a car as defect.
    /// </summary>
    /// <param name="id">ID (Int32) of the Car.</param>
    public void SetCarDefect(int id)
    {
      SimulationService.SetCarDefect(id, true);
    }
    /// <summary>
    /// Sets a car as NOT defect.
    /// </summary>
    /// <param name="id">ID (Int32) of the Car.</param>
    public void UNsetCarDefect(int id)
    {
      SimulationService.SetCarDefect(id, false);
    }

    private void StopSimulation()
    {
      if (SimulationService.IsStarted())
      {
        SimulationService.Stop();
      }



    }

    private void StartSimulation()
    {
      if (!SimulationService.IsStarted())
      {
        SimulationService.Start();
      }

      if (!ServiceUpdateTimer.Enabled)
        {
          ServiceUpdateTimer.Start();
          if (!DrawTimer.Enabled)
          {
            DrawTimer.Start();
          }
        }
      


    }

    private void StepSimulation()
    {

      SimulationService.Step();
    }


    private void DisConnect()
    {
      try
      {
        if (((IClientChannel)SimulationService)?.State == CommunicationState.Opened)
        {
          DrawTimer.Stop();
          ServiceUpdateTimer.Stop();
          Thread.Sleep(100);
          ((IClientChannel)SimulationService).Close();
          

        }
        else
        {
          try
          {
            SimulationService = cf.CreateChannel();
            ((IClientChannel)SimulationService).Open();
          }
          catch (Exception e)
          {
            SimulationService = new SimulationService();
            SimulationService.Start();
            _logger.Error(e);
          }


        }

      }
      catch (Exception )
      {
        if (SimulationService.IsStarted())
        {
          DrawTimer.Stop();
          ServiceUpdateTimer.Stop();
          


        }
        else
        {
          try
          {
            SimulationService = cf.CreateChannel();
            ((IClientChannel)SimulationService).Open();

          }
          catch (Exception e)
          {
            SimulationService = new SimulationService();
            SimulationService.Start();
            _logger.Error(e);
          }


        }
      }





    }


    private void ServiceUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
    {

      lock (this)
      {
        Nodes.Clear();
        Nodes.AddRange(SimulationService.GetNodes());
        NodeConnections.Clear();
        NodeConnections.AddRange(SimulationService.GetNodeConnections());
        Vehicles.Clear();
        Vehicles.AddRange(SimulationService.GetVehicles());
      }


    }


    /// <summary>
    /// Stops update and draw timer.
    /// </summary>
    public void StopTimers()
    {
      if (ServiceUpdateTimer.Enabled)
      {
        DrawTimer.Stop();
        ServiceUpdateTimer.Stop();
      }
    }
  }
}

