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
    public class Transport_cService : ITransport_cService<Transport_c>
    {
        public Transport_c Create(Transport_c transport)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PostAsJsonAsync("Transports_c", transport).Result;

            return transport;
        }

        public Transport_c Delete(Transport_c transport)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.DeleteAsync(string.Format("Transports_c/{0}", transport.TRANSP_C_ID)).Result;

            return response.Content.ReadAsAsync<Transport_c>().Result;
        }

        public void Delete(int transp_id)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.DeleteAsync(string.Format("Transports/{0}/Transports_c", transp_id)).Result;
        }

        public Transport_c Find(int id)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports_c/{0}", id)).Result;

            return response.Content.ReadAsAsync<Transport_c>().Result;
        }

        public Transport_c FindByRazao(string razao)
        {
            Transport_c transport_c = null;
            HttpResponseMessage response;


            try
            {
                response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports_c/Razao/Find/?razao={0}", razao)).Result;

                var result = response.Content.ReadAsAsync<IEnumerable<Transport_c>>().Result;

                if (result.Count() == 0)
                {
                    return null;
                }

                transport_c = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }

            return transport_c;


        }

        public IEnumerable<Transport_c> Index()
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync("Transports_c").Result;

            return response.Content.ReadAsAsync<IEnumerable<Transport_c>>().Result;
        }

        public IEnumerable<Transport_c> IndexByRazao(string razao)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports_c/Razao/{0}", razao)).Result;

            return response.Content.ReadAsAsync<IEnumerable<Transport_c>>().Result;
        }

        public IEnumerable<Transport_c> IndexByTranspId(int transp_id)
        { 
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Transports/{0}/Transports_c", transp_id)).Result;

            return response.Content.ReadAsAsync<IEnumerable<Transport_c>>().Result;
        }

        public Transport_c Update(Transport_c transport)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PutAsJsonAsync(string.Format("Transports_c/{0}", transport.TRANSP_C_ID), transport).Result;

            return response.Content.ReadAsAsync<Transport_c>().Result;
        }
    }
}