﻿
namespace DrendencyDemo.Web.Models.Authentication
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
