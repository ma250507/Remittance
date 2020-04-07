﻿using System;
using System.ServiceProcess;
using System.Threading;

namespace NCR.EG.Remittance.BulkUploader
{
    public partial class RemBulkFileService : ServiceBase
    {
        StatClass st;
        System.Threading.TimerCallback dl;
        System.Threading.Timer ti;
        string strLogDescription;
        public RemBulkFileService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            MyStart();
        }

        public void MyStart()
        {
            try
            {
                st = new StatClass();
                dl = new System.Threading.TimerCallback(FileTimerTask);
                ti = new System.Threading.Timer(dl, st, Timeout.Infinite, System.Convert.ToInt32(ConfigClass.CheckFileInterval));  // disabled
                ti.Change(0, System.Convert.ToInt32(ConfigClass.CheckFileInterval)); // enable
                LogClass.CreateLogDescription(ref strLogDescription, "RemBulkFileService.OnStart", "Informative", "Timer Started");
                LogClass.Log(strLogDescription, false, 0);
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "RemBulkFileService.OnStart", "Exception");
            }
        }

        private void FileTimerTask(object state)
        {
            StatClass st = new StatClass();

            try
            {
                st = (StatClass)state;
            }
            catch (Exception)
            {
                state = null;
            }

            try
            {
                ti.Change(Timeout.Infinite, System.Convert.ToInt32(ConfigClass.CheckFileInterval));  // disable
                BulkHandler handler = new BulkHandler();
                int retVal = handler.ProcessFile(ConfigClass.BulkFilePath + ConfigClass.BulkFileName);
                if (retVal == ConstantsClass.OK)
                {
                    LogClass.CreateLogDescription(ref strLogDescription, "RemBulkFileService.FileTimerTask", "Informative", "File Processed");
                    LogClass.Log(strLogDescription, false, 0);
                    ti.Change(System.Convert.ToInt32(ConfigClass.CheckFileInterval), System.Convert.ToInt32(ConfigClass.CheckFileInterval));    // enable
                }
                else
                {
                    LogClass.CreateLogDescription(ref strLogDescription, "RemBulkFileService.FileTimerTask", "Error", "BulkHandler.ProcessFile return [" + retVal.ToString() + "]");
                    LogClass.Log(strLogDescription, true, 0);
                    ti.Change(System.Convert.ToInt32(ConfigClass.CheckFileInterval), System.Convert.ToInt32(ConfigClass.CheckFileInterval));// enable
                }
            }
            catch (Exception ex)
            {
                LogClass.LogError(ex, "RemBulkFileService.FileTimerTask", "Exception");
            }
        }

        protected override void OnStop()
        {
        }
    }
    class StatClass
    {

    }
}
