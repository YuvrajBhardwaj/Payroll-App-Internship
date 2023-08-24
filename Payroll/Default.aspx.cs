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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currentusertype"] != null || Convert.ToString(Session["currentusertype"]) != "")
            Response.Redirect("welcome.aspx");
        txtUserName.Focus();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            if (username == "admin" && password == "payroll123")
            {
                Session["currentusertype"] = "Administrator";
                Response.Redirect("welcome.aspx", false);
            }
            else if (username == "manager" && password == "payroll123")
            {
                Session["currentusertype"] = "Manager";
                Response.Redirect("welcome.aspx", false);
            }
            else
            {
                DataTable dt = BusinessAccessLayer.Query("select * from employees where username='" + username + "' and password='" + password + "'");
                if (dt.Rows.Count >= 1)
                {
                    Session["currentusertype"] = "Employee";
                    Session["currentusername"] = username;
                    Session["currentuserpassword"] = password;
                    Session["currentemployeeid"] = Convert.ToInt32(dt.Rows[0]["employeeid"]);
                    Session["currentemployeename"] = Convert.ToString(dt.Rows[0]["name"]);
                    Response.Redirect("welcome.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}