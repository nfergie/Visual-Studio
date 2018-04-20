using System;
using System.Collections.Generic;
using System.Text;

namespace assignFour
{
    class HourlyEmployee
    {
        public string Name
        {
            get; set;
        }

        public int EmployeeId
        {
            get; set;
        }

        public double HourlyRate
        {
            get; set;
        }

        public double HoursWorked
        {
            get; set;
        }

        public double TimeOff
        {
            get; set;
        }

        public HourlyEmployee(string name, int employeeId, double hourlyRate)
        {
            Name = name;
            EmployeeId = employeeId;
            HourlyRate = hourlyRate;
            HoursWorked = 0;
            TimeOff = 0;
        }

        public virtual void PayEmployee()
        {
            double pay;

            if(HoursWorked > 40)
            {

                pay = (HourlyRate * 40) + ((HourlyRate * 1.5) * (HoursWorked - 40));
            }
            else
            {
                pay = HoursWorked * HourlyRate;
            }

            Console.WriteLine("You Paid {0} : ${1:0.00}", Name, pay);
        }
    }
}
