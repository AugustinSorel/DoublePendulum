using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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
            set { SetValue(FirstCirlceRadiusProperty, value); NotifyPropertyChanged("FirstCircleRadius"); }
        }

        // Using a DependencyProperty as the backing store for FirstCirlceRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstCirlceRadiusProperty =
            DependencyProperty.Register("FirstCirlceRadius", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(10, 10)));

        public Point SecondCircleRadius
        {
            get { return (Point)GetValue(SecondCircleRadiusProperty); }
            set { SetValue(SecondCircleRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondCircleRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondCircleRadiusProperty =
            DependencyProperty.Register("SecondCircleRadius", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(10, 10)));

        #endregion


        public double WeightCircle2
        {
            get { return doublePendulumModel.M2; }
            set 
            { 
                doublePendulumModel.M2 = value;
                NotifyPropertyChanged("WeightCircle2"); 
            }
        }

        public double WeightCircle1
        {
            get { return doublePendulumModel.M1; }
            set
            {
                doublePendulumModel.M1 = value;
                NotifyPropertyChanged("WeightCircle1");
            }
        }

        public double LengthArm1
        {
            get { return doublePendulumModel.R1; }
            set
            {
                doublePendulumModel.R1 = value;
                NotifyPropertyChanged("LengthArm1");
            }
        }

        public double LengthArm2
        {
            get { return doublePendulumModel.R2; }
            set
            {
                doublePendulumModel.R2 = value;
                NotifyPropertyChanged("LengthArm2");
            }
        }

        private readonly DoublePendulumModel doublePendulumModel;
        private readonly BackgroundWorker backgroundWorker;

        public DoublePendulumViewModel()
        {
            backgroundWorker = new BackgroundWorker();
            doublePendulumModel = new DoublePendulumModel();

            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateTimer();
        }

        #region Create Timer
        private void CreateTimer()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(HandleTick);
            aTimer.Interval = 5;
            aTimer.Enabled = true;
        }
        #endregion

        private void HandleTick(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {

                Point firstCirclePoint = doublePendulumModel.GetFirstPoint();
                Point secondCirclePoint = doublePendulumModel.GetSecondPoint();
                secondCirclePoint.X += firstCirclePoint.X;
                secondCirclePoint.Y += firstCirclePoint.Y;

                doublePendulumModel.Calculate2();

                EndFirstArmPoint = new Point(firstCirclePoint.X + CenterPoint.X, firstCirclePoint.Y + CenterPoint.Y);

                FirstCirclePoint = new Point(firstCirclePoint.X + CenterPoint.X, firstCirclePoint.Y + CenterPoint.Y);
                FirstCircleRadius = new Point(doublePendulumModel.M1, doublePendulumModel.M1);

                SecondArmPoint = new Point(firstCirclePoint.X + CenterPoint.X, firstCirclePoint.Y + CenterPoint.Y);
                SecondArmEndPoint = new Point(secondCirclePoint.X + CenterPoint.X, secondCirclePoint.Y + CenterPoint.Y);

                SecondCirclePoint = new Point(secondCirclePoint.X + CenterPoint.X, secondCirclePoint.Y + CenterPoint.Y);
                SecondCircleRadius = new Point(doublePendulumModel.M2, doublePendulumModel.M2);

                // friction
                //a1_v *= 0.99;
                //a2_v *= 0.99;

                DrawOldPosition(secondCirclePoint);
            }));
        }

        private static readonly Random random = new Random();

        private void DrawOldPosition(Point secondCirclePoint)
        {
            
            if (doublePendulumModel.PreviousXNotNull())
            {

                int red = random.Next(0, 255);
                int blue = random.Next(0, 255);
                int green = random.Next(0, 255);
                Line ellipse = new Line()
                {
                    Stroke = new SolidColorBrush(Color.FromRgb((byte)red, (byte)green, (byte)blue)),
                    X1 = doublePendulumModel.previousX2 + CenterPoint.X,
                    Y1 = doublePendulumModel.previousY2 + CenterPoint.Y,
                    X2 = secondCirclePoint.X + CenterPoint.X,
                    Y2 = secondCirclePoint.Y + CenterPoint.Y,
                StrokeThickness = 1,
                };

                //Ellipse ellipse = new Ellipse()
                //{
                //    Fill = Brushes.White,
                //    Height = 6,
                //    Width = 6,
                //};
                //Canvas.SetLeft(ellipse, doublePendulumModel.previousX2 + CenterPoint.X + ellipse.Width / 2);
                //Canvas.SetTop(ellipse, doublePendulumModel.previousY2 + CenterPoint.Y + ellipse.Height / 2);

                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.canvas.Children.Add(ellipse);
            }
            doublePendulumModel.StoreCurrentPoint(secondCirclePoint.X, secondCirclePoint.Y);
        }

        #region Property Changed Event Handler
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}