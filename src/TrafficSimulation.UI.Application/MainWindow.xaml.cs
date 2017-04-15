using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TrafficSimulation.UI.Application
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new ViewModelBase();
      
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
     


      DoubleAnimation da = new DoubleAnimation();
      da.Duration = new Duration(TimeSpan.FromSeconds(10));
      da.To = 300;
      da.From = 0;
      da.RepeatBehavior = RepeatBehavior.Forever;
      da.EasingFunction = new BounceEase();
      TranslateTransform tt = new TranslateTransform();
      rectangle1.RenderTransform = tt;
      tt.BeginAnimation(TranslateTransform.XProperty, da);
    



    }

    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      rectangle1.
    }
  }
}