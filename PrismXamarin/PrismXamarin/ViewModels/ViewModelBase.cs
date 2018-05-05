using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismXamarin.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }



        protected List<IDisposable> Disposables = new List<IDisposable>();

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        public virtual void Destroy()
        {
            Disposables.ForEach(disp => disp.Dispose());
        }




    }
}
