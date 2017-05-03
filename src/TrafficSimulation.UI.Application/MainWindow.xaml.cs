using System;
using System.Collections.Generic;
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
    private TrafficSimulationViewModel ViewModel
    {
      get { return DataContext as TrafficSimulationViewModel; }
    }

    /// <summary>
    /// Constructor for the MainWindow-View. Initializes all components.
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
          NodeConnection street = ViewModel.NodeConnections.First(x => x.Id == viewModelVehicle.CurrentNodeConnectionId);
          Node startNode = ViewModel.Nodes.First(x => x.Id == street.StartNodeId);
          Node endNode = ViewModel.Nodes.First(x => x.Id == street.EndNodeId);

          MainCanvas.Children.Add(rectangle);

          //double a = startNode.X * MainCanvas.ActualWidth;
          //double b = startNode.Y * MainCanvas.ActualHeight;
          //double c = endNode.X * MainCanvas.ActualWidth;
          //double d = endNode.Y * MainCanvas.ActualHeight;

          //double g = Math.Sqrt(Math.Pow(a-c,2)+Math.Pow(b-d,2));
          //double h = viewModelVehicle.PositionOnConnection;
          //double k = street.Length;

          double x01 = ((startNode.X * MainCanvas.ActualWidth * Math.Pow(endNode.Y * MainCanvas.ActualHeight, 2) - 2 * startNode.X * MainCanvas.ActualWidth * startNode.Y * MainCanvas.ActualHeight * endNode.Y * MainCanvas.ActualHeight + startNode.X * MainCanvas.ActualWidth * Math.Pow(endNode.X * MainCanvas.ActualWidth, 2) - 2 * Math.Pow(startNode.X * MainCanvas.ActualWidth, 2) * endNode.X * MainCanvas.ActualWidth +
                         startNode.X * MainCanvas.ActualWidth * Math.Pow(startNode.Y * MainCanvas.ActualHeight, 2) + Math.Pow(startNode.X * MainCanvas.ActualWidth, 3)) * street.Length
                        + (endNode.X * MainCanvas.ActualWidth - startNode.X * MainCanvas.ActualWidth) * Math.Sqrt(Math.Pow(endNode.Y * MainCanvas.ActualHeight, 2) - 2 * startNode.Y * MainCanvas.ActualHeight * endNode.Y * MainCanvas.ActualHeight + Math.Pow(endNode.X * MainCanvas.ActualWidth, 2) - 2 * startNode.X * MainCanvas.ActualWidth * endNode.X * MainCanvas.ActualWidth + Math.Pow(startNode.Y * MainCanvas.ActualHeight, 2) +
                                              Math.Pow(startNode.X * MainCanvas.ActualWidth, 2)) * Math.Sqrt(Math.Pow(startNode.X * MainCanvas.ActualWidth - endNode.X * MainCanvas.ActualWidth, 2) + Math.Pow(startNode.Y * MainCanvas.ActualHeight - endNode.Y * MainCanvas.ActualHeight, 2)) * viewModelVehicle.PositionOnConnection) /
                       ((Math.Pow(endNode.Y * MainCanvas.ActualHeight, 2) - 2 * startNode.Y * MainCanvas.ActualHeight * endNode.Y * MainCanvas.ActualHeight + Math.Pow(endNode.X * MainCanvas.ActualWidth, 2) - 2 * startNode.X * MainCanvas.ActualWidth * endNode.X * MainCanvas.ActualWidth + Math.Pow(startNode.Y * MainCanvas.ActualHeight, 2) + Math.Pow(startNode.X * MainCanvas.ActualWidth, 2)) *
                        street.Length);

          //double x02 = ((-a * Math.Pow(d, 2) + 2 * a * b * d - a * Math.Pow(c, 2) + 2 * Math.Pow(a, 2) * c -
          //               a * Math.Pow(b, 2) - Math.Pow(a, 3)) * k
          //              + (c - a) * Math.Sqrt(Math.Pow(d, 2) - 2 * b * d + Math.Pow(c, 2) - 2 * a * c + Math.Pow(b, 2) +
          //                                    Math.Pow(a, 2)) * g * h) /
          //             ((Math.Pow(d, 2) - 2 * b * d + Math.Pow(c, 2) - 2 * a * c + Math.Pow(b, 2) + Math.Pow(a, 2)) *
          //              k);
          double y01 = ((startNode.Y * MainCanvas.ActualHeight - endNode.Y * MainCanvas.ActualHeight) / (startNode.X * MainCanvas.ActualWidth - endNode.X * MainCanvas.ActualWidth)) * (x01 - startNode.X * MainCanvas.ActualWidth) + startNode.Y * MainCanvas.ActualHeight;
          //double y02 = ((b - d) / (a - c)) * (x02 - a) + b;

          Canvas.SetLeft(rectangle, x01);
          Canvas.SetTop(rectangle, y01 - rectangle.Height/2);

        }
      }
    }


  }
}