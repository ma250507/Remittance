using System.ServiceProcess;

namespace NCR.EG.Remittance.BulkUploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new RemBulkFileService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
