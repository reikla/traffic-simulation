using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Prism.Commands;
using Prism.Mvvm;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.UI.Application.ViewModel
{
  /// <summary>
  /// Implements Prism.Mvvm.BindableBase, which is the implementation of System.ComponentModel.INotifyPropertyChanged to simplify models.
  /// </summary>
  class TrafficSimulationViewModel : BindableBase
  {
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
    public List<KeyValuePair<Rectangle,Point>> ConstructionSides { get; set; }

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
      CmdDisConnect = new DelegateCommand(DisConnect);


    }

   private void StopSimulation()
    {

      SimulationService.Stop();
     
     
    }

    private void StartSimulation()
    {

      SimulationService.Start();
    }

    private void StepSimulation()
    {

      SimulationService.Step();
    }


    private void DisConnect()
    {
      if (((IClientChannel) SimulationService).State == CommunicationState.Opened)
      {
        ((IClientChannel) SimulationService).Close();
        MessageBox.Show("DISCONNECTING...");
      }
      else
      {

        MessageBox.Show("ALREADY CONNECTED");
      }
    }



  }
}
