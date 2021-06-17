using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Interno
{
    public static class GlobalVariables
    {
        public static HttpClient ApiClient = new HttpClient();

        static GlobalVariables()
        {
            //ApiClient.BaseAddress = new Uri("");
            ApiClient.BaseAddress = new Uri("http://localhost:4000/api/");
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}