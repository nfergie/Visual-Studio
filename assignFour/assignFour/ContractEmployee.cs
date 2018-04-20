using System;
using System.Collections.Generic;
using System.Text;

namespace assignFour
{
    class ContractEmployee : HourlyEmployee
    {
        public bool Paid
        {
            get; set;
        }

       public ContractEmployee(string name, int employeeId, double hourlyRate)
            : base(name, employeeId, hourlyRate)
        {
            Paid = false;
        }

        public override void PayEmployee()
        {
            if (!Paid)
            {
                Console.WriteLine("You Paid {0} : ${1:0.00}", Name, HourlyRate);
                Paid = true;
            }
            else
            {
                Console.WriteLine("You Paid {0} : $0.00", Name);
            }
        }
    }
}
