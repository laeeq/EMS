using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Config
{
    internal static class GlobalConfig
    {
        public static string AppName = "Employee Management System";
        public static string ApppNameShort = AppName.Replace(" ", ""); //"Office 365 Reports";
        public static string CompanyName = "EmpowerID";
        public const string FileName = "AppConfig.json";
    }
}
