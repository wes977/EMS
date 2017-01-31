
/*
    * Filename: Users.aspx.cs
    *
    * Description:
    * Holds the code behind for the manage/user page/
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
using System.Data;
using EMS.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EMS.Web.ems.Manage
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateUserTable();
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/ems/Create/User.aspx"), true);
        }


        [System.Web.Services.WebMethod]
        public static string DeleteUser(string username)
        {
            Connection conn = Connection.Create("conn");
            string result = "";
            bool deleteUserResult = false;


            deleteUserResult = conn.DeleteUser(username);

            if (deleteUserResult == false)
            {
                result = string.Format("{0}:{1}", "Could not delete user", username);
            }
            else
            {
                result = string.Format("{0} successfully deleted.", username);
            }

            return result;
        }



        /// <summary>
        /// Creates the user table.
        /// </summary>
        private void CreateUserTable()
        {
            bool matchedUser = false;
            Connection conn = Connection.Create("conn");
            DataTable users = conn.GetAllUsers();

            if (users.Rows.Count > 0)
            {
                TableRow row = new TableRow();

                // create the header row
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "#";
                row.Cells.Add(headerCell);

                for (int j = 0; j < users.Columns.Count; j++)
                {
                    headerCell = new TableHeaderCell();
                    headerCell.Text = users.Columns[j].ColumnName;
                    row.Cells.Add(headerCell);
                }

                headerCell = new TableHeaderCell();
                headerCell.Text = "Options";
                headerCell.ID = "0";
                row.Cells.Add(headerCell);

                // add the header row
                usersTable.Rows.Add(row);

                //Add the Column values
                for (int i = 0; i < users.Rows.Count; i++)
                {
                    row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Text = (i + 1).ToString();
                    row.Cells.Add(cell);

                    // add each cell based on data provided
                    for (int j = 0; j < users.Columns.Count; j++)
                    {
                        cell = new TableCell();
                        cell.Text = users.Rows[i][j].ToString();
                        // check if this row contains the current users username
                        if (cell.Text == Session["Username"].ToString())
                        {
                            matchedUser = true;
                        }
                        row.Cells.Add(cell);
                    }

                    cell = new TableCell();

                    // create the delete button
                    HtmlGenericControl button = new HtmlGenericControl("button");
                    button.Attributes["class"] = "fa fa-align-right fa-lg fa-trash button-hidden ";

                    // if this row is a match for the current user,
                    // dont allow deletion of himself
                    if (matchedUser)
                    {
                        button.Attributes["class"] += "text-muted";
                        button.Disabled = true;
                        matchedUser = false;
                    }
                    else
                    {
                        button.Attributes["class"] += "text-danger";
                        button.Disabled = false;
                    }

                    // add the button to the cell
                    cell.Controls.Add(button);

                    // add the cell to the row
                    row.Cells.Add(cell);
                    row.ID = (i + 1).ToString();


                    // Add the TableRow to the Table
                    usersTable.Rows.Add(row);

                }
            }
        }
    }
}