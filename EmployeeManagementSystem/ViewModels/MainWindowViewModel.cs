using Caliburn.Micro;
using EmployeeManagementSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private readonly RelayCommand _changeCommand;
        public ICommand ChangeCommand => _changeCommand;
        public string Title { get; set; } = "EMS";
        private ViewModelBase selectedViewModel;

        public ViewModelBase SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; NotifyOfPropertyChange("SelectedViewModel");  }
        }
        private string version;

        public string Version
        {
            get { return version; }
            set { version = value; NotifyOfPropertyChange("Version"); }
        }
        public MainWindowViewModel()
        {

            Version = AppHelper.GetVersion();
        }
    }
}
