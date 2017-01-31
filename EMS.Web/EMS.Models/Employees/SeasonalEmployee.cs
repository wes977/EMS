using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Employees
{
    public enum Season
    {
        Winter,
        Spring,
        Summer,
        Fall
    }

    public class SeasonalEmployee : Employee
    {

        internal SeasonalEmployee()
        {
            Type = EmployeeType.SeasonalEmployee;
        }

        public string Season { get; set; }

        public float? PiecePay { get; set; }

        public string SeasonYear { get; set; }
        
    }
}
