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
using Xamarin.Forms.Internals;

namespace PrismXamarin.ViewModels
{
	public class EndowmentPageViewModel : ViewModelBase

	{

	    private string _filter;

	    private ObservableCollection<Skill> _userNames = new ObservableCollection<Skill>();

	    public ObservableCollection<Skill> DynamicUserNames
	    {
	        get
	        {
	            return GetDynamicUserNames();
	        }
	    }

	    public string Filter
	    {
	        get => _filter;
	        set
	        {
	            _filter = value;
	            RaisePropertyChanged("DynamicUserNames");
            }
	    }

	    private ObservableCollection<Skill> GetDynamicUserNames()
	    {
	        return string.IsNullOrWhiteSpace(Filter) ? new ObservableCollection<Skill>(_userNames.Where(skill => true)) : new ObservableCollection<Skill>(_userNames.Where(skill => skill.Name.Substring(0, Filter.Length) == Filter));
        }

        
        
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
					resp.items.ForEach(user =>
					{
					    Skill newSkill = new Skill
					    {
					        IsRequired = true,
					        Name = user.ToString(),
					        Surname = user.ToString()
					    };
                        _userNames.Add(newSkill);
					});
				    RaisePropertyChanged("DynamicUserNames");
                },
				ex => {
					string error = ex.Message;
				},
				() => { }
			);
			disposables.Add(disp);
		}

	}


}
