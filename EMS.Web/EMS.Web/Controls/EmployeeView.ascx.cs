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
        public EmployeeType EmployeeType { get; private set; }
        public Field EditedField { get; private set; }
        public string NewValue { get; set; }

        public EmployeeEditedEventArgs(EmployeeType type, Field editedField, string newValue)
        {
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

    public delegate void EmployeeEditedEventHandler(object sender, EmployeeEditedEventArgs e);

    public partial class EmployeeView : System.Web.UI.UserControl
    {
        public UserViewLevel UserViewLevel { get; set; }
        public Employee Employee { get; set; }

        public event EmployeeEditedEventHandler OnEmployeeSave;

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
        }

        private void FillBaseEmployee()
        {
            fname.InnerText = Employee.FirstName;
            lname.InnerText = Employee.LastName;
            sin.InnerText = Employee.SocialInsuranceNumber;
            dob.InnerText = Employee.DateOfBirth.ToShortDateString();
            empWithCompany.InnerText = Employee.EmployedWithCompany;

            foreach(string c in Employee.CompanyName)
            {
                company.InnerText += c;

                if(c == Employee.CompanyName[Employee.CompanyName.Count - 1])
                {
                    company.InnerText += ", ";
                }
            }
        }

        private void FillFullTimeEmployee(FulltimeEmployee e)
        {
            type.InnerText = "Full Time";
            doh.InnerText = e.DateOfHire.ToShortDateString();
            dot.InnerText = e.DateOfTermination.ToShortDateString();
            salary.InnerText = e.Salary.ToString();
        }

        private void FillPartTimeEmployee(ParttimeEmployee e)
        {
            type.InnerText = "Part Time";
            doh.InnerText = e.DateOfHire.ToShortDateString();
            dot.InnerText = e.DateOfTermination.ToShortDateString();
            hourlyRate.InnerText = e.HourlyRate.ToString();
        }
        
        private void FillSeasonalEmployee(SeasonalEmployee e)
        {
            type.InnerText = "Seasonal Time";
            season.InnerText = Enum.GetName(typeof(Season), e.Season);
            seasonYear.InnerText = e.SeasonYear;
            piecePay.InnerText = e.PiecePay.ToString();
        }

        private void FillContractEmployee(ContractEmployee e)
        {
            type.InnerText = "Contract Time";
            csd.InnerText = e.ContractStartDate.ToShortDateString();
            ced.InnerText = e.ContractStopDate.ToShortDateString();
            fca.InnerText = e.FixedContractAmount.ToString();
        }
        
    }
}