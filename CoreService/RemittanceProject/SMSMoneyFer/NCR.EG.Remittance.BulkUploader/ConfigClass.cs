using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NCR.EG.Remittance.BulkUploader
{
    public class ConfigClass
    {
        private static string ServiceExecutablePath = "";
        //License
        //private static string LicenseName = "";
        //private static string LicensePath = "";
        //public static NCR.EG.Utilities.License TheCurrentLicense { get; internal set; }
        //Databae
        private static string DataSource;
        private static string InitialCatalog;
        private static string IntegratedSecurity;
        private static string UserId;
        private static string Password;
        public static string ConnectionString { get; internal set; }
        //Logs and Debugs
        public static bool DebugModeLog;
        public static string LogPath { get; internal set; }

        // Email Data
        public static bool SendEmail = false;
        public static string SmtpServer;
        public static string SmtpUserName;
        public static string SmtpUserPass;
        public static string SmtpPort;
        public static string FromMail;
        public static string EnableSSL;
        public static string UseDefaultCredentials;
        public static string BankCode;
        public static List<string> EmailTo = new List<string>();

        //Service Configuration
        public static string BulkFilePath { get; internal set; }
        public static string BulkFileName { get; internal set; }

        public static int CheckFileInterval { get; internal set; }
        public static char DataFileSeparator { get; internal set; }
        public static int MinimumDataNumber { get; internal set; }
        public static string OutputFilePath { get; internal set; }
        public static string OutputFileName { get; internal set; }
        public static string ErrorOutputFilePath { get; internal set; }
        public static bool BulkFileHaveHeader { get; internal set; }

        public static int ReadConfig()
        {
            System.Xml.XmlDocument xmlD;
            System.Xml.XmlNodeList items;
            System.Xml.XmlNode item;
            string tstr;
            string cfgFile = "";
            string tmp;
            int shortnameStart;
            int prevIndex;

            //Read config.xml
            try
            {
                tmp = System.Reflection.Assembly.GetExecutingAssembly().Location;
                shortnameStart = 0;
                prevIndex = 0;
                while (shortnameStart != -1)
                {
                    prevIndex = shortnameStart;
                    shortnameStart = tmp.IndexOf(@"\", shortnameStart + 1);
                }
                ServiceExecutablePath = tmp.Substring(0, prevIndex + 1);
                cfgFile = ServiceExecutablePath + "config.xml";

                xmlD = new System.Xml.XmlDocument();
                xmlD.Load(cfgFile);
            }
            catch (Exception ex)
            {
                LogClass.LogEvent("Can Not Load " + cfgFile + "  ex:" + ex.Message, EventLogEntryType.Error);
                return ConstantsClass.ERROR_LOADING_CONFIG_FILE;
            }
            LogClass.LogEvent("Configuration File loaded successfully file: " + cfgFile, EventLogEntryType.Information);
            LogClass.Log(Environment.NewLine + "======================= START =======================", false, 0);
            //Read Main Configuration
            try
            {
                items = null;
                item = null;
                items = xmlD.GetElementsByTagName("NCRConfiguration");

                item = items[0];

                foreach (System.Xml.XmlNode tmpItem in item.ChildNodes)
                {
                    if (tmpItem.Name.ToUpper() == "BankCode".ToUpper())
                        BankCode = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "DebugMode".ToUpper())
                    {
                        tstr = tmpItem.InnerText;
                        try
                        {
                            DebugModeLog = tstr == "1" ? true : false;
                        }
                        catch (Exception)
                        {
                            DebugModeLog = false;
                        }
                    }
                    else if (tmpItem.Name.ToUpper() == "LogPath".ToUpper())
                        LogPath = tmpItem.InnerText;
                    //else if (tmpItem.Name.ToUpper() == "LicenseName".ToUpper())
                    //{
                    //    LicenseName = tmpItem.InnerText;
                    //    if (LicenseName == "")
                    //    {
                    //        LogClass.LogEvent("Error Reading License Name For " + LogClass.EventSourceName, EventLogEntryType.Error);
                    //        return ConstantsClass.ERROR_READING_LICENSE_NAME;
                    //    }
                    //}
                }
                LogClass.Log("[*] Bank Code " + BankCode, false, 0);
                LogClass.Log("[*] Debug Mode " + DebugModeLog, false, 0);
                LogClass.Log("[*] Logs Path " + LogPath, false, 0);
                //LogClass.Log("[*] License Name " + LicenseName, false, 0);

                //if (!DebugModeLog)
                //{
                //    Parse License
                //    LicensePath = ServiceExecutablePath + LicenseName;
                //    if (!File.Exists(LicensePath))
                //    {
                //        LogClass.LogEvent("Can Not Load " + LicensePath + " Service Will Stop", EventLogEntryType.Error);
                //        return ConstantsClass.ERROR_READING_LICENSE_PATH;
                //    }

                //    TheCurrentLicense = new NCR.EG.Utilities.License(LicensePath, LogClass.AppName);
                //    if (TheCurrentLicense.LicenseStatus == false)
                //    {
                //        LogClass.Log("Error Loading License Service will stop", true, 0);
                //        LogClass.LogEvent("Error Loading License Service will stop", EventLogEntryType.Error);
                //        return ConstantsClass.ERROR_READING_LICENSE;
                //    }
                //    else
                //    {
                //        LogClass.Log("Read License Complete", false, 0);
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogClass.LogEvent("Error Reading General Configuration Settings ex:" + ex.Message, EventLogEntryType.Error);
            }
            //Read Database configuration
            try
            {
                items = null;
                item = null;
                items = xmlD.GetElementsByTagName("Database");

                item = items[0];

                foreach (System.Xml.XmlNode tmpItem in item.ChildNodes)
                {
                    if (tmpItem.Name.ToUpper() == "DataSource".ToUpper())
                        DataSource = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "InitialCatalog".ToUpper())
                        InitialCatalog = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "IntegratedSecurity".ToUpper())
                        IntegratedSecurity = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "UserId".ToUpper())
                        UserId = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "Password".ToUpper())
                        Password = tmpItem.InnerText;
                }

                ConnectionString = "data source=" + DataSource;
                ConnectionString = ConnectionString + ";initial catalog=" + InitialCatalog;
                ConnectionString = ConnectionString + ";integrated security=" + IntegratedSecurity;
                ConnectionString = ConnectionString + ";user id=" + UserId;
                string pPwd = "";
                NCR.EG.Utilities.NCRCrypto et;
                et = new NCR.EG.Utilities.NCRCrypto();
                if (et.Decrypt(Password, ref pPwd) == false)
                {
                    LogClass.LogEvent("Can Not Decrypt Database PWD", EventLogEntryType.Information);
                    return ConstantsClass.ERROR_DECRYPT_STRING;
                }
                var dummyConn = ConnectionString + ";password=" + new string('*', pPwd.Length);
                ConnectionString = ConnectionString + ";password=" + pPwd;
                LogClass.LogEvent("Database Connection Settings :[" + dummyConn + "]", EventLogEntryType.Information);
                LogClass.Log("[*] Database Connection Settings:[" + dummyConn + "]", false, 0);
            }
            catch (Exception ex)
            {
                LogClass.LogEvent("Error Reading Database Connection Settings ex:" + ex.Message, EventLogEntryType.Error);
            }
            //Read Listener configuration

            //read  Email Configuration
            try
            {
                items = null;
                item = null;
                items = xmlD.GetElementsByTagName("Email");
                item = items[0];
                foreach (System.Xml.XmlNode tmpItem in item.ChildNodes)
                {
                    if (tmpItem.Name.ToUpper() == "SendEmail".ToUpper())
                    {
                        var data = tmpItem.InnerText;
                        if (data == "1")
                            SendEmail = true;
                    }
                    else if (tmpItem.Name.ToUpper() == "SmtpServer".ToUpper())
                        SmtpServer = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "SmtpUserName".ToUpper())
                        SmtpUserName = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "SmtpUserPass".ToUpper())
                        SmtpUserPass = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "smtpPort".ToUpper())
                        SmtpPort = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "FromMail".ToUpper())
                        FromMail = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "EnableSSL".ToUpper())
                        EnableSSL = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "UseDefaultCredentials".ToUpper())
                        UseDefaultCredentials = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "BankCode".ToUpper())
                        BankCode = tmpItem.InnerText;
                    else if (tmpItem.Name.ToUpper() == "SendEmailTo".ToUpper())
                    {
                        var ems = tmpItem.InnerText;
                        string[] separator = new[] { "|" };
                        var arr = ems.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        EmailTo.AddRange(arr);
                        EmailTo.Add("YOUSSEF.MOUNIR@ncr.com");
                        EmailTo.Add("Mina.Anis@ncr.com");
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.LogEvent("Error Reading Email Data ex:" + ex.ToString(), EventLogEntryType.Error);
            }
            //read  Service configuration
            try
            {
                items = null;
                item = null;
                items = xmlD.GetElementsByTagName("ServiceConfiguration");
                item = items[0];
                foreach (System.Xml.XmlNode tmpItem in item.ChildNodes)
                {
                    if (tmpItem.Name.ToUpper() == "BulkFilePath".ToUpper())
                    {
                        BulkFilePath = tmpItem.InnerText;
                        if (!BulkFilePath.EndsWith(@"\"))
                            BulkFilePath += @"\";

                    }
                    else if (tmpItem.Name.ToUpper() == "BulkFileName".ToUpper())
                    {
                        BulkFileName = tmpItem.InnerText;

                    }
                    else if (tmpItem.Name.ToUpper() == "OutputFilePath".ToUpper())
                    {
                        OutputFilePath = tmpItem.InnerText;
                        if (!OutputFilePath.EndsWith(@"\"))
                            OutputFilePath += @"\";
                    }
                    else if (tmpItem.Name.ToUpper() == "OutputFileName".ToUpper())
                    {
                        OutputFileName = tmpItem.InnerText;
                    }
                    else if (tmpItem.Name.ToUpper() == "ErrorOutputFilePath".ToUpper())
                    {
                        ErrorOutputFilePath = tmpItem.InnerText;
                    }
                    else if (tmpItem.Name.ToUpper() == "CheckFileInterval".ToUpper())
                    {
                        string data = tmpItem.InnerText;
                        int intTmp = 0;
                        bool ok = int.TryParse(data, out intTmp);
                        if (!ok)
                            CheckFileInterval = 60 * 60 * 1000;
                        else
                            CheckFileInterval = intTmp * 60 * 1000;
                    }
                    else if(tmpItem.Name.ToUpper() == "BulkFileHaveHeader".ToUpper())
                    {
                        string data = tmpItem.InnerText;
                        BulkFileHaveHeader = (data == "1") ? true : false;
                    }
                    else if (tmpItem.Name.ToUpper() == "DataFileSeparator".ToUpper())
                    {
                        string data = tmpItem.InnerText;
                        if (string.IsNullOrEmpty(data))
                            DataFileSeparator = '|';
                        else
                            DataFileSeparator = data.ToCharArray()[0];
                    }
                    else if (tmpItem.Name.ToUpper() == "MinimumDataNumber".ToUpper())
                    {
                        string data = tmpItem.InnerText;
                        int intTmp = 0;
                        bool ok = int.TryParse(data, out intTmp);
                        if (!ok)
                            MinimumDataNumber = 4;
                        else
                            MinimumDataNumber = intTmp;
                    }
                    //else if (tmpItem.Name.ToUpper() == "PINLength".ToUpper())
                    //{
                    //    string data = tmpItem.InnerText;
                    //    int intTmp = 0;
                    //    bool ok = int.TryParse(data, out intTmp);
                    //    if (!ok)
                    //        PINLength = 4;
                    //    else
                    //        PINLength = intTmp;
                    //}
                }
            }
            catch (Exception ex)
            {
                LogClass.LogEvent("Error Reading Service Configuration ex:" + ex.ToString(), EventLogEntryType.Error);
            }
            LogClass.Log("[*] File Path:[" + BulkFilePath + "]", false, 0);
            LogClass.Log("[*] Check File Interval:[" + CheckFileInterval.ToString() + "]", false, 0);
            return ConstantsClass.OK;
        }
    }
}