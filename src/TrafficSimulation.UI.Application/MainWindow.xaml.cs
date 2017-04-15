using System;
using System.Windows;
using System.Windows.Input;
using NLog;

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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      LogManager.GetCurrentClassLogger().Warn("Button Clicked");
    }
  }

  public class ViewModelBase
  {
    public ViewModelBase()
    {
      _canExecute = true;
    }
    private ICommand _clickCommand;
    public ICommand ClickCommand
    {
      get
      {
        return _clickCommand ?? (_clickCommand = new CommandHandler(MyAction, _canExecute));
      }
    }
    private bool _canExecute;
    public void MyAction()
    {
      LogManager.GetCurrentClassLogger().Debug("Button Clicked");
    }
  }
  public class CommandHandler : ICommand
  {
    private Action _action;
    private bool _canExecute;
    public CommandHandler(Action action, bool canExecute)
    {
      _action = action;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return _canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      _action();
    }
  }
}
