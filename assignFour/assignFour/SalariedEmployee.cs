using System;
using System.Collections.Generic;
using System.Text;

namespace assignFour
{
    class SalariedEmployee : HourlyEmployee
    {
        public SalariedEmployee(string name, int employeeId, double hourlyRate) 
            : base(name, employeeId, hourlyRate)
        {

        }
        public override void PayEmployee()
        {
            double pay = HourlyRate * 40;
            Console.WriteLine("You Paid {0} : ${1}", Name, pay);
        }
    }
}
