using System;
using System.Diagnostics;
using System.IO;

namespace NCR.EG.Remittance.BulkUploader
{
    public class LogClass
    {
        public static string AppName = "RemittanceBulkUploader";
        public static string EventSourceName = "NCR RemittanceBulkUploader";
        public static void LogDB(string strFunction, string LogData)
        {
            string fname;
            string Logdir;
            string fulFilename;
            DirectoryInfo dinfo;
            FileInfo finfo;
            StreamWriter strmWrtr;
            fname = "DATABASE_" + DateTime.Now.ToString("yyyyMMdd") + "_" + AppName + ".log";
            Logdir = ConfigClass.LogPath;
            try
            {
                if (System.IO.Directory.Exists(Logdir) == false)
                    dinfo = System.IO.Directory.CreateDirectory(Logdir);
                else
                    dinfo = new DirectoryInfo(Logdir);
            }
            catch (Exception ex)
            {
                try
                {
                    Logdir = System.Environment.GetEnvironmentVariable("tmp") + @"\" + AppName + "_Log";
                    dinfo = Directory.CreateDirectory(Logdir);
                    LogEvent("Can Not Create Log:" + ConfigClass.LogPath + " ex:" + ex.Message.ToString(), EventLogEntryType.Error);
                }
                catch (Exception exx)
                {
                    LogEvent("Can Not Create Log:" + ConfigClass.LogPath + " ex:" + exx.Message.ToString(), EventLogEntryType.Error);
                    LogEvent(LogData, EventLogEntryType.Information);
                    return;
                }
            }


            try
            {
                fulFilename = dinfo.FullName + @"\" + fname;
                finfo = new FileInfo(fulFilename);

                if (finfo.Exists == false)
                    strmWrtr = finfo.CreateText();
                else if (finfo.Length < 100000000)
                    strmWrtr = finfo.AppendText();
                else
                {
                    try
                    {
                        finfo.CopyTo(finfo.FullName + ".log");
                    }
                    catch (Exception ex)
                    {
                        LogEvent("Can not backup Log file ex:" + ex.ToString(), EventLogEntryType.Warning);
                    }

                    strmWrtr = finfo.CreateText();
                }
                //strmWrtr.WriteLine(speratorLine);
                strmWrtr.WriteLine("[" + DateTime.Now.ToString("dd/MM HH:mm:ss.fff") + "|" + strFunction + "]");
                strmWrtr.WriteLine(LogData);
                strmWrtr.Flush();
                strmWrtr.Close();
                strmWrtr = null;
                finfo = null;
                dinfo = null;
            }
            catch (Exception)
            {
                return;
            }
        }
        public static void LogDBError(Exception ex, string strFunction, string strEventDescription, string atmId = null)
        {
            var LogStr = string.Empty;
            var src = "";
            if (ex.Source != null)
                src = ex.Source.ToString();
            if (ex.InnerException != null)
            {
                LogClass.CreateLogDescription(ref LogStr, strFunction, "Exception", strEventDescription + ex.InnerException.ToString() + " " + src);
                LogClass.LogDB(strFunction, LogStr);
            }
            else
            {
                LogClass.CreateLogDescription(ref LogStr, strFunction, "Exception", strEventDescription + ex.Message.ToString() + " " + src);
                LogClass.LogDB(strFunction, LogStr);
            }
        }
        public static void Log(string LogData, bool sepLine, int blnLogLevel, string atmId = null)
        {
            string fname;
            string Logdir;
            string fulFilename;
            DirectoryInfo dinfo;
            FileInfo finfo;
            StreamWriter strmWrtr;

            // Dim cd As String
            string speratorLine = "=====================================================";
            if (atmId != null)
                fname = DateTime.Now.ToString("yyyyMMdd") + "_" + AppName + "_" + atmId + ".log";
            else
                fname = DateTime.Now.ToString("yyyyMMdd") + "_" + AppName + ".log";
            Logdir = ConfigClass.LogPath;
            try
            {
                if (System.IO.Directory.Exists(Logdir) == false)
                    dinfo = System.IO.Directory.CreateDirectory(Logdir);
                else
                    dinfo = new DirectoryInfo(Logdir);
            }
            catch (Exception ex)
            {
                try
                {
                    Logdir = System.Environment.GetEnvironmentVariable("tmp") + @"\" + AppName + "_Log";
                    dinfo = Directory.CreateDirectory(Logdir);
                    LogEvent("Can Not Create Log:" + ConfigClass.LogPath + " ex:" + ex.Message.ToString(), EventLogEntryType.Error);
                }
                catch (Exception exx)
                {
                    LogEvent("Can Not Create Log:" + ConfigClass.LogPath + " ex:" + exx.Message.ToString(), EventLogEntryType.Error);
                    LogEvent(LogData, EventLogEntryType.Information);
                    return;
                }
            }


            try
            {
                fulFilename = dinfo.FullName + @"\" + fname;
                finfo = new FileInfo(fulFilename);

                if (finfo.Exists == false)
                    strmWrtr = finfo.CreateText();
                else if (finfo.Length < 100000000)
                    strmWrtr = finfo.AppendText();
                else
                {
                    try
                    {
                        finfo.CopyTo(finfo.FullName + ".log");
                    }
                    catch (Exception ex)
                    {
                        LogEvent("Can not backup Log file ex:" + ex.ToString(), EventLogEntryType.Warning);
                    }

                    strmWrtr = finfo.CreateText();
                }
                if (sepLine == true)
                {
                    strmWrtr.WriteLine(speratorLine);
                    //strmWrtr.WriteLine(DateTime.Now.ToString("MM/dd/yyy hh:mm:ss.fff tt"));
                }

                strmWrtr.WriteLine(LogData);
                strmWrtr.Flush();
                strmWrtr.Close();
                strmWrtr = null;
                finfo = null;
                dinfo = null;
            }
            catch (Exception)
            {
                return;
            }
        }
        public static void LogError(Exception ex, string strFunction, string strEventDescription, string atmId = null)
        {
            var LogStr = string.Empty;
            var src = "";
            if (ex.Source != null)
                src = ex.Source.ToString();
            if (ex.InnerException != null)
            {
                LogClass.CreateLogDescription(ref LogStr, strFunction, "Exception", strEventDescription + ex.InnerException.ToString() + " " + src);
                LogClass.Log(LogStr, true, 0, atmId);
            }
            else
            {
                LogClass.CreateLogDescription(ref LogStr, strFunction, "Exception", strEventDescription + ex.Message.ToString() + " " + src);
                LogClass.Log(LogStr, true, 0, atmId);
            }
        }

        public static void LogEvent(string Data, System.Diagnostics.EventLogEntryType eventType)
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(EventSourceName))
                    System.Diagnostics.EventLog.CreateEventSource(EventSourceName, "Application");
                System.Diagnostics.EventLog ev = new System.Diagnostics.EventLog();
                ev.Source = EventSourceName;
                ev.WriteEntry(Data, eventType);
            }
            catch (Exception)
            {
            }
        }
        public static void CreateLogDescription(ref string strLogDescription, string strFunction, string strEventType, string strEventDescription)
        {
            strLogDescription = "[" + DateTime.Now.ToString("dd/MM HH:mm:ss.fff") + "|" + (strEventType + new string(' ', 11)).Substring(0, 11) + "|" + strFunction + "] " + strEventDescription;
        }
    }
}