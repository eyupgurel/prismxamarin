using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PrismXamarin.Interface;
using Refit;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using PrismXamarin.Models;
using System.Reactive.Linq;
using System.Threading;

namespace PrismXamarin.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent();
		}
	}
}