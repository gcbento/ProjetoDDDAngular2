using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin2.AppServices.Interfaces;
using Admin2.Validators;
using Admin2.AppServices.Results;
using Admin2.Extensions;
using Admin2.Domain.Models;
using Admin2.Domain.Filters;

namespace Admin2.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SaldoController : Controller
    {
        private readonly ISaldoAppService appService;

        public SaldoController(ISaldoAppService appService)
        {
            this.appService = appService;
        }

        [HttpPost]
        public GenericResult<Estatisticas> GetEstatisticas([FromBody]SaldoFilter filter)
        {
            return appService.GetEstatisticas(filter);
        }

        [HttpPost]
        public GenericResult<IEnumerable<SaldoConta>> ListSaldoContas([FromBody]SaldoFilter filter)
        {
            return appService.ListSaldoContas(filter);
        }

        [HttpGet]
        public GenericResult<IEnumerable<Estatisticas>> LisSaldoMensal(int ano)
        {
            return appService.GetSaldoMensal(ano);
        }
    }
}
