using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gmg.Monedero.Catalogo.Service.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("api/v1.0/[controller]/[action]")]
    [Produces("application/json")]
    public class ArticuloController : ControllerBase
    {
        #region Atributos
        private readonly PruebaTecnica.BusinessLogic.Interface.IArticulo businessLogicArticulo;

        #endregion


        public ArticuloController(PruebaTecnica.BusinessLogic.Interface.IArticulo businessLogicArticulo)
        {
            this.businessLogicArticulo = businessLogicArticulo;
        }

        [HttpPost]
        public async Task<IEnumerable<PruebaTecnica.Model.Articulo>> Listar()
        {
            try
            {
                return businessLogicArticulo.Listar();
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                throw;
            }
        }
    }
}
