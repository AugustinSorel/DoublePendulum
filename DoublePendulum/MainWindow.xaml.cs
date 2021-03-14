using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
        private double a1_v = 0;
        private double a2_v = 0;
        private double g = 1;

        public MainWindow()
        {
            InitializeComponent();
            CreateTimer();
        }

        private void HandleTick(object sender, EventArgs e)
        {
            double num1 = -g * (2 * m1 + m2) * Math.Sin(a1);
            double num2 = -m2 * g * Math.Sin(a1 - 2 * a2);
            double num3 = -2 * Math.Sin(a1 - a2) * m2;
            double num4 = a2_v * a2_v * r2 + a1_v * a1_v * r1 * Math.Cos(a1 - a2);
            double den = r1 * (2 * m1 + m2 - m2 * Math.Cos(2 * a1 - 2 * a2));
            double a1_a = (num1 + num2 + num3 * num4) / den;

            num1 = 2 * Math.Sin(a1 - a2);
            num2 = (a1_v * a1_v * r1 * (m1 + m2));
            num3 = g * (m1 + m2) * Math.Cos(a1);
            num4 = a2_v * a2_v * r2 * m2 * Math.Cos(2 * a1 - 2 * a2);
            den = r2 * (2 * m1 + m2 - m2 * Math.Cos(2 * a1 - 2 * a2));

            double a2_a = (num1 * num2 + num3 + num4) / den;

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

            a1 += a1_v;
            a2 += a2_v;
            a1_v += a1_a;
            a2_v += a2_a;

            Ellipse ellipse = new Ellipse()
            {
                Stroke = Brushes.Black,
                Height = 10,
                Width = 10
            };

            Canvas.SetLeft(ellipse, x2 + 305);
            Canvas.SetTop(ellipse, y2 + 55);
            canvas.Children.Add(ellipse);
        }

        #region Create Timer
        private void CreateTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(HandleTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
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
