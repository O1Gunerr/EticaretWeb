using İlkProjeWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace İlkProjeWebUI.Models
{
    public class Cart
    {
        private List<CartLine> _cardLines= new List<CartLine>();
        public List<CartLine> CartLines

        {
            get { return _cardLines; }
        }

        public void AddProduct(Product product, int quantity)
        {
            var line = _cardLines.Where(i => i.Product.Id == product.Id).FirstOrDefault();
            if (line == null)
            {
                _cardLines.Add(new CartLine() { Product = product, Quantity = 1 });
            }
            else 
            {
                line.Quantity += quantity; 
            }
        }


        public void DeleteProduct(Product product)
        {
            var line=_cardLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line.Quantity >1)
            {
                line.Quantity--;
            }
            else
            {
                _cardLines.Remove(line);
            }
            

        }
        public int CartCount()
        {

            return _cardLines.Sum(i => i.Quantity);
        }
        public double Total()
        {
            return _cardLines.Sum(i => i.Product.Price * i.Quantity);
        }

        public void Clear()
        {
                               
            _cardLines.Clear();
        }

        public void UpdateCart()
        {

        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity {  get; set; }
    }
}