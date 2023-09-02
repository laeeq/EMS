using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagementSystem.Helpers
{
    internal class AppHelper
    {        
        public static void ShowInfoMessage(string msg, string Title = "Employee Management System")
        {
            MessageBox.Show(msg, Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void ShowErrorMessage(string errorMsg, string Title = "Employee Management System")
        {
            MessageBox.Show(errorMsg, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowConfirmDialogYesNo(string msg, string Title = "Employee Management System")
        {
            //MessageBox.Show("Do you want to Save Network Settings?", "Network Setting", MessageBoxButton.YesNo, MessageBoxImage.Information);
            var result = MessageBox.Show(msg, Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result;
        }
      
        public static string GetVersion()
        {
            string version = "Version: 1.0.0";
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Version Version = assembly.GetName().Version;
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                version = "Version: " + fvi.FileVersion.Substring(0, fvi.FileVersion.LastIndexOf('.'));
            }
            catch (Exception ex)
            {
                //lblVersion.Text = "Version: 1.4.2";
            }
            return version;
        }

    }
}
