using NCR.EG.Remittance.BulkUploader.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace NCR.EG.Remittance.BulkUploader
{
    public class BulkFileRecord
    {
        public int FieldsCount { get; internal set; }
        public int AmountIndex { get; internal set; }
        public int NationalIdIndex { get; internal set; }
        public int MobileNumberIndex { get; internal set; }
        public int NameIndex { get; internal set; }
        public int RemitterIdIndex { get; internal set; }
        public int ReferenceIndex { get; internal set; }
    }

    public class BulkHandler
    {
        List<Model.FileTransaction> FileTrxList;            //all the bulk file lines 
        List<Model.FileTransaction> ConfirmedFileTrxList;   //all the recors inside the biulk file
        List<string> ErrorParsedRecords;
        BulkFileRecord recordIndexes;
        private string headerString;
        string strLogDesc = "";
        

        public BulkHandler()
        {
            FileTrxList = new List<Model.FileTransaction>();
            ErrorParsedRecords = new List<string>();
            ConfirmedFileTrxList = new List<Model.FileTransaction>();
        }

        internal void SetRecordType(BulkFileRecord record)
        {
            recordIndexes = record;
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
                    LogClass.CreateLogDescription(ref strLogDesc, "BulkHandler2.ProcessFile", "Infromation", bulkFilePath + " Not Found");
                    LogClass.Log(strLogDesc, false, 0);
                    return ConstantsClass.ERROR_FILE_NOT_FOUND;
                }
                List<string> allLinesText = File.ReadAllLines(bulkFilePath).ToList();
                LogClass.CreateLogDescription(ref strLogDesc, "BulkHandler2.ProcessFile", "Information", "Number of records before parsing [" + allLinesText.Count() + "]");
                LogClass.Log(strLogDesc, false, 0);
                int flagHeader = 0;
                foreach (string tmpStr in allLinesText)
                {
                    if(ConfigClass.BulkFileHaveHeader && flagHeader == 0)
                    {
                        headerString = tmpStr;
                        flagHeader = 1;
                        continue;
                    }
                    //Mobile Number|National ID|Amount|Remitter name
                    if (!string.IsNullOrEmpty(tmpStr))
                    {
                        flagHeader = 1;
                        string[] data = tmpStr.Split(ConfigClass.DataFileSeparator);
                        //no missing fields??
                        if (data.Length >= recordIndexes.FieldsCount)
                        {
                            //string values not null?
                            if (!string.IsNullOrEmpty(data[recordIndexes.MobileNumberIndex].Trim()) && !string.IsNullOrEmpty(data[recordIndexes.NationalIdIndex].Trim()) && !string.IsNullOrEmpty(data[recordIndexes.NameIndex].Trim()))
                            {
                                int amt = int.MinValue;
                                //Validate amount is integer
                                bool ok = ValidateConvertToInteger(data[recordIndexes.AmountIndex].Trim(), ref amt);
                                if (!ok)
                                {
                                    ErrorParsedRecords.Add(tmpStr);
                                }

                                else
                                {
                                    int rid = int.MinValue;
                                    //Validate remitter id is integer
                                    ok = ValidateConvertToInteger(data[recordIndexes.RemitterIdIndex].Trim(), ref rid);
                                    if (!ok)
                                    {
                                        ErrorParsedRecords.Add(tmpStr);
                                    }
                                    else
                                    {
                                        Model.FileTransaction FT = new Model.FileTransaction(data[recordIndexes.MobileNumberIndex].Trim(), data[recordIndexes.NationalIdIndex].Trim(), data[recordIndexes.NameIndex].Trim(), rid, amt);
                                        retVal = FT.GenerateReference2();
                                        //Reference Number Ok?
                                        if (retVal == ConstantsClass.OK)
                                        { 
                                            FileTrxList.Add(FT);
                                        }
                                        else
                                        {
                                            ErrorParsedRecords.Add(tmpStr);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ErrorParsedRecords.Add(tmpStr);
                            }
                        }
                        else
                        {
                            ErrorParsedRecords.Add(tmpStr);
                            //LogClass.Log(string.Format(errorStr, DateTime.Now.ToString("dd/MM HH:mm:ss.fff"), ("Error" + new string(' ', 11)).Substring(0, 11), "Couldn't process the data in [" + tmpStr + "] record, data values less than " + ConfigClass.MinimumDataNumber), false, 0);
                        }
                    }
                }
                LogClass.CreateLogDescription(ref strLogDesc, "BulkHandler2.ProcessFile", "Information", "Number of records after parsing [" + FileTrxList.Count() + "]");
                LogClass.Log(strLogDesc, false, 0);
                retVal = InsertIntoDB(FileTrxList);
                GenreateOutputPackage();

                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "BulkHandler2.ProcessFile", "Exception");
                return ConstantsClass.PROCESS_FILE_EXCEPTION;
            }

        }

        private bool ValidateConvertToInteger(string v, ref int amt)
        {
            int tmpInt = 0;
            bool ok = int.TryParse(v, out tmpInt);
            amt = tmpInt;
            return ok;
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
            MoveWithReplace(ConfigClass.BulkFilePath + ConfigClass.BulkFileName, outputDirectory + ConfigClass.BulkFileName);
        }


        //delete target file if exists, as File.Move() does not support overwrite
        public static void MoveWithReplace(string sourceFileName, string destFileName)
        {
            if (File.Exists(destFileName))
            {
                File.Delete(destFileName);
            }
            File.Move(sourceFileName, destFileName);
        }

        private int CreateOutputFile(List<FileTransaction> confirmedFileTrxList, string path)
        {
            try
            {
                string output = string.Empty;
                string templateString = "{0}" + ConfigClass.DataFileSeparator.ToString() + "{1}" + ConfigClass.DataFileSeparator.ToString() + "{2}" + ConfigClass.DataFileSeparator.ToString() + "{3}" + ConfigClass.DataFileSeparator.ToString() + "{4}" + ConfigClass.DataFileSeparator.ToString() + "{5}";
                if(ConfigClass.BulkFileHaveHeader)
                {
                    if(headerString.EndsWith(";"))
                    {
                        output += headerString + "TransactionReferenceNumber;" + Environment.NewLine;
                    }
                    else
                    {
                        output += headerString + ";TransactionReferenceNumber;" + Environment.NewLine;
                    }
                }
                foreach (FileTransaction trx in confirmedFileTrxList)
                {
                    //Mobile Number|National ID|Amount|Remitter name|ReferenceNumber
                    output += string.Format(templateString, trx.MobileNumber, trx.NationalID, trx.Amount, trx.Name, trx.RemitterId , trx.ReferenceNumber) + Environment.NewLine;
                }
                System.IO.File.WriteAllText(path, output);
                return ConstantsClass.OK;
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "BulkHandler2.CreateOutputFile", "Exception");
                return ConstantsClass.WRITE_FILE_EXCEPTION;
            }
        }

        private int CreateErrorOutputFile(List<string> errorParsedRecords, string path)
        {
            try
            {
                string output = string.Empty;
                if (ConfigClass.BulkFileHaveHeader)
                {
                    output += headerString + Environment.NewLine;
                }
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
                LogClass.LogError(ex, "BulkHandler2.CreateErrorOutputFile", "Exception");
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
