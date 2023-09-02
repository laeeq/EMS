using Caliburn.Micro;
using EmployeeManagementSystem.Models;
using System;
//using WebApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using Action = System.Action;
using System.Net.Http.Json;
using System.Windows;
using EmployeeManagementSystem.Views;
using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Config;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        
        private readonly RelayCommand _searchCommand;
        public ICommand SearchCommand => _searchCommand;
        private readonly RelayCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand;
        private readonly RelayCommand _editCommand;
        public ICommand EditCommand => _editCommand;
        private readonly RelayCommand _addCommand;
        public ICommand AddCommand => _addCommand;
        private readonly RelayCommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand;
        private string searchText;
       
        public BindableCollection<Employee> Employees { get; private set; } = new BindableCollection<Employee>();
        public List<Employee> EmployeeList { get; private set; } = new List<Employee>();
        public EmployeesViewModel()
        {
            _searchCommand = new RelayCommand(Search);
            _deleteCommand = new RelayCommand(Delete);
            _editCommand = new RelayCommand(Edit);
            _addCommand = new RelayCommand(Add);
            _refreshCommand = new RelayCommand(LoadData);
             LoadData(null);
        }  
        public async void LoadData(object param)
        {
            var config = AppConfig.Load();
            if (config.VerifyConfig())
            {
                try
                {
                    EmployeeService service = new EmployeeService(config.APIURL);
                    EmployeeList =await service.GetEmployees();
                    System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        Employees.Clear();
                        Employees.AddRange(EmployeeList);
                    }));
                }
                catch (Exception ex)
                {

                    System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        AppHelper.ShowErrorMessage(ex.Message);
                    }));
                }
            }
            else
                AppHelper.ShowInfoMessage("Please set the API Url from settings");
        }
        public async void Delete(object param)
        {
            var config = AppConfig.Load();
            if (param != null)
            {
                var emp = (Employee)param;

                try
                {
                    EmployeeService service = new EmployeeService(config.APIURL);
                    bool result = await service.DeleteEmployee((int)emp.Id);
                    if (result)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            AppHelper.ShowInfoMessage($"Employee[{emp.FirstName} {emp.LastName}] has been deleted successfully!!");
                            LoadData(null);
                        }));

                    }
                }
                catch (Exception ex)
                {

                    System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        AppHelper.ShowErrorMessage($"Error:\n {ex.Message}");
                    }));
                }
            }
        }

        public void Edit(object param)
        {
            if (param != null)
            {
                var emp = (Employee)param;
                var vm = new EmployeeViewModel(emp);
                var view = new EmployeeView() { DataContext = vm };
                vm.CloseAction += new Action(delegate ()
                {
                    LoadData(null);
                    view.Close();
                });

                view.ShowDialog();
            }
        }
        public void Add(object param)
        {
                var emp = new Employee();
                var vm = new EmployeeViewModel(emp);
                var view = new EmployeeView() { DataContext = vm };
                vm.CloseAction += new Action(delegate ()
                {
                    LoadData(null);
                    view.Close();
                });

                view.ShowDialog();
        }
        public void Search(object searchTxt)
        {
            string Searchtxt = searchTxt.ToString().ToLower();
            if (EmployeeList.Count > 0)
            {
                if (!string.IsNullOrEmpty(Searchtxt))
                {
                    string txt = Searchtxt;
                    var sorted = EmployeeList.Where(e => e.FirstName.ToLower().Contains(txt) || e.LastName.ToLower().Contains(txt) || e.Department.ToLower().Contains(txt) || e.Email.ToLower().Contains(txt)).ToList();

                    Employees.Clear();
                    Employees.AddRange(sorted);

                }
                else
                {
                    Employees.Clear();
                    Employees.AddRange(EmployeeList);
                }
            }
        }
    }
}
