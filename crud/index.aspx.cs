using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace crud
{

    public partial class index : System.Web.UI.Page
    {
        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\Projects\asp .net\crud\crud\crud\App_Data\Student.mdf"";Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        SqlCommand cmd = null;
        string img = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            fill();
        }
        void getcon()
        {
            conn = new SqlConnection(s);
            conn.Open();
        }
        void Image()
        {
            if (FileUpload1.HasFile)
            {
                img = "Images/" + FileUpload1.FileName;
                FileUpload1.SaveAs(MapPath(img));
            }
        }
        void fill()
        {
            getcon();
            da = new SqlDataAdapter("select * from students", conn);
            ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        void filltext()
        {
            da = new SqlDataAdapter("select * from students where id='" + ViewState["id"].ToString() + "'", conn);
            ds = new DataSet();
            da.Fill(ds);
            TextBox1.Text = ds.Tables[0].Rows[0][1].ToString();
            RadioButtonList1.Text = ds.Tables[0].Rows[0][2].ToString();
            TextBox2.Text = ds.Tables[0].Rows[0][3].ToString();
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            getcon();
            if (btnsubmit.Text == "Submit")
            {
                Image();
                cmd = new SqlCommand("insert into students(Name,Gender,Email,Image)values('" + TextBox1.Text + "','" + RadioButtonList1.SelectedItem.ToString() + "','" + TextBox2.Text + "','" + img + "')", conn);
                cmd.ExecuteNonQuery();
            }
            if (btnsubmit.Text == "Update")
            {
                Image();
                cmd = new SqlCommand("update students set Name='" + TextBox1.Text + "',Gender='" + RadioButtonList1.SelectedItem.ToString() + "',Email='" + TextBox2.Text + "',Image='" + img + "' where id='" + ViewState["id"] + "'", conn);
                cmd.ExecuteNonQuery();
                fill();
                TextBox1.Text = "";
                TextBox2.Text = "";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                btnsubmit.Text = "Update";
                getcon();
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["id"] = id;
                filltext();
            }
            if (e.CommandName == "delete")
            {
                getcon();
                int id = Convert.ToInt32(e.CommandArgument);
                cmd = new SqlCommand("delete from students where id='" + id + "'", conn);
                cmd.ExecuteNonQuery();
                fill();
            }
        }
        protected void edit_Click(object sender, EventArgs e)
        {

        }

        protected void delete_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}