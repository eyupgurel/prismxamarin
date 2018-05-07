using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using PrismXamarin.Models;
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
            var np = new NavigationParameters {{"selected", new List<string>() {"hikalkan", "vigo"}}};

            NavigationService.NavigateAsync("EndowmentPage", np);
        }


        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            NavigationMode navigationMode = parameters.GetNavigationMode();
            if (navigationMode == NavigationMode.Back)
            {
                var skills = (ObservableCollection<Skill>) parameters["skills"];

                int i = 0;


            }
        }
    }

}
