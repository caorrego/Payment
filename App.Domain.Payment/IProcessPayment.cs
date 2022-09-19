using App.Domain.Payment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Payment
{
    public interface IProcessPayment
    {
        ResponsePayment sendPayment(RequestPayment request, string urlFacturacion, string urlCrearPedido, string token);
    }
}
