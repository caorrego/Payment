using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Payment;
using App.Domain.Payment.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace App.Web.Payment.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PaymentController : Controller
    {
        IProcessPayment _ProcessPayment;
        string urlFacturacion;
        string urlCrearPedido;

        public PaymentController(IProcessPayment processPayment, IConfiguration configuration)
        {
            this.urlFacturacion = (configuration.GetConnectionString("UrlApiFacturacion"));
            this.urlCrearPedido = (configuration.GetConnectionString("UrlApiCreacionPedido"));
            this._ProcessPayment = processPayment;         
        }     

        [HttpPost("api/generarpago")]
        public IActionResult generarPago(RequestPayment req)
        {
            string authHeader = this.HttpContext.Request.Headers["Authorization"];
            return Ok(_ProcessPayment.sendPayment(req, this.urlFacturacion, this.urlCrearPedido, authHeader));
        }
    }
}
