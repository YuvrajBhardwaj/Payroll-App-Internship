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

public partial class employees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["currentusertype"]) != "Administrator")
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
    protected void gridEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int employeeid = Convert.ToInt32(gridEmployees.Rows[e.RowIndex].Cells[0].Text);
            Session["deletingemployeeid"] = employeeid;
            Response.Redirect("deleteemployee.aspx", false);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
}