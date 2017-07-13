using System;
using System.Linq;
using System.Windows;
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

    private bool IsExceptionThrown { get; set; }
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

    Rectangle DrawTrafficLight(Brush color)
    {
      return new Rectangle()
      {
        Width = 5,
        Height = 5,
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
            Brush color = Brushes.Blue;
            if (viewModelVehicle.IsForeignCar)
            {
              color = Brushes.LightPink;
            }

            var rectangle = DrawVehicle(color);
            rectangle.Tag = viewModelVehicle.Id;
            rectangle.MouseDown += Car_OnMouseDown;
            rectangle.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.Hand;
            rectangle.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;
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



          foreach (var viewModelTrafficLight in ViewModel.TrafficLights)
          {
            TrafficLightState State = viewModelTrafficLight.State;
            Brush color = Brushes.Green; 
            switch (State)
            {
              case TrafficLightState.Red:
                color = Brushes.Red;
                break;
              case TrafficLightState.Green:
                color = Brushes.Green;
                break;
              case TrafficLightState.Disabled:
                color = Brushes.Yellow;
                break;
              default:
                color = Brushes.Green;
                break;
            }

            var rectangle = DrawTrafficLight(color);
            rectangle.Tag = viewModelTrafficLight.Id;
            rectangle.MouseDown += TrafficLight_OnMouseDown;
            rectangle.MouseEnter += (s, e) => Mouse.OverrideCursor = Cursors.Hand;
            rectangle.MouseLeave += (s, e) => Mouse.OverrideCursor = Cursors.Arrow;

            NodeConnection street =
            ViewModel.NodeConnections.First(nc => nc.Id == viewModelTrafficLight.ConnectionId);
            Node startNode = ViewModel.Nodes.First(n => n.Id == street.StartNodeId);
            Node endNode = ViewModel.Nodes.First(n => n.Id == street.EndNodeId);
            MainCanvas.Children.Add(rectangle);
            var deltaX = endNode.X - startNode.X;
            var deltaY = endNode.Y - startNode.Y;
            var x = deltaX * (viewModelTrafficLight.PositionOnConnection / street.Length) + startNode.X;
            var y = deltaY * (viewModelTrafficLight.PositionOnConnection / street.Length) + startNode.Y;
            Canvas.SetLeft(rectangle, x * MainCanvas.ActualWidth - rectangle.Height / 2);
            Canvas.SetTop(rectangle, y * MainCanvas.ActualHeight - rectangle.Height / 2);




            var ID_label = new Label()
            {
              Content = viewModelTrafficLight.Id.ToString()
            };
            MainCanvas.Children.Add(ID_label);
            Canvas.SetLeft(ID_label, x * MainCanvas.ActualWidth);
            Canvas.SetTop(ID_label, Canvas.GetTop(rectangle));


          }
        }
        catch (InvalidOperationException exception)
        {

          IsExceptionThrown = true;
          ViewModel.StopTimers();
          DisconnectBtn_Click(DisconnectBtn, new RoutedEventArgs());

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
      }
      else
      {
        StartBtn.Visibility = Visibility.Visible;
        StepBtn.IsEnabled = true;
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

      private void DisconnectBtn_Click(object sender, RoutedEventArgs e)
    {
      Button btn = sender as Button;
      btn.Visibility = Visibility.Hidden;
      if (btn.Name == "ConnectBtn")
      {
       
        DisconnectBtn.Visibility = Visibility.Visible;
        StartBtn.Visibility = Visibility.Hidden;
        StopBtn.Visibility = Visibility.Visible;
        StartBtn.IsEnabled = true;
        StopBtn.IsEnabled = true;
        DebugModeBtn.IsEnabled = true;
        StepBtn.IsEnabled = false;
        
      }
      else if (btn.Name == "DisconnectBtn") 
      {

        ConnectBtn.Visibility = Visibility.Visible;
        StartBtn.Visibility = Visibility.Visible;
        StopBtn.Visibility = Visibility.Hidden;
        StartBtn.IsEnabled = false;
        StopBtn.IsEnabled = false;
        StepBtn.IsEnabled = false;
        DebugModeBtn.IsEnabled = false;
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

    private void Car_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
      ViewModel.SetCarDefect(Int32.Parse((sender as Rectangle).Tag.ToString()));
    }
    
    private void Car_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      lb_test.Content += " repaired: " + (sender as Rectangle).Tag.ToString();
      ViewModel.UNsetCarDefect((Int32.Parse((sender as Rectangle).Tag.ToString())));
    }

    private void TrafficLight_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
      ViewModel.ToggleTrafficLight(Int32.Parse((sender as Rectangle).Tag.ToString()));
    }
  }
}