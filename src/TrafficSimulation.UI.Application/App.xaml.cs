using System.Windows;

namespace TrafficSimulation.UI.Application
{
  /// <summary>
  /// Startup-Class of the whole UI.Application
  /// </summary>
  public partial class App
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var bootstrapper = new Bootstrapper();
      bootstrapper.Run();

    }
  }
}
