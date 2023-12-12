using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
//using System.Reflection.Emit;
//using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace GridView01
{
    public partial class Page1 : System.Web.UI.Page
    {
        private void Fill()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            SqlDataAdapter da = new SqlDataAdapter("select * from std", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "std");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)                              
        {
            if (IsPostBack == false)
            {
                Fill();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)       //for row deleting
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            Label label = (Label)row.FindControl("Label1");
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            conn1.Open();
            string s = "delete from std where id='" + label.Text + "'";
            SqlCommand cmd = new SqlCommand(s, conn1);
            cmd.ExecuteNonQuery();
            conn1.Close();
            Fill();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)      //for row editing
        {
            GridView1.EditIndex = e.NewEditIndex;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)        //after editing we need to update the record
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            TextBox t1 = (TextBox)row.FindControl("TextBox1");
            TextBox t2 = (TextBox)row.FindControl("TextBox2");
            TextBox t3 = (TextBox)row.FindControl("TextBox3");
            TextBox t4 = (TextBox)row.FindControl("TextBox4");
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            conn1.Open();
            string s = "update std set name='" + t2.Text + "',mobile='" + t3.Text + "',email='" + t4.Text + "' where id='" + t1.Text + "'";
            SqlCommand cmd = new SqlCommand(s, conn1);
            cmd.ExecuteNonQuery();
            conn1.Close();
            GridView1.EditIndex = -1;

            Fill();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)      //for cancelling the rows
        {
            GridView1.EditIndex = -1;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)    //for page changing
        {
            GridView1.PageIndex = e.NewPageIndex;
            Fill();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)        //for select click
        {
            GridViewRow row = GridView1.SelectedRow;
            Label label1 = (Label)row.FindControl("Label1");
            Label label2 = (Label)row.FindControl("Label2");
            Label label3 = (Label)row.FindControl("Label3");
            Label label4 = (Label)row.FindControl("Label4");
            TextBox5.Text = label1.Text;
            TextBox6.Text = label2.Text;
            TextBox7.Text = label3.Text;
            TextBox8.Text = label4.Text;
        }

        protected void Button1_Click(object sender, EventArgs e) //for updating the record
        {
            SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            conn1.Open();
            string s = "update std set name='" + TextBox6.Text + "',mobile='" + TextBox7.Text + "',email='" + TextBox8.Text + "' where id='" + TextBox5.Text + "'";
            SqlCommand cmd = new SqlCommand(s, conn1);
            cmd.ExecuteNonQuery();
            conn1.Close();
            Fill();
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";

        }

        protected void Button2_Click(object sender, EventArgs e)  //for inserting the rows in table
        {
            GridViewRow row = GridView1.FooterRow;
            TextBox t1 = (TextBox)row.FindControl("txtid");
            TextBox t2 = (TextBox)row.FindControl("txtname");
            TextBox t3 = (TextBox)row.FindControl("txtmobile");
            TextBox t4 = (TextBox)row.FindControl("txtemail");
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["hi"].ToString());
            conn.Open();
            string query = "insert into std values('" + t1.Text + "','" + t2.Text + "','" + t3.Text + "','" + t4.Text + "')";
            SqlCommand sc = new SqlCommand(query, conn);
            sc.ExecuteNonQuery();
            conn.Close();
            Fill();
        }
    }
}