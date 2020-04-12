using System;
using System.Data;
using System.Data.SqlClient;

namespace NCR.EG.Remittance.BulkUploader
{
    public class DataBaseAccess
    {
        public static string NOROWS = "No Rows Found for [{0}]";
        public static string NOUPDATE = "Error Update Row for [{0}]";
        public static string NOINSERT = "Error Insert Row for [{0}]";

        internal static int InsertTransaction(Model.FileTransaction Trx)
        {
            string strLogDescription = string.Empty;
            System.Data.SqlClient.SqlConnection cn = new SqlConnection();
            System.Data.SqlClient.SqlCommand cmd;
            int RowsAffected;
            string Qstr = string.Empty;
            try
            {
                Qstr = "INSERT INTO [dbo].[NewTransactions] ([TransactionCode],[NationalID],[Amount],[RemitterID],[BeneficiaryName],[TransactionDateTime]) VALUES ('{0}','{1}',{2},{3},'{4}',GETDATE())";
                Qstr = string.Format(Qstr, Trx.ReferenceNumber, Trx.NationalID, Trx.Amount,Trx.RemitterId,Trx.Name);
                cn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString);
                cn.Open();
                cmd = new System.Data.SqlClient.SqlCommand(Qstr, cn);
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected < 1)
                {
                    LogClass.CreateLogDescription(ref strLogDescription, "DataBaseAccess.InsertTransaction", "Error", "No Row Affected for Insert [" + Qstr + "]");
                    LogClass.Log(strLogDescription, false, 0);
                    try
                    {
                        cn.Close();
                        cn = null;
                        cmd = null;
                    }
                    catch (Exception ex)
                    {
                        LogClass.LogError(ex, "DataBaseAccess.InsertTransaction", "Exception");
                    }
                    return ConstantsClass.DB_ERROR_INSERT;
                }
                cn.Close();
                cn = null;
                cmd = null;
                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "DataBaseAccess.InsertTransaction", "Exception for Qstr[" + Qstr + "]");
                return ConstantsClass.DB_ERROR_INSERT;
            }
        }

        internal static void BulkInsertTransactions(DataTable tbl)
        {
            string strLogDescription = string.Empty;
            System.Data.SqlClient.SqlConnection cn = new SqlConnection();
            System.Data.SqlClient.SqlCommand cmd;
            int RowsAffected;
            string Qstr = string.Empty;
            try
            {
                //Qstr = "INSERT INTO [NewTransactions]([TransactionCode],[NationalID],[Amount]) VALUES ('{0}','{1}',{2})";
                //Qstr = string.Format(Qstr, Trx.ReferenceNumber, Trx.NationalID, Trx.Amount);
                cn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString);
                SqlBulkCopy objbulk = new SqlBulkCopy(cn);
                objbulk.DestinationTableName = "NewTransactions";
                objbulk.ColumnMappings.Add("TransactionCode", "TransactionCode");
                objbulk.ColumnMappings.Add("Name", "Name");
                objbulk.ColumnMappings.Add("NationalID", "NationalID");
                objbulk.ColumnMappings.Add("Amount", "Amount");


                cn.Open();
                objbulk.WriteToServer(tbl);

                //RowsAffected = cmd.ExecuteNonQuery();
                //if (RowsAffected < 1)
                //{
                //    LogClass.CreateLogDescription(ref strLogDescription, "DataBaseAccess.InsertTransaction", "Error", "No Row Affected for Insert [" + Qstr + "]");
                //    LogClass.Log(strLogDescription, false, 0);
                //    try
                //    {
                //        cn.Close();
                //        cn = null;
                //        cmd = null;
                //    }
                //    catch (Exception ex)
                //    {
                //        LogClass.LogError(ex, "DataBaseAccess.InsertTransaction", "Exception");
                //    }
                //    return ConstantsClass.DB_ERROR_INSERT;
                //}
                cn.Close();
                cn = null;
                cmd = null;
                //return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "DataBaseAccess.InsertTransaction", "Exception for Qstr[" + Qstr + "]");
                //return ConstantsClass.DB_ERROR_INSERT;
            }
        }
    }
}