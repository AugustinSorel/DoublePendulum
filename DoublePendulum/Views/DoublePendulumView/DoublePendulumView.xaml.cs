﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DoublePendulum
{
    /// <summary>
    /// Interaction logic for DoublePendulumView.xaml
    /// </summary>
    public partial class DoublePendulumView : UserControl
    {
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

        public int Cy
        {
            get { return (int)GetValue(CyProperty); }
            set { SetValue(CyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CyProperty =
            DependencyProperty.Register("Cy", typeof(int), typeof(DoublePendulumView), new PropertyMetadata(200));

        public int Cx
        {
            get { return (int)GetValue(CxProperty); }
            set { SetValue(CxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cx.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CxProperty =
            DependencyProperty.Register("Cx", typeof(int), typeof(DoublePendulumView), new PropertyMetadata(400));
        
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

            //firstArm.X1 = cx;
            //firstArm.Y1 = Cy;
            firstArm.X2 = x1 + Cx;
            firstArm.Y2 = y1 + Cy;

            firstCircle.Center = new Point(x1 + Cx, y1 + Cy);
            firstCircle.RadiusX = firstCircle.RadiusY = m1;


            secondArm.X1 = x1 + Cx;
            secondArm.Y1 = y1 + Cy;
            secondArm.X2 = x2 + Cx;
            secondArm.Y2 = y2 + Cy;

            secondCircle.Center = new Point(x2 + Cx, y2 + Cy);
            secondCircle.RadiusX = secondCircle.RadiusY = m2;

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
                    X1 = px2 + Cx,
                    Y1 = py2 + Cy,
                    X2 = x2 + Cx,
                    Y2 = y2 + Cy,
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

                canvas.Children.Add(ellipse);
            }

            px2 = x2;
            py2 = y2;
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

        public DoublePendulumView()
        {
            InitializeComponent();
            DataContext = this;
            CreateTimer();
        }
    }
}
