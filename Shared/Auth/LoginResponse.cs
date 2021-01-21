using System;
using System.Collections.Generic;
using System.Text;

namespace JWTAuth.Shared.Auth
{
    public class LoginResponse
    {
        public int userid { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string token { get; set; }
    }
}
