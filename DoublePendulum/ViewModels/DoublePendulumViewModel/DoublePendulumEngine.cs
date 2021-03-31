using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DoublePendulum
{
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