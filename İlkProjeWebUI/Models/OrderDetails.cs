using İlkProjeWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace İlkProjeWebUI.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public Decimal Total { get; set; }
        public DateTime Date { get; set; }

        public EnumOrderState OrderState { get; set; }
        public string Username { get; set; }

        public string Adres { get; set; }

        public string sehir { get; set; }

        public string Semt { get; set; }
        public string Mahalle { get; set; }

        public string AdresTarif { get; set; }
        public virtual List<OrderLineModel> OrderLines { get; set; }
    }

    public class OrderLineModel
    {
        public int ProductId { get; set; }
       public string ProductName {  get; set; }
        public string Image {  get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
       
    }
}