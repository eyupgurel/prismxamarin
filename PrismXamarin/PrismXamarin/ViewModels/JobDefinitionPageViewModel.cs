using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;

namespace PrismXamarin.ViewModels
{
	public class JobDefinitionPageViewModel : ViewModelBase
	{
		private DelegateCommand _addSkillCommand;

	    public JobDefinitionPageViewModel(INavigationService navigationService) : base(navigationService)
	    {
	    }

	    public DelegateCommand AddSkillCommand =>
	        _addSkillCommand ?? (_addSkillCommand = new DelegateCommand(AddSkillExecute));
        

		private void AddSkillExecute()
		{
			NavigationService.NavigateAsync("NavigationPage/EndowmentPage");
		}
	}
}
