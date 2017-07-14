using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Login{ get; set; }

        [Required]
        public string Password { get; set; }
    }
}