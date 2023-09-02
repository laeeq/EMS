using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Config
{
    internal class AppConfig
    {
        public string APIURL { get; set; } = "";


        public bool Save()
        {
            bool success = false;

            var contents = JsonSerializer.Serialize(this);
            var path = FilePath;
            File.WriteAllText(path, contents);

            success = true;

            return success;
        }
        public static AppConfig Load()
        {
            AppConfig config = new AppConfig();
            try
            {
                if (IsConfigExists())
                {
                    var contents = File.ReadAllText(Path.Combine(AppFolderPath, GlobalConfig.FileName));
                    if (!string.IsNullOrEmpty(contents))
                    {
                        config = JsonSerializer.Deserialize<AppConfig>(contents);
                    }
                }
            }
            catch (Exception ex) { }
            return config;
        }
        public static bool IsConfigExists()
        {
            return File.Exists(FilePath);
        }
        public static string AppFolderPath
        {
            get
            {
                string path = "";
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = Path.Combine(appData, GlobalConfig.CompanyName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(path, GlobalConfig.AppName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string FilePath
        {
            get
            {
                string path = Path.Combine(AppFolderPath, GlobalConfig.FileName);
                return path;
            }
        }
        public bool VerifyConfig()
        {
            if (string.IsNullOrEmpty(APIURL))
                return false;
            return true;
        }
    }
}
