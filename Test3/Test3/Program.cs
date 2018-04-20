using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{
    class Point2D
    {
        private int x;
        private int y;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public Point2D()
        {
            x = 0;
            y = 0;
        }

        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static void Main(string[] args)
        {
            Point2D point = new Point2D(5, 3);
            Console.WriteLine("{0}, {1}", point.X, point.Y);
            Console.ReadKey();
        }
    }
}
