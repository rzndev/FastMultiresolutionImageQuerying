using System;
using System.Reflection;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval

{
    public static class EnvironmentVariables
    {
        public static string GetProjectName()
        {
            return string.Format("{0} {1}.{2}",
                            Application.ProductName,
                            Assembly.GetExecutingAssembly().GetName().Version.Major,
                            Assembly.GetExecutingAssembly().GetName().Version.Minor);
            
        }
        public static string GetProjectsPath
        {
            get { return Environment.ExpandEnvironmentVariables(@"%APPDATA%\ContentBasedImageRetrieval\Projects\"); }
        }

        public static string GetSettingsPath
        {
            get { return Environment.ExpandEnvironmentVariables(@"%TEMP%\ContentBasedImageRetrieval\Settings\"); }
        }
    }
}