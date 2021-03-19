using System;
using System.Windows;

namespace DoublePendulum
{
    class DoublePendulumModel
    {
        private double m1;
        private double m2;
        private readonly double r1;
        private readonly double r2;
        private double a1;
        private double a2;
        private double a1_v;
        private double a2_v;
        private readonly double g;

        public double previousX2;
        public double previousY2;

        public DoublePendulumModel()
        {
            m1 = 10;
            m2 = 10;
            r1 = 200;
            r2 = 200;
            a1 = Math.PI / 2;
            a2 = Math.PI;
            a1_v = 0;
            previousX2 = -1;
            previousY2 = -1;
            g = 1;
        }

        public double M1
        {
            get { return m1; }
            set { m1 = value; }
        }

        public double M2
        {
            get { return m2; }
            set { m2 = value; }
        }

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
            
            a1_v += a1_a;
            a2_v += a2_a;
            a1 += a1_v;
            a2 += a2_v;
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
