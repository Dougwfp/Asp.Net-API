using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Interno.Interfaces;
using Interno.Models;
using Newtonsoft.Json;

namespace Interno.Services
{
    public class TransportService : ITransportService<Transport>
    {
        public Transport Create(Transport transport)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PostAsJsonAsync("Transports", transport).Result;

            return response.Content.ReadAsAsync<Transport>().Result;
        }

        public Transport Delete(Transport transport)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.DeleteAsync(string.Format("Transports/{0}", transport.TRANSP_ID)).Result;

            return response.Content.ReadAsAsync<Transport>().Result;
        }

        public Transport Find(int id)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports/{0}", id)).Result;

            return response.Content.ReadAsAsync<Transport>().Result;
        }

        public IEnumerable<Transport> FindByCnpj(string transp_cnpj)
        {
            List<Transport> transports = new List<Transport>();
            HttpResponseMessage response;
            Transport transport;

            response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports/Cnpj/{0}", transp_cnpj)).Result;

            transport = response.Content.ReadAsAsync<Transport>().Result;
            
            if (transport.TRANSP_CNPJ != null)
            {
                transports.Add(transport);
            }

            return transports;
        }

        public IEnumerable<Transport> Index()
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync("Transports").Result;

            return response.Content.ReadAsAsync<IEnumerable<Transport>>().Result;
        }

        public IEnumerable<Transport> IndexByRazao(string razao)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports/Razao/{0}", razao)).Result;

            return response.Content.ReadAsAsync<IEnumerable<Transport>>().Result;
        }

        public Transport Update(Transport transport)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PutAsJsonAsync(string.Format("Transports/{0}", transport.TRANSP_ID), transport).Result;

            return response.Content.ReadAsAsync<Transport>().Result;
        }
    }
}