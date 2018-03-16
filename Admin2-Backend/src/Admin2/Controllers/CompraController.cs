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
    public class CompraController : Controller
    {
        private readonly ICompraAppService appService;
        private readonly CompraValidator validator;

        public CompraController(ICompraAppService appService, CompraValidator validator)
        {
            this.appService = appService;
            this.validator = validator;
        }

        [HttpPost]
        public GenericResult<Compra> Save([FromBody]Compra model)
        {
            var validator = this.validator.Validate(model);

            if (validator.IsValid)
                return appService.Save(model);

            else
            {
                var result = new GenericResult<Compra>();
                result.Errors = validator.GetErrors();
                return result;
            }
        }

        [HttpPost]
        public GenericResult<IEnumerable<Compra>> List([FromBody]CompraFilter filter)
        {
            return appService.List(filter);
        }

        //public GenericResult<Compra> Get(int id)
        //{
        //    //return appService.GetById(id);
        //    return new GenericResult<Compra>();
        //}

        [HttpPost]
        public GenericResult<bool> Delete([FromBody]int id)
        {
            return appService.Delete(id);
        }
    }
}
