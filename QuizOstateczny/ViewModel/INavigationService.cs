using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizOstateczny.ViewModel
{
    internal interface INavigationService
    {
        void NavigateTo(string viewName);
    }
}
