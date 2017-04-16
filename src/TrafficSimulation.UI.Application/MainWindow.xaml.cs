using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TrafficSimulation.UI.Application
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    bool animation_start = false;
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new ViewModelBase();
      Rectangle car = new Rectangle();
      car.Fill = Brushes.Black;
      car.Width = 100;
      car.Height = 50;

      Canvas canvas = new Canvas();
      canvas.Width = 500;
      canvas.Height = 500;
      canvas.Background = Brushes.LightGray;
      Rectangle street = new Rectangle();
      street.Fill = Brushes.SlateGray;
      canvas.Children.Add(street);
      canvas.Children.Add(car);
      

      this.Content = canvas;
      UpdateLayout();
     
    }


    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      UpdateLayout();
      MainWindow mw = sender as MainWindow;
      Canvas cv = mw.Content as Canvas;
      cv.Height = mw.ActualHeight;
      cv.Width = mw.ActualWidth;
      Rectangle street = cv.Children[0] as Rectangle;
      Rectangle car = cv.Children[1] as Rectangle;
      street.Width = mw.ActualWidth;
      street.Height = car.Height*2;
      Canvas.SetTop(street, mw.ActualHeight / 2);
      Canvas.SetTop(car, Canvas.GetTop(street) + street.Height - car.Height);
      TranslateTransform tt = new TranslateTransform();
      car.RenderTransform = tt;
      DoubleAnimation da = new DoubleAnimation(0, mw.ActualWidth/*-car.Width*/, TimeSpan.FromSeconds(20));
      da.RepeatBehavior = RepeatBehavior.Forever;
      //da.EasingFunction = new SineEase();
      tt.BeginAnimation(TranslateTransform.XProperty, da);
   
    }
  }
}