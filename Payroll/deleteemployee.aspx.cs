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

public partial class deleteemployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Administrator")
                Response.Redirect("welcome.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void lnkYes_Click(object sender, EventArgs e)
    {
        try
        {
            int employeeid = Convert.ToInt32(Session["deletingemployeeid"]);
            string sqlstr = "delete from employees where employeeid=" + employeeid;
            BusinessAccessLayer.NonQuery(sqlstr);
            Session["deletingemployeeid"] = null;
            Response.Redirect("employees.aspx", false);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void lnkNo_Click(object sender, EventArgs e)
    {
        try
        {
            Session["deletingemployeeid"] = null;
            Response.Redirect("employees.aspx", false);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}