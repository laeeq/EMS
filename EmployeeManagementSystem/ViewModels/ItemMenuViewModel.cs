using Caliburn.Micro;
using EmployeeManagementSystem.Events;
using EmployeeManagementSystem.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels
{
    public class ItemMenuViewModel :PropertyChangedBase
    {
        private readonly RelayCommand _changeCommand;
        public ICommand ChangeCommand => _changeCommand;
        public ItemMenuViewModel(MainWindowViewModel mainVM,string header, List<SubItem> subItems, PackIconKind icon)
        {
           // eventAggregator.Subscribe(this);
            _changeCommand = new RelayCommand(ChangePage);
            Header = header;
            MainView = mainVM;
            SubItems = subItems;
            Icon = icon;
        }
        private int alwaysMinusOne;

        public int AlwaysMinusOne
        {
            get { return alwaysMinusOne; }
            set { alwaysMinusOne = value; NotifyOfPropertyChange("AlwaysMinusOne"); }
        }

        public void ChangePage(object page)
        {
            if (page != null)
            {
                if (page is string)
                {
                  if(!page.ToString().ToLower().Contains("menu"))
                        MainView.SelectedViewModel = new SettingsViewModel();
                }
                else if (page is SubItem)
                {
                    var page2 = (SubItem)page;
                    switch (page2.Name)
                    {
                        case Pages.Employees:
                            MainView.SelectedViewModel = new EmployeesViewModel();
                            AlwaysMinusOne = -1;
                            break;
                        case Pages.Settings:
                            MainView.SelectedViewModel = new SettingsViewModel();
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        public ItemMenuViewModel(MainWindowViewModel mainVM, string header, UserControl screen, PackIconKind icon)
        {
            _changeCommand = new RelayCommand(ChangePage);
            MainView = mainVM;
            Header = header;
            Screen = screen;
            Icon = icon;
        }

        public string Header { get; private set; }
        public MainWindowViewModel MainView { get; set; }
        public PackIconKind Icon { get; private set; }
        public List<SubItem> SubItems { get; private set; }
        public UserControl Screen { get; private set; }

    }
}
