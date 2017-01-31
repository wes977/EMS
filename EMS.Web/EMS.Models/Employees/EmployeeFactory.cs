using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Employees
{
    public static class EmployeeFactory
    {
        public static FulltimeEmployee CreateFullTimeEmployee()
        {
            FulltimeEmployee ft = new FulltimeEmployee();


            return ft;
        }

        public static ParttimeEmployee CreatePartTimeEmployee()
        {
            ParttimeEmployee pt = new ParttimeEmployee();


            return pt;
        }

        public static ContractEmployee CreateContractEmployee()
        {
            ContractEmployee ct = new ContractEmployee();


            return ct;
        }

        public static SeasonalEmployee CreateSeasonalEmployee()
        {
            SeasonalEmployee sn = new SeasonalEmployee();


            return sn;
        }
    }
}
