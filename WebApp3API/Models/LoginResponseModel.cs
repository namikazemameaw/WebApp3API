using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp3API.Models
{
    public class LoginResponseModel
    {
        public string status { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string approve { get; set; }
    }
}