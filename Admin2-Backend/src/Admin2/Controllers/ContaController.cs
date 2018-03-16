using Admin2.AppServices.Interfaces;
using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Admin2.Extensions;
using Admin2.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ContaController : Controller
    {
        private readonly IContaAppService appService;
        private readonly ContaValidator validator;

        public ContaController(IContaAppService appService, ContaValidator validator)
        {
            this.appService = appService;
            this.validator = validator;
        }

        [HttpPost]
        public GenericResult<Conta> Save([FromBody]Conta model)
        {
            var validator = this.validator.Validate(model);

            if (validator.IsValid)
                return appService.Save(model);

            else
            {
                var result = new GenericResult<Conta>();
                result.Errors = validator.GetErrors();
                return result;
            }
        }

        [HttpPost]
        public GenericResult<IEnumerable<Conta>> List([FromBody]ContaFilter filter)
        {
            return appService.List(filter);
        }

        [HttpGet]
        public GenericResult<IEnumerable<SelectModel>> GetSelectJogosConta()
        {
            return appService.GetSelectJogosConta();
        }

        //public GenericResult<Conta> Get(int id)
        //{
        //    //return appService.GetById(id);
        //    return new GenericResult<Conta>();
        //}

        [HttpPost]
        public GenericResult<bool> Delete([FromBody]int id)
        {
            return appService.Delete(id);
        }

        [HttpPost]
        public GenericResult<bool> AddJogo([FromBody]JogosConta jogosConta)
        {
            return appService.AddJogo(jogosConta);
        }

        [HttpPost]
        public GenericResult<bool> RemoverJogo([FromBody]JogosConta jogosConta)
        {
           return appService.RemoverJogo(jogosConta);
        }

        [HttpPost]
        public GenericResult<bool> UpdateCodigosConta([FromBody] CodigosConta codigo)
        {
            return appService.UpdateCodigosConta(codigo);
        }

        [HttpPost]
        public GenericResult<bool> DeleteCodigos([FromBody]int contaId)
        {
            return appService.DeleteCodigos(contaId);
        }
    }
}
