using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Prism.Navigation;
using PrismXamarin.Interface;
using PrismXamarin.Models;
using Refit;
using Xamarin.Forms;

namespace PrismXamarin.ViewModels
{
	public class EndowmentPageViewModel : ViewModelBase

    {
        //private ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();

        //public ObservableCollection<Skill> Skills
        //{
        //    get => _skills;
        //    set => _skills = value;
        //}

        //public EndowmentPageViewModel()
        //   {
        //       Load();
        //   }

        //private void Load()
        //{
        //       _skills.Add(new Skill
        //       {
        //           IsRequired = true,
        //           Name = "Accounting"
        //       });
        //


        private ObservableCollection<Skill> _userNames = new ObservableCollection<Skill>();
	    public EndowmentPageViewModel(INavigationService navigationService)
	        : base(navigationService)
	    {
	        Title = "Endowment Page";
	        Load();
	      
        }

	    private void Load()
	    {
	        var gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
	        IObservable<ApiResponse> istanbulUsers = gitHubApi.GetIstanbulUsers();

	        var disp = istanbulUsers.Subscribe(resp =>
	            {
	                resp.items.ForEach(user => _userNames.Add(new Skill{
                        IsRequired = true,
                        Name = user.ToString(),
                        Surname = user.ToString() + user.ToString()
	                }));
	            },
	            ex => {
	                string error = ex.Message;
	            },
	            () => { }
	        );
	        disposables.Add(disp);
	    }

	    public ObservableCollection<Skill> UserNames
	    {
	        get { return _userNames; }
	    }





    }


}
