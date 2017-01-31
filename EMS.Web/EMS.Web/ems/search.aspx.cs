using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EMS.DataAccess;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using EMS.Models.Employees;
using System.Text.RegularExpressions;

namespace EMS.Web.ems
{

    /// <summary>
    /// Page that involves searching for employees
    /// </summary>
    public partial class search : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Searches the specified q.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <param name="l">The l.</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string Search(string q, string l)
        {
            string response = "";
            Connection conn = Connection.Create("conn");
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Connection.SearchResults> queryResult = conn.QueryEmployeesByName(q, int.Parse(l));
            if(queryResult != null)
            {
                response = serializer.Serialize(queryResult);
            }
            else
            {
                response = null;
            }

            return response;
        }

        /// <summary>
        /// Handles the ServerClick event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (tbSearch.Value.Length > 0)
            {

                Connection conn = Connection.Create("conn");
                Connection.SearchParameters param = Connection.SearchParameters.All;

                switch (search_param.Value)
                {
                    case "fname":
                        param = Connection.SearchParameters.FirstName;
                        break;
                    case "lname":
                        param = Connection.SearchParameters.LastName;
                        break;
                    case "sin":
                        param = Connection.SearchParameters.SIN;
                        break;
                }

                List<Employee> employees = conn.QueryEmployees(tbSearch.Value, param);
                searchRepeater.DataSource = employees;
                searchRepeater.DataBind();
                searchArea.Visible = true;

            }
            else
            {
                searchArea.Visible = false;
            }
        }

        /// <summary>
        /// Handles the ItemDataBound event of the searchRepeater control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void searchRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var emp = e.Item.DataItem as Employee;

                var control = e.Item.FindControl("employee") as EmployeeView;
                control.Employee = emp;
                if(Session["Clearance"].ToString() == "1")
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