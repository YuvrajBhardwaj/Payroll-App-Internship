using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string currentusertype = Convert.ToString(Session["currentusertype"]);
            switch (currentusertype)
            {
                case "Administrator": Response.Redirect("adminwelcome.aspx"); break;
                case "Manager": Response.Redirect("managerwelcome.aspx"); break;
                case "Employee": Response.Redirect("employeewelcome.aspx"); break;
                default: Response.Redirect("Default.aspx"); break;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}