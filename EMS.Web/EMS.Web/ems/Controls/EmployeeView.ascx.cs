using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


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

        public Field EditedField { get; private set; }
        public string NewValue { get; set; }


    }

    public enum UserViewLevel
    {
        GeneralUser,
        AdminUser
    }

    public partial class EmployeeView : System.Web.UI.UserControl
    {
        public UserViewLevel UserViewLevel { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {



            this.DataBinding += EmployeeView_DataBinding;

        }

        private void EmployeeView_DataBinding(object sender, EventArgs e)
        {

        }

        private void FillBaseEmployee()
        {

        }



        



        protected void btn1_ServerClick(object sender, EventArgs e)
        {
            
        }

    }
}