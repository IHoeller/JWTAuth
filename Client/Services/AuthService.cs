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
                // Contains all three roles (role1, role2, role3)
                //result = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidGVzdCIsImVtYWlsIjoidGVzdEBnbWFpbC5jb20iLCJyb2xlcyI6WyJyb2xlMSIsInJvbGUyIiwicm9sZTMiXX0.jOuWeyI3SReGT3NncX3qxDaS8krTsjMrzmFvza3x4xM";
                // Contains two roles (role1, role3)
                result = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidGVzdCIsInJvbGVzIjpbInJvbGUxIiwicm9sZTMiXX0.oMFZvNC7GXEGU - MO4txeRuvkFOec9EcQ0MRpf5ywp8w";

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
