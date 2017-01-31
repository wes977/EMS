using System;
using EMS.DataAccess;

namespace EMS.Web
{
    public partial class EMS_PSS : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.RedirectPermanent("/", true);
                }
            }

        }

        /// <summary>
        /// Numbers the invalid.
        /// </summary>
        /// <returns></returns>
        protected int NumberInvalid()
        {
            Connection conn = Connection.Create("conn");
            return conn.InvalidEmployeeCount();
        }

        

    }
}