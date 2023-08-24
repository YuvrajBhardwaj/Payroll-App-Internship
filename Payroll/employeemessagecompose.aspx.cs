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

public partial class employeemessagecompose : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Employee")
                Response.Redirect("welcome.aspx");
            if (Page.IsPostBack == false)
                txtMessage.Focus();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string date, messagefrom, messageto, message;
            int employeeid;
            date = DateTime.Now.ToString();
            messagefrom = Convert.ToString(Session["currentemployeename"]);
            messageto = "Manager";
            message = txtMessage.Text;
            employeeid = Convert.ToInt32(Session["currentemployeeid"]);
            string sqlstr = "insert into messages values('" + date + "','" + messagefrom + "','" + messageto + "','" + message + "'," + employeeid + ",1)";
            BusinessAccessLayer.NonQuery(sqlstr);
            Response.Redirect("employeemessageoutbox.aspx", false);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}