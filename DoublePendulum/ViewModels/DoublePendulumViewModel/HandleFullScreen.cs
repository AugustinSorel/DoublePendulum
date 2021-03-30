using System.Windows;
using System.Windows.Controls;

namespace DoublePendulum
{
    internal class HandleFullScreen
    {
        public HandleFullScreen(DoublePendulumViewModel doublePendulumViewModel)
        {
            if ((Application.Current.Windows[0] as MainWindow).WindowState == WindowState.Maximized)
            {
                (Application.Current.Windows[0] as MainWindow).WindowState = WindowState.Normal;
                doublePendulumViewModel.CenterPoint = new Point(400, 100);
                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelCircle1Data.Visibility = Visibility.Collapsed;
                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelCircle2Data.Visibility = Visibility.Collapsed;

                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelSlider1.Visibility = Visibility.Collapsed;
                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelSlider2.Visibility = Visibility.Collapsed;

                Grid.SetColumn((Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelButtons1, 1);
                Grid.SetColumn((Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelButtons2, 1);

            }
            else
            {
                (Application.Current.Windows[0] as MainWindow).WindowState = WindowState.Maximized;
                doublePendulumViewModel.CenterPoint = new Point(SystemParameters.WorkArea.Width / 2, SystemParameters.WorkArea.Height / 4);

                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelCircle1Data.Visibility = Visibility.Visible;
                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelCircle2Data.Visibility = Visibility.Visible;

                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelSlider1.Visibility = Visibility.Visible;
                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelSlider2.Visibility = Visibility.Visible;

                Grid.SetColumn((Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelButtons1, 2);
                Grid.SetColumn((Application.Current.Windows[0] as MainWindow).doublePendulumView2.stackPanelButtons2, 2);
            }
        }
    }
}