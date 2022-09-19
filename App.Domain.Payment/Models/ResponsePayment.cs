using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Payment.Models
{
    public class ResponsePayment
    {
        public string IdTransaction { get; set; }
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public string StatusDelivery { get; set; }
        public DateTime? DateDelivery { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
