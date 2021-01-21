using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JWTAuth.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpService _httpService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(
                           HttpClient httpClient,
                           IHttpService httpService,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _httpService = httpService;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<JWTAuth.Shared.Auth.LoginResponse> Login(JWTAuth.Shared.Auth.LoginRequest loginModel)
        {
            var result = await _httpService.Post<JWTAuth.Shared.Auth.LoginResponse>("api/login", loginModel);

            if (result.token != "")
            {
                await _localStorage.SetItemAsync("authToken", result.token);
                ((Helpers.AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.token);

                return result;
            }

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((Helpers.AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

    }

    public interface IAuthService
    {
        Task<JWTAuth.Shared.Auth.LoginResponse> Login(JWTAuth.Shared.Auth.LoginRequest loginModel);
        Task Logout();
        //Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
