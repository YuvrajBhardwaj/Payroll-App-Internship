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

public partial class adminmessages : System.Web.UI.Page
{
    string to = "Manager";
    private void Show()
    {
        gridMessages.DataSource = null;
        gridMessages.DataBind();
        lblMessage.Text = "";
        DataTable dt = BusinessAccessLayer.Query("select * from messages where messageto='" + to + "' and active=1 order by date desc");
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
            if (Convert.ToString(Session["currentusertype"]) != "Manager")
                Response.Redirect("welcome.aspx");
            if (Page.IsPostBack == false)
                Show();            
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void gridMessages_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (gridMessages.SelectedIndex >= 0)
            {
                string employeename = Convert.ToString(gridMessages.Rows[gridMessages.SelectedIndex].Cells[2].Text);
                Session["replyemployeename"] = employeename;
                Response.Redirect("managermessagecompose.aspx", false);
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
            string sqlstr = "update messages set active=0 where messageid=" + messageid;
            BusinessAccessLayer.NonQuery(sqlstr);
            Show();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}