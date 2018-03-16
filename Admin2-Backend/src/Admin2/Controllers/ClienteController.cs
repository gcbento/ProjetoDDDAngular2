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
    public class ClienteController : Controller
    {
        private readonly IClienteAppService appService;
        private readonly ClienteValidator validator;

        public ClienteController(IClienteAppService appService, ClienteValidator validator)
        {
            this.appService = appService;
            this.validator = validator;
        }

        [HttpPost]
        public GenericResult<Cliente> Save([FromBody]Cliente model)
        {
            var validator = this.validator.Validate(model);

            if (validator.IsValid)
                return appService.Save(model);

            else
            {
                var result = new GenericResult<Cliente>();
                result.Errors = validator.GetErrors();
                return result;
            }
        }

        [HttpPost]
        public GenericResult<IEnumerable<Cliente>> List([FromBody]ClienteFilter filter)
        {
            return appService.List(filter);
        }

        //public GenericResult<Compra> Get(int id)
        //{
        //    //return appService.GetById(id);
        //    return new GenericResult<Compra>();
        //}

        [HttpGet]
        public GenericResult<IEnumerable<SelectModel>> GetSelectClientes()
        {
            return appService.GetSelectClientes();
        }

        [HttpPost]
        public GenericResult<bool> Delete([FromBody]int id)
        {
            return appService.Delete(id);
        }
    }
}
    