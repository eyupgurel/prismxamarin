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
        private ObservableCollection<String> userNames= new ObservableCollection<String>();
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Main Page";
            load();
        }

        private void load()
        {
            IGitHubApi gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");

            IObservable<ApiResponse> istanbulUsers = gitHubApi.GetIstanbulUsers();

            IDisposable disp= istanbulUsers.ObserveOn(SynchronizationContext.Current).Subscribe(resp =>
                {
                    resp.items.ForEach(user => userNames.Add(user.ToString()));
                },
                ex => {
                    string error = ex.Message;
                },
                () => { }
            );
            disposables.Add(disp);
        }

        public ObservableCollection<String> UserNames
        {
            get { return userNames; }
        }

    }
}
