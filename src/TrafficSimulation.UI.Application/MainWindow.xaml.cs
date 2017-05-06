﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrafficSimulation.Simulation.Contracts.DTO;
using TrafficSimulation.UI.Application.ViewModel;


namespace TrafficSimulation.UI.Application
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private TrafficSimulationViewModel ViewModel => DataContext as TrafficSimulationViewModel;
  /// <summary>
  /// Constructor for the MainWindow view. Initializes all components.
  /// </summary>
  public MainWindow()
    {
      InitializeComponent();
    }

  

    Rectangle DrawVehicle(double width, double height)
    {
      return new Rectangle()
      {
        Width = width,
        Height = height,
        Fill = Brushes.Green,
        Stroke = Brushes.Green,
        StrokeThickness = 2
      };
    }

    Line DrawStreet(double xStart, double yStart, double xEnde, double yEnde)
    {
      return new Line()
      {
        X1 = xStart,
        Y1 = yStart,
        X2 = xEnde,
        Y2 = yEnde,
        Fill = Brushes.DimGray,
        Stroke = Brushes.DimGray,
        StrokeThickness = 2
        
      };
    }

    Rectangle DrawNode(double width, double height)
    {
      return new Rectangle()
      {
        Width = width,
        Height = height,
        Fill = Brushes.Black,
        Stroke = Brushes.Black,
        StrokeThickness = 2
      };
    }
    /// <summary>
    /// Places objects like vehicles, nodes and nodeconnections on to the canvas.
    /// </summary>
    public void Draw()
    {
      lock (ViewModel)
      {
        this.MainCanvas.Children.Clear();
        
        foreach (var node in ViewModel.Nodes)
        {
          var rectangle = DrawNode(5, 5);
          MainCanvas.Children.Add(rectangle);
          Canvas.SetLeft(rectangle, node.X * MainCanvas.ActualWidth);
          Canvas.SetTop(rectangle, node.Y * MainCanvas.ActualHeight - rectangle.Height/2);
        }

        foreach (var nodeconnection in ViewModel.NodeConnections)
        {
          Node startNode = ViewModel.Nodes.First(x => x.Id == nodeconnection.StartNodeId);
          Node endNode = ViewModel.Nodes.First(x => x.Id == nodeconnection.EndNodeId);
          var line = DrawStreet(startNode.X * MainCanvas.ActualWidth, startNode.Y * MainCanvas.ActualHeight, endNode.X * MainCanvas.ActualWidth, endNode.Y * MainCanvas.ActualHeight);
          MainCanvas.Children.Add(line);
        }

        foreach (var viewModelVehicle in ViewModel.Vehicles)
        {
          var rectangle = DrawVehicle(10, 10);
          NodeConnection street = ViewModel.NodeConnections.First(nc => nc.Id == viewModelVehicle.CurrentNodeConnectionId);
          Node startNode = ViewModel.Nodes.First(n => n.Id == street.StartNodeId);
          Node endNode = ViewModel.Nodes.First(n => n.Id == street.EndNodeId);

            MainCanvas.Children.Add(rectangle);
            var deltaX = endNode.X - startNode.X;
            var deltaY = endNode.Y - startNode.Y;
            var x = deltaX * (viewModelVehicle.PositionOnConnection / street.Length) + startNode.X;
            var y = deltaY * (viewModelVehicle.PositionOnConnection / street.Length) + startNode.Y;
            Canvas.SetLeft(rectangle, x * MainCanvas.ActualWidth);
            Canvas.SetTop(rectangle, y * MainCanvas.ActualHeight);

        }
      }
    }


  }
}