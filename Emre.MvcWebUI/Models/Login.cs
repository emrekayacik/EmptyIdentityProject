using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emre.MvcWebUI.Models
{
    public class Login
    {
        [DisplayName("Kullanıcı Adı")]
        [Required]
        public string Username { get; set; }
        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }

    }
}