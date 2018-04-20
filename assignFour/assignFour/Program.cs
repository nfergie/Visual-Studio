using System;
using System.Collections.Generic;

namespace assignFour
{
    class Program
    {
        public static void ListEmployees(List<HourlyEmployee> employees)
        {
            foreach(HourlyEmployee employee in employees)
            {
                Console.WriteLine("{0}. {1} Pay: ${2:0.00}, Time Off: {3:0.00}hrs",
                    employee.EmployeeId, employee.Name, employee.HourlyRate, employee.TimeOff);
            }
        }

        public static void FireEmployee(List<HourlyEmployee> employees, int employeeId)
        {
            for(int i = 0; i < employees.Count; i++)
            {
                if(employees[i].EmployeeId == employeeId)
                {
                    employees.RemoveAt(i);
                    break;
                }
            }
        }

        public static void HireEmployee(List<HourlyEmployee> employees, string employeeInput)
        {
            string[] input = employeeInput.Split(',');
            string type = input[0].Trim().ToLower();
            string name = input[1].Trim();
            double hourlyRate = Convert.ToDouble(input[2].Trim());

            Random rnd = new Random();
            int employeeId = rnd.Next(1000);
            bool result = employees.Exists(employee => employee.EmployeeId == employeeId);
            while (result)
            {
                employeeId = rnd.Next(1000);
                result = employees.Exists(employee => employee.EmployeeId == employeeId);
            }



            switch (type)
            {
                case "hourly":
                    employees.Add(new HourlyEmployee(name, employeeId, hourlyRate));
                    break;
                case "contract":
                    employees.Add(new ContractEmployee(name, employeeId, hourlyRate));
                    break;
                case "salary":
                    employees.Add(new SalariedEmployee(name, employeeId, hourlyRate));
                    break;
                default:
                    Console.WriteLine("incorrect type");
                    break;
            }
        }

        public static void Work(List<HourlyEmployee> employees, string workInput)
        {
            string[] input = workInput.ToLower().Split(',');
            int id = Convert.ToInt32(input[0]);
            double hours = Convert.ToDouble(input[1]);
            HourlyEmployee result = employees.Find(employee => employee.EmployeeId == id);
            result.HoursWorked = hours;
        }

        public static void Future(List<HourlyEmployee> employees)
        {
            foreach(HourlyEmployee employee in employees)
            {
                employee.PayEmployee();
            }
        }

        public static void ViewEmployee(List<HourlyEmployee> employees, int employeeId)
        {
            foreach(HourlyEmployee employee in employees)
            {
                if(employee.EmployeeId == employeeId)
                {
                    Console.WriteLine("{0}. {1} Pay: ${2:0.00}, Time Off: {3:0.00}hrs",
                    employee.EmployeeId, employee.Name, employee.HourlyRate, employee.TimeOff);
                }
            }
        }

        static void Main(string[] args)
        {
            List<HourlyEmployee> employees = new List<HourlyEmployee>();

            Console.WriteLine("Welcome to the HR Program");
            Console.WriteLine("The available commands are List, View, Hire, Fire, Work, Future");
            Console.WriteLine("Type Exit to close the program.");

            Console.WriteLine("Please Enter Command: ");
            string input = Console.ReadLine().ToLower();

            while(input != "exit")
            {
                switch (input)
                {
                    case "list":
                        if(employees.Count == 0)
                        {
                            Console.WriteLine("Please Hire Employees to list them.");
                        }
                        else
                        {
                            ListEmployees(employees);
                        }
                        break;
                    case "view":
                        if (employees.Count == 0)
                        {
                            Console.WriteLine("Please Hire Employees to view them.");
                        }
                        else
                        {
                            Console.WriteLine("Enter Employee ID");
                            int employeeId = Convert.ToInt32(Console.ReadLine());
                            ViewEmployee(employees, employeeId);
                        }
                        break;
                    case "fire":
                        if (employees.Count == 0)
                        {
                            Console.WriteLine("Please Hire Employees to fire them.");
                        }
                        else
                        {
                            Console.WriteLine("Enter Employee ID");
                            int employeeId = Convert.ToInt32(Console.ReadLine());
                            FireEmployee(employees, employeeId);
                        }
                        break;
                    case "hire":
                        Console.WriteLine("Please enter type: (salary/hourly/contract), name, pay");
                        string employeeInput = Console.ReadLine();
                        HireEmployee(employees, employeeInput);
                        break;
                    case "future":
                        Future(employees);
                        break;
                    case "work":
                        if (employees.Count == 0)
                        {
                            Console.WriteLine("Please Add Employees to Enter Work Hours");
                        }
                        else
                        {
                            Console.WriteLine("Please enter EmployeeID, hours");
                            string workInput = Console.ReadLine();
                            Work(employees, workInput);
                        }
                        break;
                    default:
                        Console.WriteLine("That is not a valid Command");
                        break;
                }
                Console.WriteLine("Please Enter Command");
                input = Console.ReadLine();
            }
        }
    }
}
