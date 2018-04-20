using System;
using System.Collections.Generic;
using System.IO;

namespace notes0419
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shirt> shirts = new List<Shirt>();
            for(int i = 0; i <10001; i++)
            {
                Shirt newShirt = new Shirt();
                shirts.Add(newShirt);
            }

            using (StreamWriter writer = new StreamWriter(File.OpenWrite("test_csv.csv")))
            {
                foreach 
            }
        }
    }
}
