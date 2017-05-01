using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
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
      DispatcherTimer timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(1);
      timer.Tick += timer_Tick;
      timer.Start();
    }

    void timer_Tick(object sender, EventArgs e)
    {
      UpdateViewModel();
    }

    void UpdateViewModel()
    {
    }

    public void DrawSim()
    {
      foreach (var street in ViewModel.NodeConnections)
      {
      }
    }

    public Rectangle DrawVehicle(double width, double height, Brush brush)
    {
      return new Rectangle()
      {
        Width = width,
        Height = height,
        Fill = brush
      };
    }

    public Rectangle DrawStreet(double width, double height, Brush brush)
    {
      return new Rectangle()
      {
        Width = width,
        Height = height,
        Fill = brush
      };
    }

    public Rectangle DrawNode(double width, double height, Brush brush)
    {
      return new Rectangle()
      {
        Width = width,
        Height = height,
        Fill = brush,
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
          var rectangle = DrawNode(5, 5, Brushes.Black);
          MainCanvas.Children.Add(rectangle);
          Canvas.SetLeft(rectangle, node.X * MainCanvas.ActualWidth);
          Canvas.SetTop(rectangle, node.Y * MainCanvas.ActualHeight);
        }

        foreach (var viewModelVehicle in ViewModel.Vehicles)
        {
          var rectangle = DrawVehicle(10, 10, Brushes.Green);
          MainCanvas.Children.Add(rectangle);
          Canvas.SetLeft(rectangle, viewModelVehicle.PositionOnConnection);
        }
      }
    }
  }
}