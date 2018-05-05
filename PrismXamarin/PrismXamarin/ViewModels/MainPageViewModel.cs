using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reactive.Subjects;
using PrismXamarin.Models;
using Unity.ObjectBuilder.Policies;
using PrismXamarin.Interface;
using Refit;
using System.Reactive.Linq;
using System.Threading;

namespace PrismXamarin.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
        }

    }
}
