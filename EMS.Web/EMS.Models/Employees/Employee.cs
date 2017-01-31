using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Employees
{
    public enum EmployeeType
    {
        FullTimeEmployee,
        PartTimeEmployee,
        ContractEmployee,
        SeasonalEmployee
    }

    public abstract class Employee
    {

        internal Employee()
        {
        }

        public int ID { get;  set; }

        public string EmployedWithCompany { get; set; }

        public EmployeeType Type { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public string SocialInsuranceNumber { get; set; }

        public List<string> CompanyName { get; set; }

        public string ReasonForLeaving { get; set; }
    }
}
