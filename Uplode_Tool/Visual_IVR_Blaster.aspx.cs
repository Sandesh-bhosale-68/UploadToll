using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Configuration;

public partial class Visual_IVR_Blaster : System.Web.UI.Page
{
   
    protected String ExcelFilePath;
    public int count = 0;
    DataTable tabs = new DataTable();
    protected System.Data.OleDb.OleDbCommand cmd1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Con2"].ConnectionString);
 
 
    public string batchname = string.Empty;
    StringBuilder rahul = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserName"] == null)
        //{
        //    Response.Redirect("Login.aspx");
        //}
        if (!IsPostBack)
        {
            //loadbatchid();
            //Campaign_Name();
        }
       
    }

    public void FileProcessor()
    {
        string replacenull = File.ReadAllText("EMIR_VU_E_");
        replacenull = replacenull.Replace("null", "");
        File.WriteAllText("EMIR_VU_E_", replacenull);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd12 = new SqlCommand("SP_Trunckate_InputMater_temp", con);
            cmd12.CommandType = CommandType.StoredProcedure;
            cmd12.CommandTimeout = 0;
            con.Open();
            cmd12.ExecuteNonQuery();
            con.Close();

            if (FileUpload1.PostedFile.FileName.Equals("") == true)
            {
                lblErrMsg.Text = " Select The File to Upload ";
                lblErrMsg.Visible = true;
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
                goto End1;
            }
            
            // ExcelFilePath = @"D:\Mithun\Projects\New_Uploading_Tool\New_Uploading_Tool\UploadData\";
            //ExcelFilePath = @"D:\Websites\OPO\Visual_IVR_Bluster_Uplode_Tool\UploadData\";

            ExcelFilePath = @"D:\IVR_Bluster_Health";


            string abc = DateTime.Now.ToString("yyyymmddhhmm");
            string hhh =  abc + "_" + FileUpload1.FileName.ToString();

            string excelConecctionString;
            int iReturn1 = 0;

            // iReturn1 = objDataAccess.truncatetable();

            // fud1.PostedFile.SaveAs(ExcelFilePath + fud1.FileName.ToString());
            FileUpload1.PostedFile.SaveAs(ExcelFilePath + hhh);

            excelConecctionString = "Provider = Microsoft.Jet.OLEDB.4.0;" +
                                    "Data Source = " + ExcelFilePath + hhh + " ; " +
                                    "Extended Properties = Excel 8.0;";
            System.Data.OleDb.OleDbConnection Connection = new System.Data.OleDb.OleDbConnection(excelConecctionString);

            Connection.Open();

            DataTable dtSchema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string ExcelSheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
            //------------ Copy Customer Data---------------
            cmd1 = new System.Data.OleDb.OleDbCommand("Select  * From [" + ExcelSheetName + "]", Connection);//right([Restaurant_Opening_Time],11) as [Restaurant_Opening_Time],
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            oleda = new OleDbDataAdapter(cmd1);
            oleda.Fill(ds);
            tabs = ds.Tables[0];
            Connection.Close();
            

            count = tabs.Rows.Count;
            tabs.Columns.Add("BatchID", typeof(System.String));

            var dateAndTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            batchname = "IVR_Blaster" + dateAndTime;

            for (int i = 0; i < tabs.Rows.Count; i++)
            {
                tabs.Rows[i]["BatchID"] = batchname;
            }
         
            using (con)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.ColumnMappings.Add("Policy_Number", "Policy_Number");
                    sqlBulkCopy.ColumnMappings.Add("Policy_Certificate_No", "Policy_Certificate_No");
                    sqlBulkCopy.ColumnMappings.Add("Customer_Name", "Customer_Name");
                    sqlBulkCopy.ColumnMappings.Add("Mobile_Number", "Mobile_Number");                 
                    sqlBulkCopy.ColumnMappings.Add("BatchID","BatchID");

                    sqlBulkCopy.DestinationTableName = "dbo.Input_Master_IVR_Temp";
                    con.Open();
                    sqlBulkCopy.WriteToServer(tabs);
                    con.Close();

                    SqlCommand cmdDataTr = new SqlCommand("InsertDataInputMaterIVR", con);
                    cmdDataTr.CommandType = CommandType.StoredProcedure;
                    cmdDataTr.CommandTimeout = 100;
                    con.Open();
                    cmdDataTr.ExecuteNonQuery();
                    con.Close();

                     
                    lblErrMsg.Text = "Data Uploaded SuccessFully!! with Data Count= " + count + " and Batch Name = " + batchname; ;
                    lblErrMsg.Visible = true;
                    lblErrMsg.ForeColor = System.Drawing.Color.Green;

                    try
                    {
                        SqlCommand cmdTrToCalling = new SqlCommand("transfer_to_Chola_IVR_Blater_CALLING_TABLE", con2);
                        cmdTrToCalling.CommandType = CommandType.StoredProcedure;
                        cmdTrToCalling.Parameters.AddWithValue("@BatchID", batchname);
                        cmdTrToCalling.CommandTimeout = 100;
                        con2.Open();
                        cmdTrToCalling.ExecuteNonQuery();
                        con2.Close();

                    }
                    catch (Exception ex)
                    {

                    }
                    lbltransfer.Text = " Data has been Transfer To calling SuccessFully!! with Data Count= " + count + " and Batch Name = " + batchname; ;
                    lbltransfer.Visible = true;
                    lbltransfer.ForeColor = System.Drawing.Color.Green;
                    //loadbatchid();
                }
            }


        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message.ToString();
            lblErrMsg.Visible = true;
            lblErrMsg.ForeColor = System.Drawing.Color.Red;

        }
    End1: ;

    }

     

    //protected void loadbatchid()
    //{
    //    SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["Con57"].ConnectionString);
        
    //    try
    //    {
    //        string strquery = "Select Distinct  BatchID from INPUT_MASTER where (isTransfer='0' or isTransfer is null) and Batchid is not null";
    //        SqlCommand cmd = new SqlCommand(strquery, con5);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        for (int i = 0; i < dt.Rows.Count;i++ )
    //        {
    //            rahul.Append("'");
    //            rahul.Append(dt.Rows[i]["BatchID"].ToString());
    //            rahul.Append("'");
    //            rahul.Append(",");
    //        }
    //        rahul.Length--;

          
    //            if (dt != null)
    //            {
    //                if (dt.Rows.Count > 0)
    //                {
    //                    //ddlbatchname.DataSource = dt;
    //                    //ddlbatchname.DataTextField = "BatchID";
    //                    //ddlbatchname.DataBind();
    //                    //ddlbatchname.Items.Insert(0, new ListItem("--Select--", ""));
    //                    //Label2.Text = "Check BatchId";
    //                    //lbltransfer.Visible = true;
    //                    //lblbatchid.Visible = true;
    //                    //ddlbatchname.Visible = true;
    //                    //btntransfer.Visible = true;
    //                    //Label2.Visible = true;


    //                }
    //                else
    //                {
    //                    Label2.Visible = false;
    //                    lblErrMsg.Text = "No BatchID Found!!";
    //                    lblErrMsg.ForeColor = System.Drawing.Color.Red;
    //                    //ddlbatchname.ClearSelection();
    //                }
    //            }


    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblErrMsg.Text = "Unable to Open Sql Connection for Selected Process!!";
    //    }
    //}


    //protected void Campaign_Name()
    //{
    //    SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["Con57"].ConnectionString);

    //    try
    //    {
    //        string strquery = "Select Distinct  Campaign_Name from INPUT_MASTER where (isTransfer='0' or isTransfer is null) and BatchID in (" + rahul + "); ";
    //        SqlCommand cmd = new SqlCommand(strquery, con5);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        if (dt != null)
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                //ddl_Campaign_Name.DataSource = dt;
    //                //ddl_Campaign_Name.DataTextField = "Campaign_Name";
    //                //ddl_Campaign_Name.DataBind();
    //                //ddl_Campaign_Name.Items.Insert(0, new ListItem("--Select--", ""));
    //                ////Label2.Text = "Check Campaign_Name";
    //                //lbltransfer.Visible = true;
    //                //ddl_Campaign_Name.Visible = true;
    //                //ddl_Campaign_Name.Visible = true;
    //                //btntransfer.Visible = true;
    //                //Label2.Visible = true;


    //            }
    //            else
    //            {
    //                Label2.Visible = false;
    //                lblErrMsg.Text = "No BatchID Found!!";
    //                lblErrMsg.ForeColor = System.Drawing.Color.Red;
    //               // ddlbatchname.ClearSelection();
    //            }
    //        }


    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblErrMsg.Text = "Unable to Open Sql Connection for Selected Process!!";
    //    }
    //}

   


    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    lblErrMsg.Text = "";
    //   // lbltrans.Text = "";
    //    loadbatchid();
    //}





    //protected void btntransfer_Click(object sender, EventArgs e)
    //{
    //    if (ddlbatchname.SelectedValue != "")
    //    {
    //        int iReturn = 0;
    //        iReturn = Insertintocallingtable(ddlbatchname.SelectedValue.ToString(), ddl_Campaign_Name.SelectedItem.Text.ToString());
    //        if (iReturn == -1)
    //        {
    //            lblErrMsg.Text = "";
    //            lbltrans.Text = "Data Transfer Successfully!!";
    //            lbltrans.Visible = true;
    //            lbltrans.ForeColor = System.Drawing.Color.Green;
    //            loadbatchid();
    //        }
    //        else
    //        {
    //            lblErrMsg.Text = "";
    //            lbltrans.Text = "Data Transfer Failed !!";
    //            lbltrans.Visible = true;
    //            lbltrans.ForeColor = System.Drawing.Color.Red;
    //            loadbatchid();

    //        }
    //    }
    //    else
    //    {
    //        lbltrans.Text = "Please select BatchId";
    //        lbltrans.Visible = true;
    //        lbltrans.ForeColor = System.Drawing.Color.Red;
    //    }
    //}

    //protected int Insertintocallingtable(string batchid, string Operation)
    //{
    //    int iReturn = 0;
    //    try
    //    {
    //        con5.Close();
    //        SqlCommand cmd = new SqlCommand("Transfer_TO_TMFL_CALLING", con5);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@batchid", batchid);
    //        cmd.Parameters.AddWithValue("@Operation", Operation);
    //        con5.Open();
    //        iReturn = cmd.ExecuteNonQuery();
    //        con.Close();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    return iReturn;
    //}

}

