using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.DataAccess;
using EMS.Models.Employees;

namespace EMS.Web.ems.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ViewEmployee : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = 0;
            if (Session["Clearance"] != null)
            {
                if (Request.Params["id"] != null && int.TryParse(Request.Params["id"], out id))
                {
                    Connection conn = Connection.Create("conn");
                    // get the employee out of the database
                    employee.Employee = conn.GetEmployee(id);

                    if (Session["Clearance"].ToString() == "1")
                    {
                        employee.UserViewLevel = UserViewLevel.AdminUser;
                    }
                    else
                    {
                        employee.UserViewLevel = UserViewLevel.GeneralUser;
                    }

                }
            }
        }

    }
}