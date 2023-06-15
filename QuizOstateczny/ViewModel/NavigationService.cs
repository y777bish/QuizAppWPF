using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizOstateczny.ViewModel
{
    internal class NavigationService : INavigationService
    {
        public void NavigateTo(string viewName)
        {
            // Perform any necessary logic before navigating to the specified view

            // Create an instance of the desired view
            var viewType = Type.GetType($"QuizOstateczny.View.{viewName}");
            var view = (Window)Activator.CreateInstance(viewType);

            // Set the DataContext if needed
            var viewModelType = Type.GetType($"QuizOstateczny.ViewModel.{viewName}ViewModel");
            var viewModel = (MainMenuViewModel)Activator.CreateInstance(viewModelType);
            view.DataContext = viewModel;

            // Show the view
            view.Show();
        }
    }
}

