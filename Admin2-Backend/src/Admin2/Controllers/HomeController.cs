using Admin2.AppServices.Interfaces;
using Admin2.AppServices.Results;
using Admin2.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IContaAppService contaService;
        private readonly ISaldoAppService saldoService;
        private readonly IVendaAppService vendaService;

        public HomeController(IVendaAppService vendaService, IContaAppService contaService, ISaldoAppService saldoService)
        {
            this.vendaService = vendaService;
            this.contaService = contaService;
            this.saldoService = saldoService;
        }

        [HttpGet]
        public GenericResult<Home> GetListsHome(bool fimLocacoes)
        {
            var home = new Home();
            GenericResult<Home> result = new GenericResult<Home>();

            home.ContasDisponiveis = vendaService.GetContasDisponiveis().Result.ToList();
            home.FimLocacoes = vendaService.GetLocacoes(fimLocacoes).Result.ToList();
            home.ContasDesativar = contaService.GetDesativar().Result.ToList();
            home.SaldoMes = saldoService.GetSaldoMes().Result.ToList();

            result.Result = home;

            return result;
        }
    }
}

