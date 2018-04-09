using System;
using Refit;
using PrismXamarin.Model;

namespace PrismXamarin.Interface
{
    [Headers("User-Agent: :request:")]
    interface IGitHubApi
    {
        [Get("/search/users?q=location:london")]
        IObservable<ApiResponse> GetLondonUsers();
        [Get("/search/users?q=location:istanbul")]
        IObservable<ApiResponse> GetIstanbulUsers();
    }
}