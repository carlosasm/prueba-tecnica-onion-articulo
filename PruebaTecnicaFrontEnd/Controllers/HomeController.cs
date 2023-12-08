using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace PruebaTecnicaFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            
        }

        public JsonResult getArticulos ()
        {
            var resultado = this.GetArticulo();
            return new JsonResult
            {
                Data = resultado
            };
        }

        #region Private Methods
        private string getToken ()
        {
            var client = new RestClient("https://login.microsoftonline.com/98048920-81ec-49aa-aa1a-1403f8889145/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_secret", "3kV.6~~Ps~U5rmeH~--B.02Og~b9VN3Wcq");
            request.AddParameter("client_id", "82f14f1e-3384-405b-8521-02734d89a75b");
            request.AddParameter("resource", "ae8563cd-ae5d-41b1-9bdc-72230dca9dee");

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var token = JsonConvert.DeserializeObject<Models.BearerToken>(response.Content);
                return token.access_token;
            }
            return string.Empty;
        }

        private IEnumerable<Models.Articulo> GetArticulo ()
        {
            var client = new RestClient("https://localhost:61413/api/v1.0/Articulo/Listar");
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {getToken()}");
            
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<IEnumerable<Models.Articulo>>(response.Content);

            return null;
        }

        #endregion 
    }
}