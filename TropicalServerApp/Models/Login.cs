using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TropicalServerApp.Models
{
    public class Login
    {
       

            [Required]
            public string loginID { get; set; }
            [Required]
            public string ThisPassword { get; set; }

        
    }
}