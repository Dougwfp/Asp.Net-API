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
    public class Cliente_cService : ICliente_cService<Cliente_c>
    {
        public Cliente_c Create(Cliente_c cliente)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PostAsJsonAsync("Clientes_c", cliente).Result;

            return cliente;
        }

        public Cliente_c Delete(Cliente_c cliente)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.DeleteAsync(string.Format("Clientes_c/{0}", cliente.CLIENTE_C_ID)).Result;

            return response.Content.ReadAsAsync<Cliente_c>().Result;
        }

        public void Delete(int cliente_id)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.DeleteAsync(string.Format("Clientes/{0}/Clientes_c", cliente_id)).Result;
        }

        public Cliente_c Find(int id)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes_c/{0}", id)).Result;

            return response.Content.ReadAsAsync<Cliente_c>().Result;
        }

        public Cliente_c FindByRazao(string razao)
        {
            Cliente_c cliente_c = null;
            HttpResponseMessage response;


            try
            {
                response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes_c/Razao/Find/?razao={0}", razao)).Result;

                var result = response.Content.ReadAsAsync<IEnumerable<Cliente_c>>().Result;

                if (result.Count() == 0)
                {
                    return null;
                }

                cliente_c = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }

            return cliente_c;


        }

        public IEnumerable<Cliente_c> Index()
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync("Clientes_c").Result;

            return response.Content.ReadAsAsync<IEnumerable<Cliente_c>>().Result;
        }

        public IEnumerable<Cliente_c> IndexByRazao(string razao)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes_c/Razao/{0}", razao)).Result;

            return response.Content.ReadAsAsync<IEnumerable<Cliente_c>>().Result;
        }

        public IEnumerable<Cliente_c> IndexByClienteId(int cliente_id)
        { 
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes/{0}/Clientes_c", cliente_id)).Result;

            return response.Content.ReadAsAsync<IEnumerable<Cliente_c>>().Result;
        }

        public Cliente_c Update(Cliente_c cliente)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PutAsJsonAsync(string.Format("Clientes_c/{0}", cliente.CLIENTE_C_ID), cliente).Result;

            return response.Content.ReadAsAsync<Cliente_c>().Result;
        }
    }
}