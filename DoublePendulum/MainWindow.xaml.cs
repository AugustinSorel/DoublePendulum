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
        private double r1 = 200;
        private double r2 = 200;
        private double m1 = 40;
        private double m2 = 40;
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

            double x2 = x1 + r2 * Math.Sin(a2);
            double y2 = y1 + r2 * Math.Cos(a2);

            mainArm.X1 = 300;
            mainArm.Y1 = 50;
            mainArm.X2 = x1 + 300;
            mainArm.Y2 = y1 + 50;

            center.Center = new Point(x1 + 300, y1 + 50);
            center.RadiusX = center.RadiusY = m1;


            secondArm.X1 = x1 + 300;
            secondArm.Y1 = y1 + 50;
            secondArm.X2 = x2 + 300;
            secondArm.Y2 = y2 + 50;

            center2.Center = new Point(x2 + 300, y2 + 50);
            center2.RadiusX = center2.RadiusY = m2;
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
