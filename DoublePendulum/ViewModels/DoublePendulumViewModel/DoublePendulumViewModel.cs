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

        public Point SecondCircleRadius
        {
            get { return (Point)GetValue(SecondCircleRadiusProperty); }
            set { SetValue(SecondCircleRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondCircleRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondCircleRadiusProperty =
            DependencyProperty.Register("SecondCircleRadius", typeof(Point), typeof(DoublePendulumViewModel), new PropertyMetadata(new Point(10, 10)));

        #endregion

        private DoublePendulumModel doublePendulumModel;

        public DoublePendulumViewModel()
        {
            doublePendulumModel = new DoublePendulumModel();
            CreateTimer();
        }

        private void HandleTick(object sender, EventArgs e)
        {
            Point firstPoint = doublePendulumModel.GetFirstPoint();
            Point secondPoint = doublePendulumModel.GetSecondPoint();
            secondPoint.X += firstPoint.X;
            secondPoint.Y += firstPoint.Y;

            doublePendulumModel.Calculate2();

            EndFirstArmPoint = new Point(firstPoint.X + CenterPoint.X, firstPoint.Y + CenterPoint.Y);

            FirstCirclePoint = new Point(firstPoint.X + CenterPoint.X, firstPoint.Y + CenterPoint.Y);
            FirstCircleRadius = new Point(doublePendulumModel.M1, doublePendulumModel.M1);

            SecondArmPoint = new Point(firstPoint.X + CenterPoint.X, firstPoint.Y + CenterPoint.Y);
            SecondArmEndPoint = new Point(secondPoint.X + CenterPoint.X, secondPoint.Y + CenterPoint.Y);

            SecondCirclePoint = new Point(secondPoint.X + CenterPoint.X, secondPoint.Y + CenterPoint.Y);
            SecondCircleRadius = new Point(doublePendulumModel.M2, doublePendulumModel.M2);

            // friction
            //a1_v *= 0.99;
            //a2_v *= 0.99;

            

            doublePendulumModel.StoreCurrentPoint(secondPoint.X, secondPoint.Y);
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