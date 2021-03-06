﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Timers;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;
using System.Threading;
using TrafficSimulation.TrafficLightControl.Contracts;

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
    /// Reference to the traffic light service
    /// </summary>
    public ITrafficLightService TrafficLightService { get; set; }

    /// <summary>
    /// Contains the vehicles for the simulation (received via WCF)
    /// </summary>
    public List<Vehicle> Vehicles { get; set; }

    /// <summary>
    /// Contains the nodes for the simulation (received via WCF)
    /// </summary>
    public List<Node> Nodes { get; set; }

    /// <summary>
    /// Contains the traffic lights for the simulation (received via WCF)
    /// </summary>
    public List<TrafficLight> TrafficLights { get; set; }

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
    public ChannelFactory<ISimulationService> SimulationChannelFactory { get; set; }
    /// <summary>
    /// A factory that creates channels of the type ITrafficLightService.
    /// </summary>
    public ChannelFactory<ITrafficLightService> TrafficLightChannelFactory { get; set; }

    /// <summary>
    /// Constructor for the TrafficSimulationViewModel - initializes the Lists Vehicles, Nodes and NodeConnections
    /// </summary>
    public TrafficSimulationViewModel()
    {
      Vehicles = new List<Vehicle>();
      Nodes = new List<Node>();
      NodeConnections = new List<NodeConnection>();
      TrafficLights = new List<TrafficLight>();
      CmdStartSimulation = new DelegateCommand(StartSimulation);
      CmdStopSimulation = new DelegateCommand(StopSimulation);
      CmdStepSimulation = new DelegateCommand(StepSimulation);
      CmdDisConnect = new DelegateCommand(ToggleConnection);
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

    /// <summary>
    /// Toggles Traffic Lights between there states
    /// </summary>
    /// <param name="id">ID (Int32) of the Traffic Light.</param>
    public void ToggleTrafficLight(int id)
    {
      SimulationService.ToggleTrafficLight(id);
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
        TrafficLightService.Start();
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
      TrafficLightService.Step();
    }


    private void ToggleConnection()
    {
      try
      {
        if (((IClientChannel)SimulationService)?.State == CommunicationState.Opened)
        {
          DrawTimer.Stop();
          ServiceUpdateTimer.Stop();
          Thread.Sleep(100);
          ((IClientChannel)SimulationService).Close();
          ((IClientChannel)TrafficLightService).Close();


        }
        else
        {
          SimulationService = SimulationChannelFactory.CreateChannel();
          TrafficLightService = TrafficLightChannelFactory.CreateChannel();
          ((IClientChannel)SimulationService).Open();
          ((IClientChannel)TrafficLightService).Open();
          StartSimulation();
        }

      }
      catch (Exception)
      {
        if (SimulationService.IsStarted())
        {
          DrawTimer.Stop();
          ServiceUpdateTimer.Stop();
        }
        else
        {
            SimulationService = SimulationChannelFactory.CreateChannel();
            TrafficLightService = TrafficLightChannelFactory.CreateChannel();
            ((IClientChannel)SimulationService).Open();
            ((IClientChannel)TrafficLightService).Open();
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
        TrafficLights.Clear();
        TrafficLights.AddRange(SimulationService.GetTrafficLights());
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

