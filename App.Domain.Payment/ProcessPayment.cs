using App.Domain.Payment.Models;
using App.Infraestructure.Payment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace App.Domain.Payment
{
    public class ProcessPayment : IProcessPayment
    {
        private ResponseCrearPedido responseCrearPedido = new ResponseCrearPedido();
        private ResponseFacturacion responseFacturacion = new ResponseFacturacion();
        public ResponsePayment sendPayment(RequestPayment request, string urlFacturacion, string urlCrearPedido, string token)
        {
            try
            {
                generarFacturacion(request, urlFacturacion, token);

                if (responseFacturacion.ResponseCode == "00")
                {
                    RequestCrearPedido requestCrearPedido = new RequestCrearPedido
                    {
                        IdTransaction = responseFacturacion.IdTransaction,
                        Order = JsonConvert.SerializeObject(request.productos),
                        OrderDate = DateTime.Now,
                        Price = responseFacturacion.TotalPrice
                    };

                    generarPedido(requestCrearPedido, urlCrearPedido, token);

                    if (responseCrearPedido.ResponseCode == "00")
                    {
                        return construirRespuestaPayment(responseCrearPedido.IdTransaction, responseFacturacion.Message + " y " + responseCrearPedido.Message, responseCrearPedido.ResponseCode, responseCrearPedido.DateDelivery, responseCrearPedido.StatusDelivery, responseFacturacion.TotalPrice);
                    }
                    else
                    {
                        return construirRespuestaPayment(null, responseCrearPedido.Message, responseCrearPedido.ResponseCode, null, "No enviado", 0);
                    }
                }
                else
                {
                    return construirRespuestaPayment(null, responseFacturacion.Message, responseFacturacion.ResponseCode, null, "No enviado", 0);
                }
            }
            catch (Exception ex)
            {
                return construirRespuestaPayment(null, ex.Message + "----" + ex.StackTrace, "99", null, "No enviado", 0);
            }
        }

        private void generarFacturacion(RequestPayment request, string urlFacturacion, string token)
        {
            generalProcess<RequestPayment> Facturar = new generalProcess<RequestPayment>();

            HttpResponseMessage message = new HttpResponseMessage();
            message = Facturar.generalApiProcess(request, urlFacturacion, token).Result;

            if (message.StatusCode != HttpStatusCode.OK)
            {
                responseFacturacion.IdTransaction = "0";
                responseFacturacion.Message = message.ReasonPhrase;
                responseFacturacion.ResponseCode = message.StatusCode.ToString();
                responseFacturacion.TotalPrice = 0;
            }
            responseFacturacion = JsonConvert.DeserializeObject<ResponseFacturacion>(message.Content.ReadAsStringAsync().Result);
        }

        private void generarPedido(RequestCrearPedido request, string urlCrearPedido, string token)
        {
            generalProcess<RequestCrearPedido> CrearPedido = new generalProcess<RequestCrearPedido>();

            HttpResponseMessage message = new HttpResponseMessage();
            message = CrearPedido.generalApiProcess(request, urlCrearPedido, token).Result;

            if (message.StatusCode != HttpStatusCode.OK)
            {
                responseCrearPedido.IdTransaction = "0";
                responseCrearPedido.Message = message.ReasonPhrase;
                responseCrearPedido.ResponseCode = message.StatusCode.ToString();
                responseCrearPedido.DateDelivery = null;
            }
            responseCrearPedido = JsonConvert.DeserializeObject<ResponseCrearPedido>(message.Content.ReadAsStringAsync().Result);
        }

        private ResponsePayment construirRespuestaPayment(string autNumber, string message, string responseCode, DateTime? dateDelivery, string statusDelivery, decimal totalPrice)
        {
            return new ResponsePayment
            {
                IdTransaction = autNumber,
                Message = message,
                ResponseCode = responseCode,
                DateDelivery = dateDelivery,
                StatusDelivery = statusDelivery,
                TotalPrice = totalPrice
            };
        }
    }
}
