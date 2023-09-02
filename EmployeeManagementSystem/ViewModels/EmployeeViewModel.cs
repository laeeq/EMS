using Caliburn.Micro;
using EmployeeManagementSystem.Config;
using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Action = System.Action;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeViewModel : PropertyChangedBase
    {
        private object lockItem=new object();
        private readonly RelayCommand _closeCommand;
        public ICommand CloseCommand => _closeCommand;
        private readonly RelayCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand;
        public Action CloseAction { get; set; }
        private Employee employee;
        
        private string addTxt;

        public string AddTxt
        {
            get { return addTxt; }
            set { addTxt = value; NotifyOfPropertyChange("AddTxt"); }
        }

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; NotifyOfPropertyChange("Employee"); }
        }

        public EmployeeViewModel(Employee emp)
        {
            Employee = emp;
            if (emp.Id == 0)
            {
                AddTxt = "Add";                
            }
            else
                AddTxt = "Save";
            _closeCommand = new RelayCommand(c => CloseAction());
            _saveCommand = new RelayCommand(Save);            
        }
        public void Save(object param) 
        {
            var config = AppConfig.Load();
            if (Employee.Id == 0)
            {

                Task.Run(() => {
                    try
                    {
                        EmployeeService service = new EmployeeService(config.APIURL);
                        bool result = service.AddEmployee(Employee).Result;
                        if (result)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                AppHelper.ShowInfoMessage($"Employee[{Employee.FirstName} {Employee.LastName}] has been added successfully!!");
                                CloseAction();
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
                });
            }
            else 
            {
                Task.Run(() => {
                    try
                    {
                        EmployeeService service = new EmployeeService(config.APIURL);
                        bool result = service.UpdateEmployee(Employee).Result;
                        if (result)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke((Action)(() =>
                            {
                                AppHelper.ShowInfoMessage($"Employee[{Employee.FirstName} {Employee.LastName}] has been updated successfully!!");
                                CloseAction();
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
                });
            }
        }
    }
}
