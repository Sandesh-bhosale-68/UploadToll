
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{


    string strcon = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UserName"] = null;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string script = "";


        if (txtUser.Value == "" && txtPassword.Value == "")
        {
            script = "alert(\"Please Enter User Details!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        if (txtUser.Value == "" || txtPassword.Value == "")
        {
            script = "alert(\"Please Enter User Details!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }



        SqlConnection conobj = new SqlConnection(strcon);
        SqlCommand cmd10 = new SqlCommand("logInValidation", conobj);
        cmd10.Parameters.AddWithValue("@UserCode", txtUser.Value);
        cmd10.Parameters.AddWithValue("@Password", txtPassword.Value);
        cmd10.CommandType = CommandType.StoredProcedure;
        conobj.Open();
        string abc = "";
        try
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd10);
            da.Fill(ds);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                abc = ds.Tables[0].Rows[0]["User_Name"].ToString();
                Session["UserName"] = abc;
                Response.Redirect("Visual_IVR_Blaster.aspx");
            }
            else
            {
                txtUser.Value = "";
                txtPassword.Value = "";
                script = "alert(\"You don't have permission to access!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
        catch (Exception ex)
        {

           string script2 = "alert(\"Error Occured!\");";
           ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script2, true);
        }
        
        conobj.Close();
    }
}
