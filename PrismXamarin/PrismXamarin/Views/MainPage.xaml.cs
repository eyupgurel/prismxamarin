using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PrismXamarin.Interface;
using Refit;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using PrismXamarin.Model;
using System.Reactive.Linq;
using System.Threading;

namespace PrismXamarin.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
                InitializeComponent();
                var listView = new ListView();
                List<User> users = new List<User>();
                List<String> user_names = new List<String>();

                JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = { new StringEnumConverter() }
                };

                IGitHubApi gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");

                IObservable<ApiResponse> istanbulUsers = gitHubApi.GetIstanbulUsers();

                istanbulUsers.ObserveOn(SynchronizationContext.Current).Subscribe(resp =>
                    {
                        users = resp.items;
                        foreach (User user in users)
                        {
                            user_names.Add(user.ToString());
                        }
                        listView.ItemsSource = user_names;
                        Padding = new Thickness(0, 20, 0, 0);
                        Content = listView;
                    },
                    ex => {
                        string error = ex.Message;
                    },
                    () => { }
                );
        }
	}
}