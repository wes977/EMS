
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

using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EMS.Web.ems.Manage
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
    
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/ems/Create/User.aspx"), true);
        }







    }
}