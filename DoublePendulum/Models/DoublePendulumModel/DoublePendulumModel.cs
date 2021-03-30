using System;
using System.ComponentModel;
using System.Windows;

namespace DoublePendulum
{
    class DoublePendulumModel : INotifyPropertyChanged
    {
        private double m1;
        private double m2;
        private double r1;
        private double r2;
        private double a1;
        private double a2;
        private double a1_v;
        private double a2_v;
        private double g;
        private double friction;

        public double previousX2;
        public double previousY2;

        public DoublePendulumModel()
        {
            ResetValue();
        }

        public double M1
        {
            get { return m1; }
            set 
            {
                m1 = value;
                NotifyPropertyChanged("M1");
            }
        }

        public double M2
        {
            get { return m2; }
            set 
            { 
                m2 = value;
                NotifyPropertyChanged("M2");
            }
        }

        public double R1
        {
            get { return r1; }
            set 
            { 
                r1 = value;
                NotifyPropertyChanged("R1");
            }
        }

        public double R2
        {
            get { return r2; }
            set 
            { 
                r2 = value;
                NotifyPropertyChanged("R2");
            }
        }

        public double A1
        {
            get { return Math.Round(a1, 4); }
            set 
            {
                a1 = value; 
                NotifyPropertyChanged("A1"); 
            }
        }

        public double A2
        {
            get { return Math.Round(a2, 4); }
            set 
            { 
                a2 = value;
                NotifyPropertyChanged("A2");
            }
        }

        public double A1_v
        {
            get { return Math.Round(a1_v, 4); }
            set 
            { 
                a1_v = value; 
                NotifyPropertyChanged("A1_v"); 
            }
        }

        public double A2_v
        {
            get { return Math.Round(a2_v, 4); }
            set 
            { 
                a2_v = value; 
                NotifyPropertyChanged("A2_v"); 
            }
        }

        public double G
        {
            get { return g; }
            set
            {
                g = value;
                NotifyPropertyChanged("G");
            }
        }

        public double Friction
        {
            get { return friction; }
            set { friction = value; }
        }


        internal void ResetValue()
        {
            M1 = 10;
            M2 = 10;
            R1 = 200;
            R2 = 200;
            A1 = Math.PI / 2;
            A2 = Math.PI;
            A1_v = 0;
            A2_v = 0;
            previousX2 = -1;
            previousY2 = -1;
            G = 1;
            Friction = 1;
        }

        #region Property Changed Event Handler 
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        internal bool PreviousXNotNull()
        {
            return previousX2 != -1;
        }

        internal void StoreCurrentPoint(double x2, double y2)
        {
            previousX2 = x2;
            previousY2 = y2;
        }

        internal void Calculate2()
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
            
            A1_v += a1_a;
            A2_v += a2_a;
            A1 += a1_v;
            A2 += a2_v;

            // friction
             A1_v *= friction;
             A2_v *= friction;
        }

        internal Point GetFirstPoint()
        {
            return new Point(r1 * Math.Sin(a1), r1 * Math.Cos(a1));
        }

        internal Point GetSecondPoint()
        {
            return new Point(r2 * Math.Sin(a2), r2 * Math.Cos(a2));
        }
    }
}
