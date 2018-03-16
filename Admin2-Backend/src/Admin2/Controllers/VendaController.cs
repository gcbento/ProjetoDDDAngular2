using Admin2.AppServices.Interfaces;
using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Admin2.Extensions;
using Admin2.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VendaController : Controller
    {
        private readonly IVendaAppService appService;
        private readonly VendaValidator validator;
    
        public VendaController(IVendaAppService appService, VendaValidator validator)
        {
            this.appService = appService;
            this.validator = validator;
        }

        [HttpPost]
        public GenericResult<Venda> Save([FromBody]Venda model)
        {
            var validator = this.validator.Validate(model);

            if (validator.IsValid)
                return appService.Save(model);

            else
            {
                var result = new GenericResult<Venda>();
                result.Errors = validator.GetErrors();
                return result;
            }
        }

        [HttpPost]
        public GenericResult<IEnumerable<Venda>> List([FromBody]VendaFilter filter)
        {
            return appService.List(filter);
        }

        [HttpGet]
        public GenericResult<IEnumerable<ItemVenda>> GetItensVendaByContaId(int contaId)
        {
            return appService.GetItensVendaByContaId(contaId);
        }

        [HttpGet]
        public GenericResult<IEnumerable<SelectModel>> GetSelectJogosVenda()
        {
            return appService.GetSelectJogosVenda();
        }

        [HttpGet]
        public GenericResult<List<IEnumerable<ItemVenda>>> GetItensByMes(int ano, int mes)
        {
            return appService.GetItensByMes(ano, mes);
        }

        [HttpGet]
        public GenericResult<IEnumerable<Conta>> GetContasByJogo(string nomeJogo)
        {
            return appService.GetContasByJogo(nomeJogo);
        }

        [HttpPost]
        public GenericResult<bool> EstenderLocacao([FromBody]ItemVenda item)
        {
            return appService.EstenderLocacão(item);
        }

        [HttpPost]
        public GenericResult<bool> FinalizarLocacao([FromBody]int id)
        {
            return appService.FinalizarLocacão(id);
        }

        [HttpPost]
        public GenericResult<bool> Delete([FromBody]int id)
        {
            return appService.Delete(id);
        }
    }
}
