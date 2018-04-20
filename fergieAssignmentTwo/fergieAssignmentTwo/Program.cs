using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fergieAssignmentTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            float itemPrice;
            string priceString;
            string itemName;

            Console.Write("Please input item Name: ");
            itemName = Console.ReadLine();

            Console.Write("Please input item Price: ");
            priceString = Console.ReadLine();
            if (priceString.Contains("$"))
            {
                itemPrice = float.Parse(priceString.Substring(1));
            }
            else
            {
                itemPrice = float.Parse(priceString);
            }   
            

            Console.Write("{0} costs ${1:0.00}", itemName, itemPrice);

            Console.ReadKey();
        }
    }
}
