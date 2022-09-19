using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Payment.Models
{
    public class RequestCrearPedido
    {
        public string IdTransaction { get; set; }
        public decimal Price { get; set; }
        public string Order { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
