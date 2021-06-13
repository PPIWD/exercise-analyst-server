using System;
using System.Collections.Generic;
using API.Services.Common;

namespace API.Services.Auth.Dtos.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public IList<string> Roles { get; set; }
        public string Email { get; set; }
        public int HeightInCm { get; set; }
        public double WeightInKg { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAtUTC { get; set; }
    }
}