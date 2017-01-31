using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Employees
{
    public class FulltimeEmployee : Employee
    {
        internal FulltimeEmployee()
        {
            Type = EmployeeType.FullTimeEmployee;
        }

        public DateTime DateOfHire { get; set; }


        public DateTime DateOfTermination { get; set; }

        public float? Salary { get; set; }

    }
}