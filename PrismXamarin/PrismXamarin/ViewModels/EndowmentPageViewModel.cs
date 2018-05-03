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

        private ObservableCollection<Skill> _dynamicUserNames = new ObservableCollection<Skill>();
	    private ObservableCollection<Skill> _originalUserNames = new ObservableCollection<Skill>();

	    public ObservableCollection<Skill> DynamicUserNames
	    {
	        get => _dynamicUserNames;
	        set => _dynamicUserNames = value;
	    }

	    public string Filter
	    {
	        get => _filter;
	        set
	        {
	            DynamicUserNames = FilterSkillSet(value);
	            _filter = value;
	            RaisePropertyChanged("DynamicUserNames");
            }
	    }

	    private ObservableCollection<Skill> FilterSkillSet(string filter)
	    {
	        return string.IsNullOrWhiteSpace(filter) ? _originalUserNames: new ObservableCollection<Skill>(_dynamicUserNames.Where(skill => skill.Name.Substring(0, filter.Length) == filter));
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
					    _dynamicUserNames.Add(newSkill);
                        _originalUserNames.Add(newSkill);
					});
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
