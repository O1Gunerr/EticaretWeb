using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace İlkProjeWebUI.Entity
{
    public class Order
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
        public  virtual List<OrderLine>OrderLine { get; set; }
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity {  get; set; }
        public double Price { get; set; }
        public int ProductId {  get; set; }
        public virtual Product Product { get; set; }

       

    }
}