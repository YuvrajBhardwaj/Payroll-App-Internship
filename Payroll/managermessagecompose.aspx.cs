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

public partial class adminmessagecompose : System.Web.UI.Page
{
    string from = "Manager";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Manager")
                Response.Redirect("welcome.aspx");
            if (Page.IsPostBack == false)
            {
                dropTo.Focus();
                dropTo.Items.Clear();
                dropTo.Items.Add("---Select---");
                DataTable dt = BusinessAccessLayer.Query("select * from employees order by levelid");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                        dropTo.Items.Add(Convert.ToString(dt.Rows[i]["name"]));
                    dropTo.SelectedIndex = 0;
                    if (Session["replyemployeename"] != null && Convert.ToString(Session["replyemployeename"]) != "")
                    {
                        int n = 0;
                        for (int i = 0; i < dropTo.Items.Count; i++)
                        {
                            if (Convert.ToString(Session["replyemployeename"]) == dropTo.Items[i].Text)
                                n = i;
                        }
                        Session["replyemployeename"] = null;
                        dropTo.SelectedIndex = n;
                        txtMessage.Focus();
                    }
                }
                else
                    Response.Redirect("welcome.aspx", false);
            }
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
            messagefrom = from;
            messageto = dropTo.SelectedItem.Text;
            message = txtMessage.Text;
            DataTable dt = BusinessAccessLayer.Query("select employeeid from employees where name='" + messageto + "'");
            if (dt.Rows.Count > 0)
            {
                employeeid = Convert.ToInt32(dt.Rows[0]["employeeid"]);
                string sqlstr = "insert into messages values('" + date + "','" + messagefrom + "','" + messageto + "','" + message + "'," + employeeid + ",1)";
                BusinessAccessLayer.NonQuery(sqlstr);
            }
            Response.Redirect("managermessageoutbox.aspx", false);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}