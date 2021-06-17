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
    public class ClienteService : IClienteService<Cliente>
    {
        public Cliente Create(Cliente cliente)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PostAsJsonAsync("Clientes", cliente).Result;

            return response.Content.ReadAsAsync<Cliente>().Result;
        }

        public Cliente Delete(Cliente cliente)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.DeleteAsync(string.Format("Clientes/{0}", cliente.CLIENTE_ID)).Result;

            return response.Content.ReadAsAsync<Cliente>().Result;
        }

        public Cliente Find(int id)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes/{0}", id)).Result;

            return response.Content.ReadAsAsync<Cliente>().Result;
        }

        public IEnumerable<Cliente> FindByCnpj(string cliente_cnpj)
        {
            List<Cliente> clientes = new List<Cliente>();
            HttpResponseMessage response;
            Cliente cliente;

            response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes/Cnpj/{0}", cliente_cnpj)).Result;

            cliente = response.Content.ReadAsAsync<Cliente>().Result;
            
            if (cliente.CLIENTE_CNPJ != null)
            {
                clientes.Add(cliente);
            }

            return clientes;
        }

        public IEnumerable<Cliente> Index()
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync("Clientes").Result;

            return response.Content.ReadAsAsync<IEnumerable<Cliente>>().Result;
        }

        public IEnumerable<Cliente> IndexByRazao(string razao)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.GetAsync(string.Format("Clientes/Razao/{0}", razao)).Result;

            return response.Content.ReadAsAsync<IEnumerable<Cliente>>().Result;
        }

        public Cliente Update(Cliente cliente)
        {
            HttpResponseMessage response = GlobalVariables.ApiClient.PutAsJsonAsync(string.Format("Clientes/{0}", cliente.CLIENTE_ID), cliente).Result;

            return response.Content.ReadAsAsync<Cliente>().Result;
        }
    }
}