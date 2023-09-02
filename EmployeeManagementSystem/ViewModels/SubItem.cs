using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeeManagementSystem.ViewModels
{
    public class SubItem
    {
        private readonly RelayCommand _changeCommand;
        public ICommand ChangeCommand => _changeCommand;
        public SubItem(string name, UserControl screen = null)
        {
            _changeCommand = new RelayCommand(ChangePage);
            Name = name;
            Screen = screen;
        }
        public string Name { get; private set; }
        public UserControl Screen { get; private set; }
        public void ChangePage(object e)
        {
        }
    }
}
