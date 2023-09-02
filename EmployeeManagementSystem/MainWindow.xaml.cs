using EmployeeManagementSystem.ViewModels;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            //ThemeManager.Current.ChangeTheme(this,"Light.Blue");
            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Employees"));

            var item2 = new ItemMenuViewModel(vm,"Menu", menuReports, PackIconKind.CallToAction);
            var item3 = new ItemMenuViewModel(vm,"Settings", new UserControl(), PackIconKind.Gear);
            var item4 = new ItemMenuViewModel(vm,"Support", new UserControl(), PackIconKind.Message);

            Menu.Children.Add(new UserControlMenuItem(item2));
            Menu2.Children.Add(new UserControlMenuItem(item3));
            Menu2.Children.Add(new UserControlMenuItem(item4));
            this.DataContext = vm;
        }
    }
}
