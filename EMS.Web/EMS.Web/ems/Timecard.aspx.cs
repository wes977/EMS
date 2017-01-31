using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.DataAccess;
using EMS.Models.Employees;

namespace EMS.Web
{
    public partial class Timecard : System.Web.UI.Page
    {
        Employee employee = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    /* Find what type of user is being edited, else 404 */
            //    if (Request.HttpMethod == "GET")
            //    {
            //        string type = Request.Params["employeeid"];

            //        if (type == "fulltime")
            //        {
            //            employeeType.InnerText = "Full Time Employee Timecard";
            //            employeeViews.ActiveViewIndex = 0;
            //        }
            //        else if (type == "parttime")
            //        {
            //            employeeType.InnerText = "Part Time Employee Timecard";
            //            employeeViews.ActiveViewIndex = 1;
            //        }
            //        else if (type == "seasonal")
            //        {
            //            employeeType.InnerText = "Seasonal Employee";
            //            employeeViews.ActiveViewIndex = 2;
            //        }
            //        else if (type == "contract" && Session["Clearance"].ToString() == "1")
            //        {
            //            employeeType.InnerText = "Contract Employee Timecard";
            //            employeeViews.ActiveViewIndex = 3;
            //        }
            //        else
            //        {
            //            Response.Redirect(ResolveUrl("~/ems/Errors/404.aspx"));
            //        }
            //    }
            //}
            System.Collections.Specialized.NameValueCollection postedValues = Request.Params;
            int id = 0;
            if (int.TryParse(postedValues["employeeid"], out id))
            {
                Connection conn = Connection.Create("conn");
                employee = conn.GetEmployee(id);
                employeeType.InnerText = "Time Card Entry For " + employee.FirstName + " " + employee.LastName;
            }
            else
            {
                Response.Redirect("~/ems/", true);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Connection conn = Connection.Create("conn");
            string DateTimecardString = DateTimecard.Value;
            string StartTimeString = StartTimecard.Value;
            string EndTimeString =  EndTimecard.Value;
            if (DateTimecardString != "" && StartTimeString != "" && EndTimeString != "")
            {
                ERROR.InnerText = "";
                DateTime TimeCardDate = DateTime.Parse(DateTimecardString);
                DateTime TimeCardStartTime = DateTime.Parse(StartTimeString);
                DateTime TimeCardEndTime = DateTime.Parse(EndTimeString);
                int STHours = TimeCardStartTime.Hour;
                int STMinutes = TimeCardStartTime.Minute;
                int ETHours = TimeCardEndTime.Hour;
                int ETMinutes = TimeCardEndTime.Minute;
                int TimeWorkedHours = 0;
                int TimeWorkedMinutes = 0;
                if (ETMinutes >= STMinutes)     // This is for if the times are 1:33  and 2:15 to make sure that it take an extra hour away to make it zero
                {
                    TimeWorkedHours = ETHours - STHours;
                    TimeWorkedMinutes = ETMinutes - STMinutes;
                }
                else {
                    TimeWorkedHours = ETHours - STHours - 1;
                    TimeWorkedMinutes = (60 - STMinutes) + ETMinutes;
                }

                decimal Hours = 0.0M;
                decimal Minutes = 0.0M;
                Minutes = ((decimal)TimeWorkedMinutes / 60);
                Minutes = Math.Round(Minutes, 2);
                Hours = TimeWorkedHours + Minutes;
                conn.AddTimecard(employee.ID, TimeCardDate, Hours);
                EndTimecard.Value = "";
                StartTimecard.Value = "";
                DateTimecard.Value = "";
            }
            else
            {
                ERROR.InnerText = "Please fill all fields";
            }
        }
    }
}