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

public partial class generatepayslip : System.Web.UI.Page
{
    private void GenerateData()
    {
        try
        {
            int employeeid = Convert.ToInt32(Session["currentpayslipemployeeid"]);
            lblDeductionForLeaves.Text = "";
            lblNetSalary.Text = "";
            int month = dropMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(dropYear.SelectedItem.Text);
            DataTable dt = BusinessAccessLayer.Query("select * from payslips where employeeid=" + employeeid + " and month=" + month + " and year=" + year);
            if (dt.Rows.Count == 0)
            {
                if (txtNoOfLeaves.Text.Length <= 0)
                    txtNoOfLeaves.Text = "0";
                int noofleaves = Convert.ToInt32(txtNoOfLeaves.Text);
                if (noofleaves >= 0 && noofleaves <= 31)
                {
                    DataTable dt1 = BusinessAccessLayer.Query("select * from employees e,levels l where e.levelid=l.levelid and employeeid=" + employeeid);
                    decimal basicsalary = Convert.ToDecimal(dt1.Rows[0]["basicsalary"]);
                    int daysinmonth = DateTime.DaysInMonth(Convert.ToInt32(dropYear.SelectedItem.Text), dropMonth.SelectedIndex + 1);
                    decimal salaryperday = basicsalary / daysinmonth;
                    decimal deductionforleaves = noofleaves * salaryperday;
                    decimal netsalary = basicsalary - deductionforleaves;
                    lblSalaryPerDay.Text = Math.Round(salaryperday, 2).ToString();
                    lblDeductionForLeaves.Text = Math.Round(deductionforleaves, 2).ToString();
                    lblNetSalary.Text = Math.Round(netsalary, 2).ToString();
                }
            }
            else
            {
                lblBasicSalary.Text = Convert.ToString(dt.Rows[0]["basicsalary"]);
                txtNoOfLeaves.Text = Convert.ToString(dt.Rows[0]["noofleaves"]);
                lblSalaryPerDay.Text = Convert.ToString(dt.Rows[0]["salaryperday"]);
                lblDeductionForLeaves.Text = Convert.ToString(dt.Rows[0]["deductionforleaves"]);
                lblNetSalary.Text = Convert.ToString(dt.Rows[0]["netsalary"]);
            }
            txtNoOfLeaves.Focus();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }                
    }
    private void GenerateDataWithOutNoOfLeaves()
    {
        try
        {
            int employeeid = Convert.ToInt32(Session["currentpayslipemployeeid"]);
            lblDeductionForLeaves.Text = "";
            lblNetSalary.Text = "";
            if (txtNoOfLeaves.Text.Length <= 0)
                txtNoOfLeaves.Text = "0";
            int noofleaves = Convert.ToInt32(txtNoOfLeaves.Text);
            if (noofleaves >= 0 && noofleaves <= 31)
            {
                DataTable dt1 = BusinessAccessLayer.Query("select * from employees e,levels l where e.levelid=l.levelid and employeeid=" + employeeid);
                decimal basicsalary = Convert.ToDecimal(dt1.Rows[0]["basicsalary"]);
                int daysinmonth = DateTime.DaysInMonth(Convert.ToInt32(dropYear.SelectedItem.Text), dropMonth.SelectedIndex + 1);
                decimal salaryperday = basicsalary / daysinmonth;
                decimal deductionforleaves = noofleaves * salaryperday;
                decimal netsalary = basicsalary - deductionforleaves;
                lblSalaryPerDay.Text = Math.Round(salaryperday, 2).ToString();
                lblDeductionForLeaves.Text = Math.Round(deductionforleaves, 2).ToString();
                lblNetSalary.Text = Math.Round(netsalary, 2).ToString();
            }
            txtNoOfLeaves.Focus();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                for (int i = DateTime.Now.Year; i >= 1950; i--)
                    dropYear.Items.Add(i.ToString());
                int employeeid = Convert.ToInt32(Session["currentpayslipemployeeid"]);
                DataTable dt = BusinessAccessLayer.Query("select * from employees e,levels l where e.levelid=l.levelid and employeeid=" + employeeid);
                if (dt.Rows.Count > 0)
                {
                    lblEmployee.Text = Convert.ToString(dt.Rows[0]["name"]);
                    lblBasicSalary.Text = Convert.ToString(dt.Rows[0]["basicsalary"]);
                    DateTime date = DateTime.Now.AddMonths(-1);
                    dropMonth.SelectedIndex = date.Month - 1;
                    dropYear.SelectedIndex = dropYear.Items.IndexOf(new ListItem(date.Year.ToString()));
                    GenerateData();
                    txtNoOfLeaves.Focus();
                }
                else
                    Response.Redirect("payslips.aspx", false);
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
            if (Page.IsValid)
            {
                int employeeid, month, year, noofleaves;
                string monthname;
                DateTime generatedon;
                decimal basicsalary, salaryperday, deductionperleaves, netsalary;
                employeeid = Convert.ToInt32(Session["currentpayslipemployeeid"]);
                month = dropMonth.SelectedIndex + 1;
                monthname = dropMonth.SelectedItem.Text;
                year = Convert.ToInt32(dropYear.SelectedItem.Text);
                generatedon = DateTime.Now;
                basicsalary = Convert.ToDecimal(lblBasicSalary.Text);
                noofleaves = Convert.ToInt32(txtNoOfLeaves.Text);
                salaryperday = Convert.ToDecimal(lblSalaryPerDay.Text);
                deductionperleaves = Convert.ToDecimal(lblDeductionForLeaves.Text);
                netsalary = Convert.ToDecimal(lblNetSalary.Text);
                DataTable dt = BusinessAccessLayer.Query("select * from payslips where employeeid=" + employeeid + " and month=" + month + " and year=" + year);
                string sqlstr;
                if (dt.Rows.Count == 0)
                    sqlstr = "insert into payslips values(" + employeeid + "," + month + ",'" + monthname + "'," + year + ",'" + generatedon.ToString() + "'," + basicsalary + "," + noofleaves + "," + salaryperday + "," + deductionperleaves + "," + netsalary + ")";
                else
                    sqlstr = "update payslips set generatedon='" + generatedon.ToString() + "',basicsalary=" + basicsalary + ",noofleaves=" + noofleaves + ",salaryperday=" + salaryperday + ",deductionforleaves=" + deductionperleaves + ",netsalary=" + netsalary + " where employeeid=" + employeeid + " and month=" + month + " and year=" + year;
                BusinessAccessLayer.NonQuery(sqlstr);
                Response.Redirect("payslips.aspx");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error: " + ex.Message;
        }
    }
    protected void txtNoOfLeaves_TextChanged(object sender, EventArgs e)
    {
        GenerateDataWithOutNoOfLeaves();
    }
    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNoOfLeaves.Text = "0";
        GenerateData();
    }
    protected void dropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNoOfLeaves.Text = "0";
        GenerateData();
    }
}