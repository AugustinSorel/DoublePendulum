using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DoublePendulum
{
    class DoublePendulumViewModel : DependencyObject, INotifyPropertyChanged
    {
        #region Dp
        public Point CenterPoint
        {
            get { return (Point)GetValue(CenterPointProperty); }
            set { SetValue(CenterPointProperty, value); NotifyPropertyChanged("CenterPoint"); }
        }

        // Using a DependencyProperty as the backing store for vector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterPointProperty =
            DependencyProperty.Register("CenterPoint", typeof(Point), typeof(DoublePendulumView), new PropertyMetadata(new Point(400, 200)));

        public Point EndFirstArmPoint
        {
            get { return (Point)GetValue(EndFirstArmPointProperty); }
            set { SetValue(EndFirstArmPointProperty, value); NotifyPropertyChanged("EndFirstArmPoint"); }
        }

        // Using a DependencyProperty as the backing store for EndFirstArmPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndFirstArmPointProperty =
            DependencyProperty.Register("EndFirstArmPoint", typeof(Point), typeof(DoublePendulumView), new PropertyMetadata(new Point(0, 0)));

        public Point FirstCirclePoint
        {
            get { return (Point)GetValue(FirstCirclePointProperty); }
            set { SetValue(FirstCirclePointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstCirclePoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstCirclePointProperty =
            DependencyProperty.Register("FirstCirclePoint", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(0, 0)));

        public Point SecondCirclePoint
        {
            get { return (Point)GetValue(SecondCirclePointProperty); }
            set { SetValue(SecondCirclePointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondCirclePoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondCirclePointProperty =
            DependencyProperty.Register("SecondCirclePoint", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(0, 0)));

        public Point SecondArmPoint
        {
            get { return (Point)GetValue(SecondArmPointProperty); }
            set { SetValue(SecondArmPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondArmPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondArmPointProperty =
            DependencyProperty.Register("SecondArmPoint", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(0, 0)));

        public Point SecondArmEndPoint
        {
            get { return (Point)GetValue(SecondArmEndPointProperty); }
            set { SetValue(SecondArmEndPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondArmEndPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondArmEndPointProperty =
            DependencyProperty.Register("SecondArmEndPoint", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(0,0)));



        public Point FirstCircleRadius
        {
            get { return (Point)GetValue(FirstCirlceRadiusProperty); }
            set { SetValue(FirstCirlceRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstCirlceRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstCirlceRadiusProperty =
            DependencyProperty.Register("FirstCirlceRadius", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(10, 10)));

        #endregion

        public DoublePendulumView DoublePendulumView { get; }

        public DoublePendulumViewModel(DoublePendulumView doublePendulumView)
        {
            CreateTimer();
            DoublePendulumView = doublePendulumView;
        }

        private double r1 = 200;
        private double r2 = 200;
        private double m1 = 10;
        private double m2 = 10;
        private double a1 = Math.PI / 2;
        private double a2 = Math.PI;
        private double a1_v = 0;
        private double a2_v = 0;
        private double g = 1;

        private double px2 = -1;
        private double py2 = -1;

        private void HandleTick(object sender, EventArgs e)
        {
            double num1 = -g * (2 * m1 + m2) * Math.Sin(a1);
            double num2 = -m2 * g * Math.Sin(a1 - 2 * a2);
            double num3 = -2 * Math.Sin(a1 - a2) * m2;
            double num4 = a2_v * a2_v * r2 + a1_v * a1_v * r1 * Math.Cos(a1 - a2);
            double den = r1 * (2 * m1 + m2 - m2 * Math.Cos(2 * a1 - 2 * a2));
            double a1_a = (num1 + num2 + num3 * num4) / den;

            num1 = 2 * Math.Sin(a1 - a2);
            num2 = a1_v * a1_v * r1 * (m1 + m2);
            num3 = g * (m1 + m2) * Math.Cos(a1);
            num4 = a2_v * a2_v * r2 * m2 * Math.Cos(a1 - a2);
            den = r2 * (2 * m1 + m2 - m2 * Math.Cos(2 * a1 - 2 * a2));
            double a2_a = num1 * (num2 + num3 + num4) / den;

            double x1 = r1 * Math.Sin(a1);
            double y1 = r1 * Math.Cos(a1);

            double x2 = x1 + r2 * Math.Sin(a2);
            double y2 = y1 + r2 * Math.Cos(a2);

            EndFirstArmPoint = new Point(x1 + CenterPoint.X, y1 + CenterPoint.Y);

            FirstCirclePoint = new Point(x1 + CenterPoint.X, y1 + CenterPoint.Y);
            FirstCircleRadius = new Point(m1, m1);

            SecondArmPoint = new Point(x1 + CenterPoint.X, y1 + CenterPoint.Y);
            SecondArmEndPoint = new Point(x2 + CenterPoint.X, y2 + CenterPoint.Y);

            SecondCirclePoint = new Point(x2 + CenterPoint.X, y2 + CenterPoint.Y);
            DoublePendulumView.secondCircle.RadiusX = DoublePendulumView.secondCircle.RadiusY = m2;

            a1_v += a1_a;
            a2_v += a2_a;
            a1 += a1_v;
            a2 += a2_v;

            //a1_v *= 0.99;
            //a2_v *= 0.99;

            if (px2 != -1)
            {
                Line ellipse = new Line()
                {
                    Stroke = Brushes.White,
                    X1 = px2 + CenterPoint.X,
                    Y1 = py2 + CenterPoint.Y,
                    X2 = x2 + CenterPoint.X,
                    Y2 = y2 + CenterPoint.Y,
                    Fill = Brushes.Black,
                    StrokeThickness = 1,
                };

                //Ellipse ellipse1 = new Ellipse()
                //{
                //    Fill = Brushes.Black,
                //    Height = 6,
                //    Width = 6,
                //};
                //Canvas.SetLeft(ellipse1, px2 + cx + 3);
                //Canvas.SetTop(ellipse1, py2 + cy + 3);

                DoublePendulumView.canvas.Children.Add(ellipse);
            }

            px2 = x2;
            py2 = y2;
        }

        #region Create Timer
        private void CreateTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(HandleTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }
        #endregion

        #region Property Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}