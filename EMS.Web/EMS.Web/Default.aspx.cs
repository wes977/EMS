/*
    * Filename: Default.aspx.cs
    *
    * Description:
    * The default login page for both users. 
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
using EMS.Models.Users;

namespace EMS.Web
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// The page load handler 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["Username"] != null)
            {
                Session.Remove("Username");
                Session.Remove("Clearance");
                Session.Remove("LastName");
                Session.Remove("FirstName");
            }
            
            
        }

        /// <summary>
        /// The login button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Connection conn = Connection.Create("conn");
            if (conn != null)
            {
                User user = conn.FindUser(tbUserId.Text, tbPassword.Text);

                if (user != null)
                {
                    Session["Username"] = user.Username;
                    Session["LastName"] = user.LastName;
                    Session["FirstName"] = user.FirstName;
                    Session["Clearance"] = user.Clearance;

                    Response.Redirect("ems/", true);

                }
                else
                {
                    errLabel.Text = "Invalid Username or Password";
                }

            }
            else
            {
                errLabel.Text = "Error retrieving user information";
            }
        }
    }
}