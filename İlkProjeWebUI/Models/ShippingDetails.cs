using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace İlkProjeWebUI.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Lütfen adres tanımını giriniz")]
        public string AdresBasligi { get; set; }
        
        public string Username { get; set; }


        [Required(ErrorMessage = "Lütfen adresi giriniz")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Lütfen şehir bilgisi giriniz")]
        public string sehir { get; set; }
        [Required(ErrorMessage = "Lütfen semt bilgisi giriniz")]
        public string Semt { get; set; }
        [Required(ErrorMessage = "Lütfen Mahalle bilgisi giriniz")]
        public string Mahalle {  get; set; }

        public string AdresTarif { get; set; }

    }
      
}