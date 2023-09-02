using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Events
{
    internal class ChangePageEvent
    {
        internal string page = "";
        public ChangePageEvent(string Page)
        {
            page = Page;
        }
    }
}
