# Payment
Proyecto Payment prueba TUYA
# Introducción
Para el desarrollo de la prueba técnica de TUYA realicé dos proyectos que funcionan en conjunto, uno de ellos es "ExternalServices" y el otro es "Payment".
Payment se desarrolló para ser usado directamente por el cliente (por ejemplo un SPA), el cual consta de 3 capas las cuales son:

1.Application: En esta capa va el proyecto web api con elcontrolador que se expone al cliente, además de los parámetros de configuración en el archivo appsettings.json

2.Domain: En esta capa se encuentra la librería con todo lo realionado a la lógica del negocio.

3.Infraestructure: En esta capa se encuentra la librería con todo aquello que corresponde a la comunicación con los servicios externos que expone el proyecto "ExternalServices".

El proyecto se desarrolló en .net core 3.1
# Funcionamiento

Payment se comunica con el proyecto ExternalServices para el proceso de Facturacion y pedido a través de APIs expuestas. Ambos procesos se encuentran divididos para simular
procesos totalmente independientes como muchas veces se ve en la vida real.

Antes de ejecutar el proyecto, debemos configurar las urls de los servicios externos (servicios expuestos por el otro proyecto "externalServices"), para ello debemos abrir el
archivo appsettings.json, y configurar las variables "UrlApiFacturacion" y "UrlApiCreacionPedido", teniendo en cuenta que la ruta para alcanzar las APIs es /api/facturar y /api/crearpedido
respectivamente.

Por favor tener en cuenta el puerto por el cual se va a ejecutar cada proyecto, ya que deben ser diferentes y esto se debe tener en cuenta durante la configuración
de los parámetros anteriormente mencionados.

# Consumo de la API de pagos

El proyecto Payment expone una API POST para ser consumida por el cliente (https://localhost:5001/Payment/api/generarpago), y se deben tener las siguientes consideraciones:

1. Se debe enviar un bearer token. Este no se validará, pero es necesario para efectos de la simulación. En caso de no enviarse, la transacción se rechazará.
2. El único código CVC que funciona es "123". En caso de enviar otro, el pago será rechazado y no se procederá con el proceso de pedidos.
3. El objeto para formar el request contiene la siguiente estructura:

{
   "cardNumber":"5748484914524855",
   "expirationDate":"2405",
   "cvc":"123",
   "name":"Camilo Andres Orrego alzate",
   "productos":[
      {
         "description":"pantalon",
         "price":12000
      },
      {
         "description":"camisa",
         "price":80000
      },
      {
         "description":"zapatos",
         "price":220000
      },
      {
         "description":"ropa interior",
         "price":50000
      },
      {
         "description":"camiseta",
         "price":45000
      }
   ]
}

4. El array de "productos" puede contener tantos productos como desee.
