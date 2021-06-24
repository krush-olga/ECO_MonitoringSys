using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawChartModule.Models
{
    public struct CustomPoint<T>
    {
        public T X;
        public double Y;
        public CustomPoint(T X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
