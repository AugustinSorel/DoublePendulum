using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublePendulum
{
    class DoublePendulumModel
    {
        public double previousX2 = -1;
        public double previousY2 = -1;

        internal bool PreviousXNotNull()
        {
            return previousX2 != -1;
        }

        internal void StoreCurrentPoint(double x2, double y2)
        {
            previousX2 = x2;
            previousY2 = y2;
        }
    }
}
