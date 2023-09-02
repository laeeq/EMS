using EmployeeManagementSystem.Config;
using EmployeeManagementSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        private readonly RelayCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand;
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; NotifyOfPropertyChange("Url"); }
        }

        public SettingsViewModel()
        {
            var config =AppConfig.Load();
            url = config.APIURL;
            _saveCommand = new RelayCommand(Save);
        }
        public void Save(object param)
        {
            string url = param.ToString();
            if (string.IsNullOrEmpty(param.ToString()))
                AppHelper.ShowErrorMessage("Please enter the API Url");
            else 
            {
                var config = AppConfig.Load();
                config.APIURL = url;
                if (config.Save())
                    AppHelper.ShowInfoMessage("Config has been saved successfully!");
                else
                    AppHelper.ShowErrorMessage("Config could not be saved");
            }
        }
    }
}
