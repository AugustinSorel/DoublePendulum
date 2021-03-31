using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
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
        private DoublePendulumEngine doublePendulumEngine;
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

    class DoublePendulumEngine
    {
        private readonly BackgroundWorker backgroundWorker;
        private readonly DoublePendulumViewModel doublePendulumViewModel;
        private Timer aTimer;

        public DoublePendulumEngine(DoublePendulumViewModel doublePendulumViewModel)
        {
            backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerAsync();
            this.doublePendulumViewModel = doublePendulumViewModel;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateTimer();
        }

        #region HandleTick
        private void HandleTick(object sender, EventArgs e)
        {
            Point firstCirclePoint = doublePendulumViewModel.doublePendulumModel.GetFirstPoint();
            Point secondCirclePoint = doublePendulumViewModel.doublePendulumModel.GetSecondPoint();
            secondCirclePoint.X += firstCirclePoint.X;
            secondCirclePoint.Y += firstCirclePoint.Y;

            doublePendulumViewModel.doublePendulumModel.Calculate2();

            Application.Current.Dispatcher.Invoke(new Action(() => {

                doublePendulumViewModel.EndFirstArmPoint = new Point(firstCirclePoint.X + doublePendulumViewModel.CenterPoint.X, firstCirclePoint.Y + doublePendulumViewModel.CenterPoint.Y);
                
                doublePendulumViewModel.FirstCirclePoint = new Point(firstCirclePoint.X + doublePendulumViewModel.CenterPoint.X, firstCirclePoint.Y + doublePendulumViewModel.CenterPoint.Y);
                doublePendulumViewModel.FirstCircleRadius = new Point(doublePendulumViewModel.doublePendulumModel.M1, doublePendulumViewModel.doublePendulumModel.M1);
                
                doublePendulumViewModel.SecondArmPoint = new Point(firstCirclePoint.X + doublePendulumViewModel.CenterPoint.X, firstCirclePoint.Y + doublePendulumViewModel.CenterPoint.Y);
                doublePendulumViewModel.SecondArmEndPoint = new Point(secondCirclePoint.X + doublePendulumViewModel.CenterPoint.X, secondCirclePoint.Y + doublePendulumViewModel.CenterPoint.Y);
                
                doublePendulumViewModel.SecondCirclePoint = new Point(secondCirclePoint.X + doublePendulumViewModel.CenterPoint.X, secondCirclePoint.Y + doublePendulumViewModel.CenterPoint.Y);
                doublePendulumViewModel.SecondCircleRadius = new Point(doublePendulumViewModel.doublePendulumModel.M2, doublePendulumViewModel.doublePendulumModel.M2);

                DrawOldPosition(secondCirclePoint);
            }));
        }
        #endregion

        #region DrawOlPosition
        private void DrawOldPosition(Point secondCirclePoint)
        {
            if (doublePendulumViewModel.doublePendulumModel.PreviousXNotNull() && (Application.Current.Windows[0] as MainWindow).doublePendulumView2.checkBoxTrace.IsChecked == true)
            {
                Line ellipse = new Line()
                {
                    Stroke = Brushes.White,
                    X1 = doublePendulumViewModel.doublePendulumModel.previousX2 + doublePendulumViewModel.CenterPoint.X,
                    Y1 = doublePendulumViewModel.doublePendulumModel.previousY2 + doublePendulumViewModel.CenterPoint.Y,
                    X2 = secondCirclePoint.X + doublePendulumViewModel.CenterPoint.X,
                    Y2 = secondCirclePoint.Y + doublePendulumViewModel.CenterPoint.Y,
                    StrokeThickness = 1,
                };

                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.canvas.Children.Add(ellipse);
            }
            doublePendulumViewModel.doublePendulumModel.StoreCurrentPoint(secondCirclePoint.X, secondCirclePoint.Y);
        }
        #endregion

        #region Create Timer
        private void CreateTimer()
        {
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(HandleTick);
            aTimer.Interval = 5;
            aTimer.Enabled = true;
        }

        internal void Start()
        {
            aTimer.Enabled = true;
        }

        internal void Pause()
        {
            aTimer.Enabled ^= true;
        }

        internal void RemoveTraceLine()
        {
            List<Line> listOfLineToRemove = new List<Line>();
            foreach (var item in (Application.Current.Windows[0] as MainWindow).doublePendulumView2.canvas.Children)
                if (item.GetType() == typeof(Line) && (item as Line).Name == string.Empty)
                    listOfLineToRemove.Add(item as Line);

            foreach (var item in listOfLineToRemove)
                (Application.Current.Windows[0] as MainWindow).doublePendulumView2.canvas.Children.Remove(item);
        }

        internal void Stop()
        {
            if (aTimer.Enabled)
            {
                aTimer.Stop();
                Application.Current.Dispatcher.Invoke(new Action(() => {

                    doublePendulumViewModel.CenterPoint = (Application.Current.Windows[0] as MainWindow).WindowState == WindowState.Maximized
                    ? new Point(SystemParameters.WorkArea.Width / 2, SystemParameters.WorkArea.Height / 4)
                    : new Point(400, 100);
                    doublePendulumViewModel.EndFirstArmPoint = new Point(0, 0);
                    doublePendulumViewModel.FirstCirclePoint = new Point(0, 0);
                    doublePendulumViewModel.SecondCirclePoint = new Point(0, 0);
                    doublePendulumViewModel.SecondArmPoint = new Point(0, 0);
                    doublePendulumViewModel.SecondArmEndPoint = new Point(0, 0);
                    doublePendulumViewModel.FirstCircleRadius = new Point(10, 10);
                    doublePendulumViewModel.SecondCircleRadius = new Point(10, 10);

                    RemoveTraceLine();

                    doublePendulumViewModel.doublePendulumModel.ResetValue();

                }));
            }
        }
        #endregion
    }
}