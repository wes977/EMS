/*
* Name: Employee.aspx
*
* Programmers: Kyle Kreutzer, Kyle Marshall, Colin Mills, Wes Thompson
*
* Date: 04/21/2016
*
* Description:
* Holds the page that is responsible for creating
* and validating new employees.
* 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using EMS.DataAccess;
using EMS.Models.Employees;
using EMS.Models;

namespace EMS.Web.ems.Create
{
    /// <summary>
    /// Allows the entry of a new employee into the system, allowing
    /// fields to be specified like date of birth, hire etc.
    /// </summary>
    public partial class Employee : System.Web.UI.Page
    {
        Connection database = Connection.Create("conn");

        /// <summary>
        /// Loads up the page, checking the clearance of the
        /// user accessing the page. If a post back is performed, this
        /// method will validate the employee being entered, returning
        /// results back to the user to be displayed.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">The event args</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    /* Determine type of employee being entered, setup page accordingly */
                    SetupPageType();

                    /* Disable admin fields if general user is accessing page */
                    if (Session["Clearance"].ToString() == "2")
                    {
                        DisableAdminFields();
                    }

                    /* Get and store the list of companies */
                    lstCompanies.DataTextField = "name";
                    lstCompanies.DataSource = database.GetCompanyNames();
                    lstCompanies.DataBind();
                }
                else
                {
                    /* Validate, insert if valid */
                    if (ValidateEmployee() == true)
                    {
                        InsertEmployee();
                        ClearFields();
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = "Employee added.";
                    }
                }
            }
            catch (SqlException)
            {
                lblStatus.ForeColor = System.Drawing.Color.DarkRed;
                lblStatus.Text = "An internal database error occured creating the employee.";
            }
            catch (Exception)
            {
                lblStatus.ForeColor = System.Drawing.Color.DarkRed;
                lblStatus.Text = "An unknown error occured. Please contact system administrator";
            }
        }

        private void ClearFields()
        {
            firstName.Text = "";
            lastName.Text = "";
            SIN.Text = "";
            dateOfBirth.Value = "";
            ftDateOfTerm.Value = "";
            ftHireDate.Value = "";
            ptHireDate.Value = "";
            ptDateOfTerm.Value = "";
            conEndDate.Value = "";
            conStartDate.Value = "";
            txtMoney.Text = "";
            seasonYear.Value = "";
        }

        /// <summary>
        /// Disables fields only deemed to be accessible
        /// by the administrator in the form.
        /// </summary>
        private void DisableAdminFields()
        {
            /* Disable fields deemed only accessible by admin */
            txtMoney.Enabled = false;
            ftDateOfTerm.Disabled = true;
            ptDateOfTerm.Disabled = true;
        }

        /// <summary>
        /// Determines the type of employee that is being created,
        /// than inserts them into the database using the data access
        /// layer object.
        /// </summary>
        private void InsertEmployee()
        {
            string clearance = Session["Clearance"].ToString();
            string employeeType = Request.Params["type"];

            switch (employeeType)
            {
                case "fulltime":
                    database.CreateEmployee(firstName.Text, lastName.Text, SIN.Text, txtMoney.Text,
                        lstCompanies.SelectedValue, dateOfBirth.Value, ftHireDate.Value, ftDateOfTerm.Value,
                        reasonForLeaving.SelectedValue, employeeType, null, null, clearance);
                    break;

                case "parttime":
                    database.CreateEmployee(firstName.Text, lastName.Text, SIN.Text, txtMoney.Text,
                       lstCompanies.SelectedValue, dateOfBirth.Value, ptHireDate.Value, ptDateOfTerm.Value,
                       reasonForLeaving.SelectedValue, employeeType, null, null, clearance);
                    break;

                case "contract":
                    database.CreateEmployee(firstName.Text, lastName.Text, SIN.Text, txtMoney.Text,
                        lstCompanies.SelectedValue, dateOfBirth.Value, conStartDate.Value, conEndDate.Value,
                        reasonForLeaving.SelectedValue, employeeType, null, null, clearance);
                    break;

                case "seasonal":
                    database.CreateEmployee(firstName.Text, lastName.Text, SIN.Text, txtMoney.Text,
                        lstCompanies.SelectedValue, dateOfBirth.Value, conStartDate.Value, conEndDate.Value,
                        reasonForLeaving.SelectedValue, employeeType, season.Text, seasonYear.Value, clearance);
                    break;
            }
        }


        /// <summary>
        /// Validates the employee that has been submitted on post back.
        /// If any errors are detected, the errors will be displayed to the
        /// user in a list box.
        /// </summary>
        /// <returns>bool -> True if valid, false otherwise</returns>
        private bool ValidateEmployee()
        {
            bool hasErrors = false;

            EmployeeValidator validator = new EmployeeValidator();

            string employeeType = Request.Params["type"];

            /* Validate common employee attributes */
            validator.ValidateName(firstName.Text, Name.FirstName);
            validator.ValidateName(lastName.Text, Name.LastName);

            /* Validate money if admin only
            */
            if (Session["Clearance"].ToString() == "1")
            {
                validator.ValidatePay(txtMoney.Text);
            }

            /* Validate based on the type of employee */
            switch (employeeType)
            {
                case "fulltime":
                    validator.CheckDigit(SIN.Text);
                    validator.ValidateBirthDate(dateOfBirth.Value, EmployeeType.FullTime);
                    validator.ValidateDates(ftHireDate.Value, ftDateOfTerm.Value);
                    break;

                case "parttime":
                    validator.CheckDigit(SIN.Text);
                    validator.ValidateBirthDate(dateOfBirth.Value, EmployeeType.PartTime);
                    validator.ValidateDates(ptHireDate.Value, ptDateOfTerm.Value);
                    break;

                case "seasonal":
                    validator.CheckDigit(SIN.Text);
                    validator.ValidateBirthDate(dateOfBirth.Value, EmployeeType.Seasonal);
                    validator.ValidateSeasonYear(seasonYear.Value);
                    break;

                case "contract":
                    validator.ValidateBusinessNumber(SIN.Text, database.GetDateOfIncorporation(lstCompanies.SelectedValue));
                    validator.ValidateBirthDate(dateOfBirth.Value, EmployeeType.Contract);
                    validator.ValidateDates(conStartDate.Value, conEndDate.Value);
                    break;
            }

            hasErrors = validator.HasErrors;

            if (hasErrors)
            {
                /* Show errors to the user */
                errorPanel.Visible = true;
                lstErrors.DataSource = validator.ErrorList;
                lstErrors.DataBind();
            }
            else
            {
                lstErrors.Visible = false;
            }

            return !hasErrors;
        }


        /// <summary>
        /// Checks the get request and sets up the page
        /// according to if its a fulltime, parttime, seasonal,
        /// or contract employee being created.
        /// </summary>
        private void SetupPageType()
        {
            /* Find what type of user is being edited, else 404 */
            if (Request.HttpMethod == "GET")
            {
                string type = Request.Params["type"];

                if (type == "fulltime")
                {
                    /* Change labels for full time */
                    employeeType.InnerText = "Full Time Employee";
                    lblMoney.InnerText = "Salary";
                    employeeViews.ActiveViewIndex = 0;
                }
                else if (type == "parttime")
                {
                    /* Change labels for part time */
                    employeeType.InnerText = "Part Time Employee";
                    lblMoney.InnerText = "Hourly Rate";
                    employeeViews.ActiveViewIndex = 1;
                }
                else if (type == "seasonal")
                {
                    /* Change labels for seasonal*/
                    employeeType.InnerText = "Seasonal Employee";
                    lblMoney.InnerText = "Piece Pay";
                    panelReasonForLeaving.Visible = false;
                    employeeViews.ActiveViewIndex = 2;
                }
                else if (type == "contract" && Session["Clearance"].ToString() == "1")
                {
                    /* Add contract elapsed option to reason for leaving box, change labels */
                    reasonForLeaving.Items.Add("Contract Elapsed");
                    employeeType.InnerText = "Contract Employee";
                    lblMoney.InnerText = "Contract Amount";
                    lblSinBn.InnerText = "Business Number";
                    employeeViews.ActiveViewIndex = 3;
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/ems/Errors/404.aspx"));
                }
            }
        }
    }
}