using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace DoublePendulum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double r1 = 100;
        private double r2 = 100;
        private double m1 = 10;
        private double m2 = 10;
        private double a1 = 0;
        private double a2 = 0;

        public MainWindow()
        {
            InitializeComponent();
            CreateTimer();
        }

        private void HandleTick(object sender, EventArgs e)
        {
            double x1 = r1 * Math.Sin(a1);
            double y1 = r1 * Math.Cos(a1);

            mainArm.X1 = 0 + 300;
            mainArm.Y1 = 0 + 50;
            mainArm.X2 = x1 + 300;
            mainArm.Y2 = y1 + 50;

            center.Center = new Point(x1+300, y1+50);
            center.RadiusX = center.RadiusY = m1;
        }

        #region Create Timer
        private void CreateTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(HandleTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();
        }
        #endregion

        #region Key Event Handler
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Application.Current.Shutdown();
        }
        #endregion
    }
}
