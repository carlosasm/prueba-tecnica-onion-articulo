using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.Model.Configuration
{
    public class SwaggerConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public List<SwaggerEndpoint> Endpoints { get; set; }
    }
}
