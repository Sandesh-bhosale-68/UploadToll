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

public partial class FLAG : System.Web.UI.Page
{

    DataTable CSVTable = new DataTable();
    protected String ExcelFilePath;
    public int count = 0;
    string EmpId = "";

    DataTable tabs = new DataTable();
    protected System.Data.OleDb.OleDbCommand cmd1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMFL_MUM"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMFL_MUM"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
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
            EmpId = Session["UserName"].ToString();
            if (FileUpload1.PostedFile.FileName.Equals("") == true)
            {
                lblErrMsg.Text = " Select The File to Upload ";
                lblErrMsg.Visible = true;
                lblErrMsg.ForeColor = System.Drawing.Color.Red;
                goto End1;
            }

            // ExcelFilePath = @"D:\Mithun\Projects\New_Uploading_Tool\New_Uploading_Tool\UploadData\";
            ExcelFilePath = @"D:\Websites\OPO\TMFL_DATAUPLOAD\UploadData\";


            string abc = DateTime.Now.ToString("yyyymmddhhmm");
            string hhh = abc + "_" + FileUpload1.FileName.ToString();

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

            DataTable dts = new DataTable();
            dts = ds.Tables[0];
            dts.Columns.Add("EmpId", typeof(System.String));


            for (int i = 0; i < dts.Rows.Count; i++)
            {
                dts.Rows[i]["EmpId"] = EmpId;
            }

            tabs = dts;
            





            Connection.Close();
            //for (int i = 0; i < tabs.Rows.Count; i++ )
            //{
            //    string mm = tabs.Rows[i]["Restaurant_Opening_Time"].ToString();
            //    string aa = tabs.Rows[i]["Restaurant_Opening_Time"].ToString().Substring(10, 15);
            //}

            count = tabs.Rows.Count;
            //tabs.Columns.Add("BatchID", typeof(System.String));

            var dateAndTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            //batchname = "GoAir_" + dateAndTime;

            //for (int i = 0; i < tabs.Rows.Count; i++)
            //{
            //    tabs.Rows[i]["BatchID"] = batchname;
            //}

            ////con1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TMFL_MUM"].ConnectionString;
            ////con1.Open();
            ////SqlCommand cmd = new SqlCommand("Usp_BackupTruncate_FLag_Upload", con1);
           
            ////cmd.CommandType = CommandType.StoredProcedure;

            ////cmd.Parameters.AddWithValue("@EmpId", EmpId);
           
            ////cmd.CommandTimeout = 0;

            ////cmd.ExecuteNonQuery();
            using (con)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {

                    //sqlBulkCopy.ColumnMappings.Add("Company", "Company");

                    sqlBulkCopy.ColumnMappings.Add("Contract_no", "Contract_no");
                    sqlBulkCopy.ColumnMappings.Add("FLAG", "FLAG");
                    sqlBulkCopy.ColumnMappings.Add("EmpId", "EmpId");
                    sqlBulkCopy.DestinationTableName = "dbo.FLag_Upload";

                   con.Open();
                    sqlBulkCopy.WriteToServer(tabs);
                    con.Close();
                   // InsertDominos_transfer_Details(hhh);

                   // con1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Con57"].ConnectionString;
                  // con1.Open();
                    SqlCommand cmd12 = new SqlCommand("Usp_Update_TMFLCEarly_stage", con1);
                    cmd12.CommandType = CommandType.StoredProcedure;
                    cmd12.CommandTimeout = 0;
                    con1.Open();
                    cmd12.ExecuteNonQuery();
                    con1.Close();


                    //con1.Close();
                    lblErrMsg.Text = "Data Uploaded Successfully!!";
                    lblErrMsg.Visible = true;
                    lblErrMsg.ForeColor = System.Drawing.Color.Green;

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



    //public void InsertDominos_transfer_Details(string hhh)
    //{
    //    string excelConecctionString;

    //    excelConecctionString = "Provider = Microsoft.Jet.OLEDB.4.0;" +
    //                                "Data Source = " + ExcelFilePath + hhh + " ; " +
    //                                "Extended Properties = Excel 8.0;";
    //    System.Data.OleDb.OleDbConnection Connection = new System.Data.OleDb.OleDbConnection(excelConecctionString);

    //    Connection.Open();

    //    DataTable dtSchema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //    string ExcelSheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
    //    //------------ Copy Customer Data---------------
    //    cmd1 = new System.Data.OleDb.OleDbCommand("Select  * From [" + ExcelSheetName + "]", Connection);//right([Restaurant_Opening_Time],11) as [Restaurant_Opening_Time],
    //    OleDbDataAdapter oleda = new OleDbDataAdapter();
    //    DataSet ds = new DataSet();
    //    oleda = new OleDbDataAdapter(cmd1);
    //    oleda.Fill(ds);
    //    tabs = ds.Tables[0];
    //    Connection.Close();

    //    count = tabs.Rows.Count;

    //    using (con)
    //    {
    //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //        {
    //            sqlBulkCopy.ColumnMappings.Add("SrNo", "SrNo");
    //            sqlBulkCopy.ColumnMappings.Add("RestaurantID", "RestaurantID");
    //            sqlBulkCopy.ColumnMappings.Add("RestaurantName", "RestaurantName");
    //            sqlBulkCopy.ColumnMappings.Add("City", "City");
    //            sqlBulkCopy.ColumnMappings.Add("State", "State");
    //            sqlBulkCopy.ColumnMappings.Add("pincode", "pincode");
    //            sqlBulkCopy.ColumnMappings.Add("Circle", "Circle");
    //            sqlBulkCopy.ColumnMappings.Add("Region", "Region");
    //            sqlBulkCopy.ColumnMappings.Add("PhoneL1", "PhoneL1");
    //            sqlBulkCopy.ColumnMappings.Add("GDMName", "GDMName");
    //            sqlBulkCopy.ColumnMappings.Add("GDMContact", "GDMContact");

    //            sqlBulkCopy.DestinationTableName = "dbo.Dominos_transfer_Details";
    //            con.Open();
    //            sqlBulkCopy.WriteToServer(tabs);
    //            con.Close();


    //        }
    //    }
    //}


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblTotal1.Text = "";
        lblUpload1.Text = "";
        lblDuplicate1.Text = "";
        lblErrMsg.Text = "";
    }





}

