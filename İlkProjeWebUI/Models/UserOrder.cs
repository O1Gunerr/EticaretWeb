using İlkProjeWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace İlkProjeWebUI.Models
{
    public class UserOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public Decimal Total { get; set; }
        public EnumOrderState OrderState { get; set; }
        public DateTime Date { get; set; }
    }

}