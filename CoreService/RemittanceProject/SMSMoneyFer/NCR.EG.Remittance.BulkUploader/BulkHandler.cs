using NCR.EG.Remittance.BulkUploader.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace NCR.EG.Remittance.BulkUploader
{
    public class BulkHandler
    {
        List<Model.FileTransaction> FileTrxList;
        List<Model.FileTransaction> ConfirmedFileTrxList;
        List<string> ErrorParsedRecords;
        //List<Model.RemittanceTrasnaction> RemTrxList;
        string strLogDesc = "";
        public BulkHandler()
        {
            FileTrxList = new List<Model.FileTransaction>();
            ErrorParsedRecords = new List<string>();
            ConfirmedFileTrxList = new List<Model.FileTransaction>();
        }

        internal int ProcessFile(string bulkFilePath)
        {
            try
            {
                int retVal;
                string errorStr = "[{0}|{1}|BulkHandler.ProcessFile] {2}";
                LogClass.Log(Environment.NewLine, false, 0);
                if (!File.Exists(bulkFilePath))
                {
                    LogClass.CreateLogDescription(ref strLogDesc, "BulkHandler.ProcessFile", "Infromation", bulkFilePath + " Not Found");
                    LogClass.Log(strLogDesc, false, 0);
                    return ConstantsClass.ERROR_FILE_NOT_FOUND;
                }
                List<string> allLinesText = File.ReadAllLines(bulkFilePath).ToList();
                LogClass.CreateLogDescription(ref strLogDesc, "BulkHandler.ProcessFile", "Information", "Number of records before parsing [" + allLinesText.Count() + "]");
                LogClass.Log(strLogDesc, false, 0);

                foreach (string tmpStr in allLinesText)
                {
                    //Mobile Number|National ID|Amount|Remitter name
                    if (!string.IsNullOrEmpty(tmpStr))
                    {
                        string[] data = tmpStr.Split(ConfigClass.DataFileSeparator);
                        if (data.Length >= ConfigClass.MinimumDataNumber)
                        {
                            if (!string.IsNullOrEmpty(data[0].Trim()) && !string.IsNullOrEmpty(data[1].Trim()) && !string.IsNullOrEmpty(data[3].Trim()))
                            {
                                int tmpInt = 0;
                                int amt = 0;
                                bool ok = int.TryParse(data[2].Trim(), out tmpInt);
                                if (!ok)
                                {
                                    ErrorParsedRecords.Add(tmpStr);
                                    //LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "Counldn't convert to number " + data[2].Trim()), false, 0);
                                }
                                else
                                {
                                    amt = tmpInt;
                                    Model.FileTransaction FT = new Model.FileTransaction(data[0].Trim(), data[1].Trim(), amt, data[3].Trim());
                                    retVal = FT.GenerateReference2();
                                    if (retVal == ConstantsClass.OK)
                                        FileTrxList.Add(FT);
                                    else
                                    {
                                        ErrorParsedRecords.Add(tmpStr);
                                        //LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "Model.FileTransaction.GenerateReference return [" + retVal.ToString() + "]"), false, 0);
                                    }
                                }
                            }
                            else
                            {
                                ErrorParsedRecords.Add(tmpStr);
                                //LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "empty fields detected"), false, 0);
                            }
                        }
                        else
                        {
                            ErrorParsedRecords.Add(tmpStr);
                            //LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "Couldn't process the data in [" + tmpStr + "] record, data values less than " + ConfigClass.MinimumDataNumber), false, 0);
                        }
                    }
                    else
                    {
                        //LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Infromation" + new string(' ', 11)).Substring(0, 11), "Empty line found in file"), false, 0);
                    }
                }

                LogClass.CreateLogDescription(ref strLogDesc, "BulkHandler.ProcessFile", "Information", "Number of records after parsing [" + FileTrxList.Count() + "]");
                LogClass.Log(strLogDesc, false, 0);
                retVal = BulkInsertIntoDB(FileTrxList);
                GenreateOutputPackage();

                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {

                LogClass.LogError(ex, "BulkHandler.ProcessFile", "Exception");
                return ConstantsClass.PROCESS_FILE_EXCEPTION;
            }

        }

        private void GenreateOutputPackage()
        {
            string folderName = DateTime.Now.ToString("yyyyMMdd") + @"\";
            string outputDirectory = ConfigClass.OutputFilePath + folderName;
            string processedFile = outputDirectory + ConfigClass.OutputFileName;
            //string originalFile;
            string errorFile = outputDirectory + "ERROR_" + ConfigClass.BulkFileName;
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            int retVal = CreateOutputFile(ConfirmedFileTrxList, processedFile);
            if (ErrorParsedRecords.Count > 0)
            {
                retVal = CreateErrorOutputFile(ErrorParsedRecords, errorFile);
            }
        }

        private int CreateOutputFile(List<FileTransaction> confirmedFileTrxList, string path)
        {
            try
            {
                string output = string.Empty;
                string templateString = "{0}" + ConfigClass.DataFileSeparator.ToString() + "{1}" + ConfigClass.DataFileSeparator.ToString() + "{2}" + ConfigClass.DataFileSeparator.ToString() + "{3}" + ConfigClass.DataFileSeparator.ToString() + "{4}";
                foreach (FileTransaction trx in confirmedFileTrxList)
                {
                    //Mobile Number|National ID|Amount|Remitter name|ReferenceNumber
                    output += string.Format(templateString, trx.MobileNumber, trx.NationalID, trx.Amount, trx.Name, trx.ReferenceNumber) + Environment.NewLine;
                }
                System.IO.File.WriteAllText(path, output);
                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "BulkHandler.CreateOutputFile", "Exception");
                return ConstantsClass.WRITE_FILE_EXCEPTION;
            }
        }

        private int CreateErrorOutputFile(List<string> errorParsedRecords, string path)
        {
            try
            {
                string output = string.Empty;
                foreach (string err in errorParsedRecords)
                {
                    //Mobile Number|National ID|Amount|Remitter name|ReferenceNumber
                    output += err + Environment.NewLine;
                }
                System.IO.File.WriteAllText(path, output);
                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "BulkHandler.CreateErrorOutputFile", "Exception");
                return ConstantsClass.WRITE_FILE_EXCEPTION;
            }
        }


        private int BulkInsertIntoDB(List<FileTransaction> fileTrxList)
        {
            LogClass.Log(string.Format("[{0}|{1}|BulkHandler.BulkInsertIntoDB] {2}", DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Information" + new string(' ', 11)).Substring(0, 11), "Number of records before insertion [" + fileTrxList.Count() + "]"), false, 0);

            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("TransactionCode", typeof(string)));
            tbl.Columns.Add(new DataColumn("Name", typeof(string)));
            tbl.Columns.Add(new DataColumn("NationalID", typeof(string)));
            tbl.Columns.Add(new DataColumn("Amount", typeof(Int32)));

            foreach (Model.FileTransaction trx in fileTrxList)
            {

                DataRow dr = tbl.NewRow();
                dr["TransactionCode"] = trx.ReferenceNumber;
                dr["Name"] = trx.Name;
                dr["NationalID"] = trx.NationalID;
                dr["Amount"] = trx.Amount;
                tbl.Rows.Add(dr);
            }
            DataBaseAccess.BulkInsertTransactions(tbl);
            return ConstantsClass.OK;
        }
        private int InsertIntoDB(List<FileTransaction> fileTrxList)
        {
            LogClass.Log(string.Format("[{0}|{1}|BulkHandler.InsertIntoDB] {2}", DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Information" + new string(' ', 11)).Substring(0, 11), "Number of records for bulk insertion [" + fileTrxList.Count() + "]"), false, 0);

            foreach (Model.FileTransaction trx in fileTrxList)
            {
                int retVal = DataBaseAccess.InsertTransaction(trx);
                if (retVal != ConstantsClass.OK)
                    LogClass.Log(string.Format("[{0}|{1}|BulkHandler.InsertIntoDB] {2}", DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "DataBaseAccess.InsertTransaction return [" + retVal.ToString() + "]"), false, 0);
                else
                    ConfirmedFileTrxList.Add(trx);
            }
            LogClass.Log(string.Format("[{0}|{1}|BulkHandler.InsertIntoDB] {2}", DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Information" + new string(' ', 11)).Substring(0, 11), "Number of records after insertion [" + ConfirmedFileTrxList.Count() + "]"), false, 0);
            return ConstantsClass.OK;
        }
    }
}

/*
 * NCR.EG.Utilities.NCRCrypto crypt = new Utilities.NCRCrypto();

            foreach (Model.FileTransaction FT in FileTrxList)
            {
                string randomPin = string.Empty;
                string encRandomPin = string.Empty;
                Model.RemittanceTrasnaction Trx = new Model.RemittanceTrasnaction();
                Trx.DepositorMobile = Trx.BeneficiaryMobile = FT.MobileNumber;
                Trx.Amount = FT.Amount;
                retVal = BuildTrxCodeClass.GetRandomPIN(ref randomPin, ConfigClass.PINLength);
                if (retVal == ConstantsClass.OK)
                {
                    Trx.BeneficiaryPIN = randomPin;
                    if (crypt.Encrypt(Trx.BeneficiaryPIN, ref encRandomPin))
                    {
                        Trx.BeneficiaryEncryptedPIN = encRandomPin;
                        encRandomPin = randomPin = "";
                        retVal = BuildTrxCodeClass.GetRandomPIN(ref randomPin, ConfigClass.PINLength);
                        if (retVal == ConstantsClass.OK)
                        {
                            Trx.DepositorPIN = randomPin;
                            if (crypt.Encrypt(Trx.DepositorPIN, ref encRandomPin))
                            {
                                Trx.DepositorEncryptedPIN = encRandomPin;
                                encRandomPin = randomPin = "";
                                RemTrxList.Add(Trx);
                            }
                            else
                            {
                                LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "Utilities.NCRCrypto.Encrypt return[" + retVal.ToString() + "] for Depositor"), false, 0);
                            }
                        }
                        else
                        {
                            LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "BuildTrxCodeClass.GetRandomPIN return[" + retVal.ToString() + "] for Depositor"), false, 0);
                        }

                    }
                    else
                    {
                        LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "Utilities.NCRCrypto.Encrypt return[" + retVal.ToString() + "] for Beneficiary"), false, 0);
                    }
                }
                else
                {
                    LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "BuildTrxCodeClass.GetRandomPIN return[" + retVal.ToString() + "] for Beneficiary"), false, 0);
                }
            }

    ***/
