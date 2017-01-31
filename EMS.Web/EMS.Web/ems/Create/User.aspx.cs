
/*
    * Filename: User.aspx.cs
    *
    * Description:
    * Holds the code behind for the Create/User page
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
using EMS.DataAccess;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Web.ems.Create
{
    public partial class User : System.Web.UI.Page
    {

        /// <summary>
        /// Page load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Match password server validation.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void matchPswd_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string pswd = pswdBox.Text;
            string validationPswd = renterPswdBox.Text;

            if (pswd != validationPswd)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true; 
            }
        }


        /// <summary>
        /// Used for validation of the username.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void usrNameCheckLength_ServerValidate(object source, ServerValidateEventArgs args)
        {
            const int MAX_LENGTH = 15;

            string usrName = userBox.Text;

            if (usrName.Length > MAX_LENGTH)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }

        }

        /// <summary>
        /// The click handler for the submit button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool result = false;

            Connection conn = Connection.Create("conn");

            int secClearence = (lstUserType.SelectedIndex);

            result = conn.AddUser(userBox.Text, firstNameBox.Text, lastNameBox.Text, secClearence, pswdBox.Text);
            
            if (result == false)
            {
                errLbl.Text = "User not added.";
            }
            else
            {
                errLbl.ForeColor = System.Drawing.Color.Green;
                errLbl.Text = "User added.";
            }
        }


        /// <summary>
        /// Checks the usernames length.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void Length_Validate(object source, ServerValidateEventArgs args)
        {
            string value = args.Value;

            if (value.Length > 40)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}