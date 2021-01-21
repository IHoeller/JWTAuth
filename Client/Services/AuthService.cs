using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace JWTAuth.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<string> Login(JWTAuth.Shared.Auth.LoginRequest loginModel)
        {
            string result = "";

            if (loginModel.Username == "test" && loginModel.Password == "test")
            {
                result = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOYW1lIjoidGVzdCIsImVtYWlsIjoidGVzdEBnbWFpbC5jb20ifQ.Qq1e_nPCnRNNJb1NF36ZVEkLT4KfHu8A2u3uEBr7S5Q";
            }

            if (result != "")
            {
                await _localStorage.SetItemAsync("authToken", result);
                ((Helpers.AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result);

                return result;
            }

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((Helpers.AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }

    }

    public interface IAuthService
    {
        Task<String> Login(JWTAuth.Shared.Auth.LoginRequest loginModel);
        Task Logout();
    }
}
