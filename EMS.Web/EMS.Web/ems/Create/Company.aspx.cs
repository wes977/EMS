
/*
    * Filename: Company.aspx.cs
    *
    * Description:
    * Holds the code behind for the Create/Company page
    *
    * Authors:
    * Kyle Marshall
    * Kyle Kreutzer
    *  Wes Thompson
    * Colin Mills
    *
    * Date: 2016-04-21   
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.DataAccess;


namespace EMS.Web.ems.Create
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void ClearForm()
        {
            txtCity.Text = "";
            txtCompanyName.Text = "";
            txtCompanyStreet.Text = "";
            txtCountry.Text = "";
            txtPhone.Text = "";
            txtPostalCode.Text = "";
            txtFax.Text = "";
            txtYear.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool result = false;

            Connection conn = Connection.Create("conn");

            result = conn.AddCompany(txtCompanyName.Text, txtCompanyStreet.Text, txtPostalCode.Text, txtCity.Text,
                                txtCountry.Text, txtPhone.Text, txtFax.Text, txtYear.Text);

            if (result == true)
            {
                errLbl.ForeColor = System.Drawing.Color.Green;
                errLbl.Text = "Company added.";

            }
            else
            {
                errLbl.Text = "Company not added";
            }





        }

    }
}