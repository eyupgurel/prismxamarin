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
	public class LoginPageViewModel : BindableBase
	{


	    private string _userName;
	    private string _userPassword;
        private readonly Command _loginCommand;
	    private INavigationService _navigationService;


	    public LoginPageViewModel(INavigationService navigationService)
	    {
	        _navigationService = navigationService;
	        _loginCommand = new Command(LoginExecute, LoginCanExecute);
	    }


        public string UserName
	    {
	        get => _userName;
	        set
	        {
	            _userName = value;
                LoginCommand.ChangeCanExecute();
	        }
	    }
	    public string UserPassword
	    {
	        get => _userPassword;
	        set
	        {
	            _userPassword = value;
                LoginCommand.ChangeCanExecute();
	        }
	    }


	    public Command LoginCommand => _loginCommand;



	    private void LoginExecute()
	    {
	        _navigationService.NavigateAsync("NavigationPage/EndowmentPage");
	    }

	    private bool LoginCanExecute()
	    {
	        return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(UserPassword);
	    }
	}
}
