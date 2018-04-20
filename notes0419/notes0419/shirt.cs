using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace notes0419
{
    class Shirt
    {
        public int Size
        {
            get; set;
        } 

        public string Color
        {
            get; set;
        }

        public bool Gender
        {
            get; set;
        }

        public NeckType Neck
        {
            get; set;
        }

        public enum NeckType
        {
            vneck, regular
        };

        public Shirt()
        {
            Random rnd = new Random();
   
            int color1 = rnd.Next(0, 256);
            int color2 = rnd.Next(0, 256);
            int color3 = rnd.Next(0, 256);

            Size = rnd.Next(0, 51);

            Gender = Convert.ToBoolean(rnd.Next(0, 2));

            Color = "(" + color1 + ", " + color2 + ", "+ color3 + ")";

            Neck = (NeckType)rnd.Next(0, 2);
        }

        public void WriteCsv(StreamWriter writer)
        {

        }
    }
}
