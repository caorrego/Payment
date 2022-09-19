using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Payment.Models
{
    public class RequestPayment
    {
        public string cardNumber { get; set; }
        public string expirationDate { get; set; }
        public string cvc { get; set; }
        public string name { get; set; }
        public List<Product> productos { get; set; } = new List<Product>();

    }

    public class Product
    {
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
