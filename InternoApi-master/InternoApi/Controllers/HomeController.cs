using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternoApi.Controllers
{
    public class HomeController : Controller
    {
        public object Index()
        {
            try
            {
                throw new Exception("Hello World");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }
        }
    }
}
