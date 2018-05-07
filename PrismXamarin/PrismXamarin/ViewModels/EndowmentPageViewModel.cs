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
        private readonly Subject<string> _sparseFilterSubject = new Subject<string>();
	    private readonly ObservableCollection<Skill> _userNames = new ObservableCollection<Skill>();

	    private DelegateCommand _changeSkillsCommand;

	    public DelegateCommand ChangeSkillsCommand => 
	        _changeSkillsCommand ?? (_changeSkillsCommand = new DelegateCommand(ChangeSkillsExecute));

	    private void ChangeSkillsExecute()
	    {
            NavigationParameters np = new NavigationParameters()
            {
                {"skills", _userNames}
            };
	        NavigationService.GoBackAsync(np);
        }

	    public ObservableCollection<Skill> DynamicUserNames => GetDynamicUserNames();

	    public string Filter
	    {
	        get => _filter;
	        set
	        {
	            _filter = value;
                _sparseFilterSubject.OnNext(_filter);
            }
	    }

	    private ObservableCollection<Skill> GetDynamicUserNames()
	    {
	        return string.IsNullOrWhiteSpace(Filter) ? new ObservableCollection<Skill>(_userNames.Where(skill => true)) : new ObservableCollection<Skill>(_userNames.Where(skill => (skill.Name.Trim().Length >= Filter.Trim().Length) && (skill.Name.Trim().Substring(0, Filter.Length).ToLower() == Filter.Trim().ToLower())));
        }
     
        
        public EndowmentPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Title = "Endowment Page";
		  
		}

		private void Load(IEnumerable<string> checkedNames)
		{
			var gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
			var istanbulUsers = gitHubApi.GetIstanbulUsers();

			var disp = istanbulUsers.Subscribe(resp =>
				{
					resp.items.ForEach(user =>
					{
					    var newSkill = new Skill
					    {
					        IsRequired = checkedNames.Contains(user.ToString()),
					        Name = user.ToString(),
					        Surname = user.ToString()
					    };
                        _userNames.Add(newSkill);
					});
				    RaisePropertyChanged("DynamicUserNames");
                },
				ex => {
					var error = ex.Message;
				},
				() => { }
			);
		    Disposables.Add(disp);

		   disp = _sparseFilterSubject.Throttle(TimeSpan.FromMilliseconds(100)).Subscribe((s => RaisePropertyChanged("DynamicUserNames")));
		   Disposables.Add(disp);
         

        }

	    public override void OnNavigatingTo(NavigationParameters parameters)
	    {
	        base.OnNavigatingTo(parameters);
            Load((IEnumerable<string>)parameters["selected"]);
	    }
    }


}
