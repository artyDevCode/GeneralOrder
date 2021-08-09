using System;
using System.Collections.Specialized;
using System.Linq;
using System.Configuration;
using GeneralOrderCore;
using ApplicationLogger;
using System.Windows.Documents;

namespace GeneralOrderCore
{
    public static class AppConfig
    {

        /// <summary>
        /// CreateConnectionString handles the connection to a SQL database (ILD). It returns the DB Connection back to the calling class
        /// AppConfig
        /// </summary>
        /// <returns></returns>
        public static object[] GetAppSettings() //string datasource, string initialCatalog, string userId, string password)
        {
            NameValueCollection config = null;
            try
            {
                //Reads the config file, in this case RemoveTRIMRevisions.dll.config located where the binaries for this assembly is
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";

                if (!System.IO.File.Exists(map.ExeConfigFilename))
                {
                    System.IO.File.Create(map.ExeConfigFilename); //create the file if it does not exist so the user can find where its looking for the config
                    throw new System.IO.FileNotFoundException("File not found " + map.ExeConfigFilename + "  in GetConfiguration");
                }
                Configuration libConfig = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                AppSettingsSection section = (libConfig.GetSection("appSettings") as AppSettingsSection);
                if (section != null)
                {

                    //Get the app settings parameters
                    config = new NameValueCollection
                        {
                                { section.Settings["MinFont"].Key, section.Settings["MinFont"].Value },
                                { section.Settings["UIFontSize"].Key, section.Settings["UIFontSize"].Value },
                                { section.Settings["MaxFont"].Key, section.Settings["MaxFont"].Value }
                        };

                }

                return new []
                {
                    config.GetValues(0).FirstOrDefault() ,
                    config.GetValues(1).FirstOrDefault(),
                    config.GetValues(2).FirstOrDefault()                 
                };


            }
            catch (Exception e)
            {
                MessageBoxHelper.CatchExceptionMessageBox($"Error has occured -- AppConfig.GetAppSettings -- { e.Message} -- Contact IT support.");
                WriteToDbLogInstance.WriteToDbInstance.WriteLogInstance(new AppLog
                {
                    AdditionalUserInfo = "Exception",
                    LogDetail = e.Message,
                    LogDetail_Additional = "AppConfig.GetAppSettings",
                    LogTime = DateTime.Now,
                    UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    Workstation = System.Environment.MachineName
                }, "GeneralOrderCore", false);
                throw new FieldAccessException("Location =- " + System.Reflection.Assembly.GetExecutingAssembly().Location + " in GetConfiguration");
            }

        }

    }
}
