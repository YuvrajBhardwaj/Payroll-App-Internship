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

public partial class employeemessageoutbox : System.Web.UI.Page
{
    private void Show()
    {
        gridMessages.DataSource = null;
        gridMessages.DataBind();
        lblMessage.Text = "";
        int from = Convert.ToInt32(Session["currentemployeeid"]);
        DataTable dt = BusinessAccessLayer.Query("select * from messages where messageto='Manager' and employeeid=" + from + " order by date desc");
        if (dt.Rows.Count > 0)
        {
            gridMessages.DataSource = dt;
            gridMessages.DataBind();
        }
        else
            lblMessage.Text = "No Messages.";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Employee")
                Response.Redirect("welcome.aspx");
            if (Page.IsPostBack == false)
            {
                Show();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void gridMessages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int messageid = Convert.ToInt32(gridMessages.Rows[e.RowIndex].Cells[0].Text);
            string sqlstr = "delete from messages where messageid=" + messageid;
            BusinessAccessLayer.NonQuery(sqlstr);
            Show();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}