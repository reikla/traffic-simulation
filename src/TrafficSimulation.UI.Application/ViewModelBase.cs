using System.Windows.Input;
using NLog;

namespace TrafficSimulation.UI.Application
{
  public class ViewModelBase
  {
    private ICommand _clickCommand;

    public ICommand ClickCommand => _clickCommand ?? (_clickCommand = new CommandHandler(()=>LogManager.GetCurrentClassLogger().Warn("Button Clicked"), true));
  }
}