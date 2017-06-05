using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using NLog;
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

    private bool IsDebugOn { get; set; }

    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor for the MainWindow view. Initializes all components.
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();
      
    }

  


    

    Rectangle DrawVehicle(Brush color)
    {
      return new Rectangle()
      {
        Width = 10,
        Height = 10,
        Fill = color,
        Stroke = color,
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
        try
        {


          foreach (var node in ViewModel.Nodes)
          {
            var rectangle = DrawNode(5, 5);
            MainCanvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, node.X * MainCanvas.ActualWidth - rectangle.Height / 2);
            Canvas.SetTop(rectangle, node.Y * MainCanvas.ActualHeight - rectangle.Height / 2);


          }

          foreach (var nodeconnection in ViewModel.NodeConnections)
          {
            Node startNode = ViewModel.Nodes.First(x => x.Id == nodeconnection.StartNodeId);
            Node endNode = ViewModel.Nodes.First(x => x.Id == nodeconnection.EndNodeId);
            var line = DrawStreet(startNode.X * MainCanvas.ActualWidth, startNode.Y * MainCanvas.ActualHeight,
              endNode.X * MainCanvas.ActualWidth, endNode.Y * MainCanvas.ActualHeight);
            MainCanvas.Children.Add(line);


          }


          foreach (var viewModelVehicle in ViewModel.Vehicles)
          {
            Brush color = Brushes.Green;
            if (viewModelVehicle.IsForeignCar)
            {
              color = Brushes.Red;
            }

            var rectangle = DrawVehicle(color);

            NodeConnection street =
              ViewModel.NodeConnections.First(nc => nc.Id == viewModelVehicle.CurrentNodeConnectionId);
            Node startNode = ViewModel.Nodes.First(n => n.Id == street.StartNodeId);
            Node endNode = ViewModel.Nodes.First(n => n.Id == street.EndNodeId);
            MainCanvas.Children.Add(rectangle);
            var deltaX = endNode.X - startNode.X;
            var deltaY = endNode.Y - startNode.Y;
            var x = deltaX * (viewModelVehicle.PositionOnConnection / street.Length) + startNode.X;
            var y = deltaY * (viewModelVehicle.PositionOnConnection / street.Length) + startNode.Y;
            Canvas.SetLeft(rectangle, x * MainCanvas.ActualWidth - rectangle.Height / 2);
            Canvas.SetTop(rectangle, y * MainCanvas.ActualHeight - rectangle.Height / 2);


            if (IsDebugOn)
            {
              if (viewModelVehicle.DebugInfo != null)
              {
                var DebugInfo_label = new Label()
                {
                  Content = viewModelVehicle.DebugInfo.ToString()
                };
                MainCanvas.Children.Add(DebugInfo_label);
                Canvas.SetLeft(DebugInfo_label, x * MainCanvas.ActualWidth);
                Canvas.SetTop(DebugInfo_label, Canvas.GetTop(rectangle));
              }
            }

          }



          foreach (var cs in ViewModel.ConstructionSides)
          {
            var rectangle = cs.Key;
            var pos = cs.Value;
            MainCanvas.Children.Add(rectangle);
            Canvas.SetTop(rectangle, pos.Y);
            Canvas.SetLeft(rectangle, pos.X);

          }
        }
        catch (InvalidOperationException exception)
        {
          //if (DisconnectBtn.IsEnabled)
          //{
          //  DisconnectBtn.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
          //  DisconnectBtn.Command?.Execute(null);
          //  return;
          //}
          Logger.Error(exception);
        }
      }

    }


    private void StartStopBtn_OnClick(object sender, RoutedEventArgs e)
    {
      Button btn = sender as Button;
      btn.Visibility = Visibility.Hidden;
      if (btn.Name == "StartBtn")
      {
        StopBtn.Visibility = Visibility.Visible;
        StepBtn.IsEnabled = false;
        //DisconnectBtn.IsEnabled = true;
      }
      else
      {
        StartBtn.Visibility = Visibility.Visible;
        StepBtn.IsEnabled = true;
        //DisconnectBtn.IsEnabled = false;
      }
       
    }

    private void MainCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
      //var rect = DrawConstructionSide(20, 20);
      //Point pos = e.GetPosition(MainCanvas);
      //MainCanvas.Children.Add(rect);
      //Canvas.SetLeft(rect, pos.X);
      //Canvas.SetTop(rect, pos.Y);
      //ViewModel.ConstructionSides.Add(new KeyValuePair<Rectangle, Point>(rect,pos));

      
    }

    Rectangle DrawConstructionSide(double width, double height)
    {
      return new Rectangle()
      {
        Width = width,
        Height = height,
        Fill = Brushes.DarkRed,
        Stroke = Brushes.Yellow,
        StrokeThickness = 2
      };
    }

    private void DisconnectBtn_Click(object sender, RoutedEventArgs e)
    {
      Button btn = sender as Button;
      btn.Visibility = Visibility.Hidden;
      if (btn.Name == "ConnectBtn")
      {
        DisconnectBtn.Visibility = Visibility.Visible;
        StartBtn.IsEnabled = true;
        StopBtn.IsEnabled = true;
        StepBtn.IsEnabled = false;
      }
      else if (btn.Name == "DisconnectBtn") 
      {
        ConnectBtn.Visibility = Visibility.Visible;
        StartBtn.IsEnabled = false;
        StopBtn.IsEnabled = false;
        StepBtn.IsEnabled = false;

      }
    }

    private void DebugModeRadio_Checked(object sender, RoutedEventArgs e)
    {

      if (IsDebugOn)
      {
        IsDebugOn = false;
        DebugModeBtn.Content = "Debug";
        DebugModeBtn.Foreground = Brushes.Black;
      }
      else
      {
        IsDebugOn = true;
        DebugModeBtn.Content = "DEBUGGING";
        DebugModeBtn.Foreground = Brushes.OrangeRed;
      }

      

    }


    private void MainCanvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      var mc = sender as Canvas;
      mc.Width = mc.ActualHeight;
      
    }
  }
}