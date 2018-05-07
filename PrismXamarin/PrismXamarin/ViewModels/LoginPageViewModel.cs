using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;

namespace PrismXamarin.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{


	    private string _userName;
	    private string _userPassword;
        private DelegateCommand _loginCommand;

	    public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
	    {
	    }

	    public DelegateCommand LoginCommand => 
	        _loginCommand ?? (_loginCommand = new DelegateCommand(LoginExecute, () => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(UserPassword)).ObservesProperty(() => UserName).ObservesProperty(() => UserPassword));
        
        

        public string UserName
	    {
	        get => _userName;
	        set  => SetProperty(ref _userName, value);
	    }
	    public string UserPassword
	    {
	        get => _userPassword;
	        set => SetProperty(ref _userPassword, value);
        }
        

	    private void LoginExecute()
	    {
	        NavigationService.NavigateAsync("NavigationPage/EndowmentPage");
	    }
	}
}
