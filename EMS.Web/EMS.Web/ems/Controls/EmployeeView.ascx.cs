using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.Models.Employees;

namespace EMS.Web
{
    public enum Field
    {
        FirstName,
        LastName,
        SocialInsuranceNumber,
        EmployedWithCompany,
        DateOfHire,
        DateOfTermination,
        DateOfBirth,
        ReasonForLeaving,
        Salary,
        PiecePay,
        HourlyRate,
        FixedContractAmount,
        Season,
        SeasonYear
    }

    public class EmployeeEditedEventArgs
    {
        public int ID { get; private set; }
        public EmployeeType EmployeeType { get; private set; }
        public Field EditedField { get; private set; }
        public string NewValue { get; set; }

        public EmployeeEditedEventArgs(int id, EmployeeType type, Field editedField, string newValue)
        {
            ID = id;
            EmployeeType = type;
            EditedField = editedField;
            NewValue = newValue;
        }
    }

    public enum UserViewLevel
    {
        GeneralUser,
        AdminUser
    }

    public partial class EmployeeView : System.Web.UI.UserControl
    {
        public UserViewLevel UserViewLevel { get; set; }
        public Employee Employee { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(Employee != null)
            {
                // fill base employee information
                FillBaseEmployee();

                // display 
                if(Employee is FulltimeEmployee)
                {
                    FulltimeEmployee ft = Employee as FulltimeEmployee;
                    FillFullTimeEmployee(ft);
                }
                else if(Employee is ParttimeEmployee)
                {
                    ParttimeEmployee pt = Employee as ParttimeEmployee;
                    FillPartTimeEmployee(pt);
                }
                else if(Employee is SeasonalEmployee)
                {
                    SeasonalEmployee sn = Employee as SeasonalEmployee;
                    FillSeasonalEmployee(sn);
                }
                else if(Employee is ContractEmployee)
                {
                    ContractEmployee ct = Employee as ContractEmployee;
                    FillContractEmployee(ct);
                }
            }


            this.DataBinding += EmployeeView_DataBinding;

        }

        private void EmployeeView_DataBinding(object sender, EventArgs e)
        {
            if (Employee != null)
            {
                // fill base employee information
                FillBaseEmployee();

                // display 
                if (Employee is FulltimeEmployee)
                {
                    FulltimeEmployee ft = Employee as FulltimeEmployee;
                    FillFullTimeEmployee(ft);
                }
                else if (Employee is ParttimeEmployee)
                {
                    ParttimeEmployee pt = Employee as ParttimeEmployee;
                    FillPartTimeEmployee(pt);
                }
                else if (Employee is SeasonalEmployee)
                {
                    SeasonalEmployee sn = Employee as SeasonalEmployee;
                    FillSeasonalEmployee(sn);
                }
                else if (Employee is ContractEmployee)
                {
                    ContractEmployee ct = Employee as ContractEmployee;
                    FillContractEmployee(ct);
                }
            }
        }

        private void FillBaseEmployee()
        {
            fname.InnerText = Employee.FirstName;
            lname.InnerText = Employee.LastName;
            sin.InnerText = Employee.SocialInsuranceNumber;

            if(Employee.DateOfBirth == DateTime.MinValue)
            {
                dob.InnerHtml = "<strong class='text-danger'>N/A</strong>";
            }
            else
            {
                dob.InnerText = Employee.DateOfBirth.ToShortDateString();
            }

            
            empWithCompany.InnerText = Employee.EmployedWithCompany;

            foreach(string c in Employee.CompanyName)
            {
                company.InnerText += c;

                if(c == Employee.CompanyName[Employee.CompanyName.Count - 1])
                {
                    company.InnerText += ", ";
                }
            }

            rfl.InnerText = Employee.ReasonForLeaving;
        }

        private void FillFullTimeEmployee(FulltimeEmployee e)
        {
            type.InnerText = "Full Time";
            if(e.DateOfHire == DateTime.MinValue)
            {
                doh.InnerText = "N/A";
            }
            else
            {
                doh.InnerText = e.DateOfHire.ToShortDateString();
            }
            
            if(e.DateOfTermination == DateTime.MinValue)
            {
                dot.InnerText = "N/A";
            }
            else
            {
                dot.InnerText = e.DateOfTermination.ToShortDateString();
            }
            
            salary.InnerText = e.Salary.ToString();
        }

        private void FillPartTimeEmployee(ParttimeEmployee e)
        {
            type.InnerText = "Part Time";

            if (e.DateOfHire == DateTime.MinValue)
            {
                doh.InnerText = "N/A";
            }
            else
            {
                doh.InnerText = e.DateOfHire.ToShortDateString();
            }

            if (e.DateOfTermination == DateTime.MinValue)
            {
                dot.InnerText = "N/A";
            }
            else
            {
                dot.InnerText = e.DateOfTermination.ToShortDateString();
            }

            hourlyRate.InnerText = e.HourlyRate.ToString();
        }
        
        private void FillSeasonalEmployee(SeasonalEmployee e)
        {
            type.InnerText = "Seasonal";
            season.InnerText = e.Season;
            seasonYear.InnerText = e.SeasonYear;
            piecePay.InnerText = e.PiecePay.ToString();
        }

        private void FillContractEmployee(ContractEmployee e)
        {
            type.InnerText = "Contract";

            if(e.ContractStartDate == DateTime.MinValue)
            {
                csd.InnerHtml = "<strong class='text-danger'>N/A</strong>";
            }
            else
            {
                csd.InnerText = e.ContractStartDate.ToShortDateString();
            }

            if(e.ContractStopDate == DateTime.MinValue)
            {
                ced.InnerHtml = "<strong class='text-danger'>N/A</strong>";
            }
            else
            {
                ced.InnerText = e.ContractStopDate.ToShortDateString();
            }
         
            fca.InnerText = e.FixedContractAmount.ToString();
        }

        protected void btn1_ServerClick(object sender, EventArgs e)
        {
            
        }

    }
}