using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace İlkProjeWebUI.Models
{
    public class Login
    {
        [Required]
        [DisplayName("Kullanıcı Adınız")]
        public string UserName { get; set; }
        
        [Required]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}