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

public partial class newemployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Administrator")
                Response.Redirect("welcome.aspx");
            if (Page.IsPostBack == false)
            {
                dropLevel.Items.Clear();
                DataTable dt = BusinessAccessLayer.Query("select * from levels order by levelid");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                        dropLevel.Items.Add(Convert.ToString(dt.Rows[i]["levelid"]));
                    dropLevel.SelectedIndex = 0;
                }
                else
                    Response.Redirect("welcome.aspx", false);
                int newemployeeid = BusinessAccessLayer.GetNextID("employees", "employeeid");
                lblEmployeeID.Text = Convert.ToString(newemployeeid);
                txtName.Focus();
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
            string name, phone, email, designation, level, username, password;
            name = txtName.Text;
            phone = txtPhone.Text;
            email = txtEmail.Text;
            designation = txtDesignation.Text;
            level = dropLevel.SelectedItem.Text;
            username = txtUsername.Text;
            password = txtPassword.Text;
            string sqlstr = "insert into employees values('" + name + "','" + phone + "','" + email + "','" + designation + "','" + level + "','" + username + "','" + password + "')";
            BusinessAccessLayer.NonQuery(sqlstr);
            Response.Redirect("employees.aspx", false);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}