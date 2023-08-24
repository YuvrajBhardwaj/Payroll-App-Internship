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

public partial class manageremployees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Manager")
                Response.Redirect("welcome.aspx");
            DataTable dt = BusinessAccessLayer.Query("select * from employees order by employeeid");
            gridEmployees.DataSource = dt;
            gridEmployees.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}