using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Employees
{
    public class ContractEmployee : Employee
    {
        internal ContractEmployee()
        {
            Type = EmployeeType.ContractEmployee;
        }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractStopDate { get; set; }

        public float? FixedContractAmount { get; set; }

    }
}
