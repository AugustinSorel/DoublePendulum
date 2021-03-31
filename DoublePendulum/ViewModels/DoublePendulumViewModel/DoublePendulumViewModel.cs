using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
            DependencyProperty.Register("CenterPoint", typeof(Point), typeof(DoublePendulumView), new PropertyMetadata(new Point(400, 100)));

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

        #region Property
        public DoublePendulumModel DoublePendulumModel
        {
            get { return doublePendulumModel; }
            set { doublePendulumModel = value; NotifyPropertyChanged("DoublePendulumModel"); }
        }

        #endregion

        #region Fields
        public DoublePendulumModel doublePendulumModel;
        private readonly DoublePendulumEngine doublePendulumEngine;
        #endregion

        public DoublePendulumViewModel()
        {
            doublePendulumModel = new DoublePendulumModel();
            doublePendulumEngine = new DoublePendulumEngine(this);
        }

        #region Click Event
        internal void Start()
        {
            doublePendulumEngine.Start();
        }

        internal void Pause()
        {
            doublePendulumEngine.Pause();
        }

        internal void FullScreen()
        {
            new HandleFullScreen(this);
            doublePendulumEngine.RemoveTraceLine();
        }

        internal void Stop()
        {
            doublePendulumEngine.Stop();
        }

        internal void CleanData()
        {
            doublePendulumEngine.RemoveTraceLine();
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