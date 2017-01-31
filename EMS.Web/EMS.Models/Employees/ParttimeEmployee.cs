using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Employees
{
    public class ParttimeEmployee : Employee
    {
        internal ParttimeEmployee()
        {
            Type = EmployeeType.PartTimeEmployee;
        }

        public DateTime DateOfHire { get; set; }

        public DateTime DateOfTermination { get; set; }

        public float? HourlyRate { get; set; }

    }
}
