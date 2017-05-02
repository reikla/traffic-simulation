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


    public MainWindow()
    {
      InitializeComponent();
    }
    //  DispatcherTimer timer = new DispatcherTimer();
    //  timer.Interval = TimeSpan.FromSeconds(1);
    //  timer.Tick += timer_Tick;
    //  timer.Start();
    //}

    //void timer_Tick(object sender, EventArgs e)
    //{
    //  UpdateViewModel();
    //}

    //void UpdateViewModel()
    //{
    //}

    //public void DrawSim()
    //{
    //  foreach (var street in ViewModel.NodeConnections)
    //  {
    //  }
    //}

    public Rectangle DrawVehicle(double width, double height)
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

    public Line DrawStreet(double xStart, double yStart, double xEnde, double yEnde)
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

    public Rectangle DrawNode(double width, double height)
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
          //if (endNode.Y == startNode.Y)
          //{
          //  Canvas.SetLeft(rectangle, (viewModelVehicle.PositionOnConnection / street.Length) * (endNode.X * MainCanvas.ActualWidth - startNode.X * MainCanvas.ActualWidth));
          //  Canvas.SetTop(rectangle, endNode.Y * MainCanvas.ActualHeight - rectangle.Height / 2);
          //}
          //else if (endNode.X == startNode.X)
          //{

          //  Canvas.SetLeft(rectangle, endNode.X * MainCanvas.ActualWidth - rectangle.Width / 2);
          //  Canvas.SetTop(rectangle, (viewModelVehicle.PositionOnConnection / street.Length) * (endNode.Y * MainCanvas.ActualHeight - startNode.Y * MainCanvas.ActualHeight));

          //}

          //double distance = Math.Sqrt(Math.Pow((endNode.X - startNode.X) * MainCanvas.ActualWidth, 2) +
          //                    Math.Pow((endNode.Y - startNode.Y) * MainCanvas.ActualHeight, 2));

          //double progressonstreet = viewModelVehicle.PositionOnConnection / street.Length;



        }
      }
    }


  }
}