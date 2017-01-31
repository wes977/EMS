/*
    * Filename: Companies.aspx.cs
    *
    * Description:
    * The code behind for the Companies.
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
using System.Data;
using System.Data.Sql;

using EMS.DataAccess;
using System.Web.UI.HtmlControls;

namespace EMS.Web.ems.Manage
{

    /// <summary>
    /// Holds the definition of the Manage/Companies class.
    /// </summary>
    public partial class Companies : System.Web.UI.Page
    {
        /// <summary>
        /// On page load we want to create the companies table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateCompaniesTbl();
        }

        /// <summary>
        /// Brings up the create company page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void createNewCompany_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/ems/Create/Company.aspx"));

        }


        /// <summary>
        /// DeleteCompany class to be called from ajax on the client side.
        /// </summary>
        /// <param name="companyString"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string DeleteCompany(string companyString)
        {
            Connection conn = Connection.Create("conn");
            string result = "";
            bool deleteCompanyResult = false;
             
            deleteCompanyResult = conn.DeleteCompany(companyString);
        
            if (deleteCompanyResult == false)
            {
                result = string.Format("{0}: {1}", "Could not delete company", companyString);
            }
            else
            {
                result = string.Format("{0} successfully deleted.", companyString);
            }

            return result;
        }


        /// <summary>
        /// Creates the companies table.
        /// </summary>
        private void CreateCompaniesTbl()
        {
            Connection conn = Connection.Create("conn");
            DataTable companies = conn.GetCompanies();


            if (companies.Rows.Count > 0)
            {
                TableRow row = new TableRow();

                // Add a header cell. 
                TableHeaderCell headerCell = new TableHeaderCell();

                for (int j = 0; j < companies.Columns.Count; j++)
                {
                    headerCell = new TableHeaderCell();
                    headerCell.Text = companies.Columns[j].ColumnName;
                    row.Cells.Add(headerCell);
                }

                companiesTable.Rows.Add(row);
                
                headerCell = new TableHeaderCell();
                headerCell.Text = "Options";
                headerCell.ID = "0";
                row.Cells.Add(headerCell);

                for (int i = 0; i < companies.Rows.Count; i++)
                {
                   
                    row = new TableRow();
                    TableCell cell = null;
                    

                    // add each cell based on data provided
                    for (int j = 0; j < companies.Columns.Count; j++)
                    {
                        cell = new TableCell();
                        cell.Text = companies.Rows[i][j].ToString();
                        row.Cells.Add(cell);
                    }

                    cell = new TableCell();

                    // create the delete button
                    HtmlGenericControl button = new HtmlGenericControl("button");
                    button.Attributes["class"] = "fa fa-align-right fa-lg fa-trash button-hidden text-danger";

                    // add the button to the cell
                    cell.Controls.Add(button);

                    // add the cell to the row
                    row.Cells.Add(cell);
                    row.ID = (i + 1).ToString();
 
                    companiesTable.Rows.Add(row);
                }

            }

        }


    }
}