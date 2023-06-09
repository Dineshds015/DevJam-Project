﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

public partial class treasure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["a"] == null)
        {
            Response.Redirect("homepage.aspx");

        }
        if (!this.IsPostBack)
        {
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from tbl_register where stud_id='" + Session["a"] + "'";
                cmd.Connection = con;

                using (MySqlDataReader msr = cmd.ExecuteReader())
                {
                    msr.Read();
                    img_c_image.ImageUrl = msr["s_image"].ToString();
                }
                con.Close();
            }
            commentct();
        }
    }
    string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private void commentct()
    {
        using (MySqlConnection con = new MySqlConnection(constring))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from tbl_treasery", con);
            MySqlCommand cm = new MySqlCommand("select 't_id' from tbl_treasery", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            MySqlDataAdapter da1 = new MySqlDataAdapter(cm);
            DataTable dt1 = new DataTable();
            Label1.Text = "Total Post : " + da1.Fill(dt1).ToString();
            gridview.DataSource = dt;
            gridview.DataBind();
            con.Close();
        }
    }
    protected void btn_home_Click(object sender, EventArgs e)
    {
        if (Session["a"] != null)
        {
            Response.Redirect("dashboard.aspx");
        }
        else
        {
            Response.Redirect("homepage.aspx");
        }
    }
    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Session["a"] = null;
        Response.Redirect("homepage.aspx");
    }
    protected void btn_img_setting_Click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
    protected void btn_fb_onClick(object sender, EventArgs e)
    {
        Response.Redirect("https://www.facebook.com/MNNITALLD");
    }
    protected void btn_tt_onClick(object sender, EventArgs e)
    {
        Response.Redirect("https://twitter.com/MNNITALLD");
    }
    protected void btn_ig_onClick(object sender, EventArgs e)
    {
        Response.Redirect("https://www.instagram.com/mnnitalld/");
    }
    protected void btn_em_onClick(object sender, EventArgs e)
    {
        Response.Redirect("https://nodalc@mnnit.ac.in");
    }
    protected void btn_fd_onClick(object sender, EventArgs e)
    {
        Response.Redirect("https://www.linkedin.com/school/motilal-nehru-national-institute-of-technology/");
    }

    protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr = gridview.SelectedRow;
        string filePath = Server.MapPath("~/Files/DS_END_2014.pdf");
        //string filePath=Server.MapPath("~/" + gr.Cells[3].Text);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
}