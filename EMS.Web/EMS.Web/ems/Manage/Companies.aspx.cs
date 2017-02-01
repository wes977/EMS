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






    }
}