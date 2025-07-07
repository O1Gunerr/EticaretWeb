using System;

namespace İlkProjeWebUI.Entity
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsPercentage { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
