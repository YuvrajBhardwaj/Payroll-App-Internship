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

public partial class print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            string htmlcode = Convert.ToString(Session["htmlcodetoprint"]);
            Session["htmlcodetoprint"] = null;
            Response.Write(htmlcode);
            Response.Write("<script language='javascript'>window.print();</script>");
        }
    }
}