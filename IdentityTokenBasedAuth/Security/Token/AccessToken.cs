﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTokenBasedAuth.Security.Token
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expration { get; set; }
        public string RefreshToken { get; set; }
    }
}
