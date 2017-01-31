using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.DataAccess;
using System.Data;
namespace EMS.Web.ems
{
    public partial class Reports : System.Web.UI.Page
    {

        string report = "";
        DataTable companies;

        Connection database = Connection.Create("conn");
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Determine type of employee being entered, setup page accordingly */
            SetupPageType();
        }

        /// <summary>
        /// Checks the get request and sets up the page
        /// according to if its a fulltime, parttime, seasonal,
        /// or contract employee being created.
        /// </summary>
        private void SetupPageType()
        {
            if (!IsPostBack)
            {
                /* Find what type of user is being edited, else 404 */
                if (Request.HttpMethod == "GET")
                {
                    report = Request.Params["type"];

                    ViewState["type"] = report;

                    if (report == "SR")
                    {
                        reportType.InnerText = "Seniority Report";
                        ReportViews.ActiveViewIndex = 0;
                        WeekPickerMV.Visible = false;
                    }
                    else if (report == "WHW")
                    {
                        reportType.InnerText = "Weekly Hours Worked";
                        ReportViews.ActiveViewIndex = 1;
                        WeekPickerMV.Visible = true;
                    }
                    else if (report == "PR")
                    {
                        reportType.InnerText = "Payroll Report";
                        ReportViews.ActiveViewIndex = 2;
                        WeekPickerMV.Visible = true;
                    }
                    else if (report == "AER")
                    {
                        reportType.InnerText = "Active Employemnt Report";
                        ReportViews.ActiveViewIndex = 3;
                        WeekPickerMV.Visible = false;
                    }
                    else if (report == "IER" && Session["Clearance"].ToString() == "1")
                    {
                        reportType.InnerText = "Inactive Employment Report";
                        ReportViews.ActiveViewIndex = 4;
                        WeekPickerMV.Visible = false;
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/ems/Errors/404.aspx"), true);
                    }


                    // get all companies
                    companies = database.GetCompanyNames();
                    ViewState["companies"] = companies;
                    FillCompanyDropDown(companies);

                }
            }
            else
            {
                report = ViewState["type"].ToString();
                companies = (DataTable)ViewState["companies"];
                FillCompanyDropDown(companies);
            }
        }

        private void FillCompanyDropDown(DataTable table)
        {
            companyDropDown.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                companyDropDown.Items.Add(new ListItem()
                {
                    Text = row[0].ToString()
                });
            }
        }

        protected void btnGenerate_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String CompanyName = "";



            string ReportTimeString = "";
            DateTime ReportTime;
            if (WeekReport.Value != "")
            {
                ReportTime = Convert.ToDateTime(WeekReport.Value);
                if (ReportTime.DayOfWeek == DayOfWeek.Sunday) // Setting the Week to monday 
                {
                    ReportTime = ReportTime.AddDays(-6);
                    ReportTimeString = ReportTime.ToString();
                }
                else if (ReportTime.DayOfWeek == DayOfWeek.Tuesday)
                {
                    ReportTime = ReportTime.AddDays(-1);
                    ReportTimeString = ReportTime.ToString();
                }
                else if (ReportTime.DayOfWeek == DayOfWeek.Wednesday)
                {
                    ReportTime = ReportTime.AddDays(-2);
                    ReportTimeString = ReportTime.ToString();
                }
                else if (ReportTime.DayOfWeek == DayOfWeek.Thursday)
                {
                    ReportTime = ReportTime.AddDays(-3);
                    ReportTimeString = ReportTime.ToString();
                }
                else if (ReportTime.DayOfWeek == DayOfWeek.Friday)
                {
                    ReportTime = ReportTime.AddDays(-4);
                    ReportTimeString = ReportTime.ToString();
                }
                else if (ReportTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    ReportTime = ReportTime.AddDays(-5);
                    ReportTimeString = ReportTime.ToString();
                }
            }

            CompanyName = companyDropDown.SelectedItem.ToString();
            ReportViews.Visible = true;
            switch (report)
            {
                case "SR":
                    reportType.InnerText = "Seniority Report";
                    ReportViews.ActiveViewIndex = 0;

                    database.GetReports(dt, "SeniorityReport", CompanyName);
                    SRGrid.DataSource = dt;
                    SRGrid.DataBind();
                    dt = new DataTable();

                    break;
                case "WHW":
                    reportType.InnerText = "Weekly Hours Worked";
                    ReportViews.ActiveViewIndex = 1;

                    database.GetReports(dt, "WeeklyHoursReport", CompanyName, "FT", ReportTimeString);
                    WHRGridFT.DataSource = dt;
                    WHRGridFT.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "WeeklyHoursReport", CompanyName, "PT", ReportTimeString);
                    WHRGridPT.DataSource = dt;
                    WHRGridPT.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "WeeklyHoursReport", CompanyName, "SN", ReportTimeString);
                    WHRGridSN.DataSource = dt;
                    WHRGridSN.DataBind();
                    dt = new DataTable();

                    WHRReportMes.Text = "For week starting: " + ReportTimeString;

                    break;
                case "PR":
                    reportType.InnerText = "Payroll Report";
                    ReportViews.ActiveViewIndex = 2;


                    database.GetReports(dt, "PayrollReport", CompanyName, "FT", ReportTimeString);
                    PRGridFT.DataSource = dt;
                    PRGridFT.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "PayrollReport", CompanyName, "PT", ReportTimeString);
                    PRGridPT.DataSource = dt;
                    PRGridPT.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "PayrollReport", CompanyName, "SN", ReportTimeString);
                    PRGridSN.DataSource = dt;
                    PRGridSN.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "PayrollReport", CompanyName, "CT", ReportTimeString);
                    PRGridCT.DataSource = dt;
                    PRGridCT.DataBind();
                    dt = new DataTable();

                    PRReportMes.Text = "For week starting: " + ReportTimeString;

                    break;
                case "AER":
                    reportType.InnerText = "Active Employemnt Report";
                    ReportViews.ActiveViewIndex = 3;

                    database.GetReports(dt, "ActiveEmploymentReport", CompanyName, "FT");
                    AERGridFT.DataSource = dt;
                    AERGridFT.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "ActiveEmploymentReport", CompanyName, "PT");
                    AERGridPT.DataSource = dt;
                    AERGridPT.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "ActiveEmploymentReport", CompanyName, "SN");
                    AERGridSN.DataSource = dt;
                    AERGridSN.DataBind();
                    dt = new DataTable();

                    database.GetReports(dt, "ActiveEmploymentReport", CompanyName, "CT");
                    AERGridCT.DataSource = dt;
                    AERGridCT.DataBind();
                    dt = new DataTable();

                    

                    break;
                case "IER":
                    reportType.InnerText = "Inactive Employment Report";
                    ReportViews.ActiveViewIndex = 4;

                    database.GetReports(dt, "InactiveEmploymentReport", CompanyName);
                    IERGrid.DataSource = dt;
                    IERGrid.DataBind();
                    dt = new DataTable();

                    break;
            }




            reportUpdatePanel.Update();
        }
    }
}