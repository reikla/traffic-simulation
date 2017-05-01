using System;
using System.ServiceModel;
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

      return new Rectangle(){
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


  }
}