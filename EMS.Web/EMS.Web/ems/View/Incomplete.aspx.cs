using System;
using System.Collections.Generic;
using EMS.DataAccess;
using System.Web.UI.WebControls;
using EMS.Models.Employees;

namespace EMS.Web.ems.View
{
    public partial class Incomplete : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection conn = Connection.Create("conn");

            if(conn.InvalidEmployeeCount() > 0)
            {
                List<Employee> employees = conn.QueryEmployees(null, Connection.SearchParameters.Invalid);
                incompleteRepeater.DataSource = employees;

                incompleteRepeater.DataBind();
                pageHead.InnerHtml = "<em>Incomplete Employees<em>";
                incompleteRepeater.Visible = true;
            }
            else
            {
                pageHead.InnerHtml = "<em>No Incomplete Employees<em>";
                incompleteRepeater.Visible = false;
            }

        }

        /// <summary>
        /// Handles the ItemDataBound event of the incompleteRepeater control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void incompleteRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var emp = e.Item.DataItem as Employee;

                var control = e.Item.FindControl("employee") as EmployeeView;
                control.Employee = emp;
                if (Session["Clearance"].ToString() == "1")
                {
                    control.UserViewLevel = UserViewLevel.AdminUser;
                }
                else
                {
                    control.UserViewLevel = UserViewLevel.GeneralUser;
                }

                control.DataBind();

            }
        }
    }
}