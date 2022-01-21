using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceBCCR
    {
		private readonly string TOKEN = "ECEAMAMA90";
		private readonly string NOMBRE = "Eduardo Arley";
		private readonly string CORREO = "eduarley99@gmail.com";

		public double GetDolar(DateTime pFechaInicial, DateTime pFechaFinal, String pCompraoVenta)
		{

			
			DataSet dataset = null;
			string fecha_inicial, fecha_final;
			string tipoCompraoVenta;

			// Se convierten las fechas a string en el formato solicitado
			fecha_inicial = pFechaInicial.ToString("dd/MM/yyyy");
			fecha_final = pFechaFinal.ToString("dd/MM/yyyy");

			// se compara si es compra o venta 318 o 317
			tipoCompraoVenta = pCompraoVenta.Equals("c", StringComparison.InvariantCulture) ? "317" : "318";

			// Protocolo de comunicaciones
			System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

			// Se instancia al Servicio Web
			BCCR.wsindicadoreseconomicosSoapClient client =
				new BCCR.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");

			// Se invoca.
			dataset = client.ObtenerIndicadoresEconomicos(tipoCompraoVenta, fecha_inicial,
						  fecha_final, NOMBRE, "N", CORREO, TOKEN);

			DataTable table = dataset.Tables[0];
			Double dolar=0d;
			foreach (DataRow row in table.Rows)
			{
				// Validar el error. No es la forma correcta pero bueno.
				if (row[0].ToString().Contains("error"))
				{
					throw new Exception(row[0].ToString());
				}

				
				dolar = Convert.ToDouble(row[2].ToString());
				
			}

			return dolar;
		}

	}
}
