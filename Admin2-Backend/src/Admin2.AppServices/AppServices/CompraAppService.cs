using Admin2.AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Admin2.Domain.Models;
using Admin2.Domain.Filters;
using Admin2.Domain.Repositories;
using Admin2.AppServices.Results;

namespace Admin2.AppServices.AppServices
{
    internal class CompraAppService : ICompraAppService
    {
        private readonly ICompraRepository service;


        public CompraAppService(ICompraRepository service)
        {
            this.service = service;
        }

        public GenericResult<Compra> Save(Compra model)
        {
            GenericResult<Compra> result = new GenericResult<Compra>();

            try
            {
                result.Result = service.Save(model);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<Compra>> List(CompraFilter filter)
        {
            GenericResult<IEnumerable<Compra>> result = new GenericResult<IEnumerable<Compra>>();
            try
            {
                result.Result = service.List(filter);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }   

            return result;
        }

        public GenericResult<Compra> GetById(int id)
        {
            GenericResult<Compra> result = new GenericResult<Compra>();

            try
            {
                result.Result = service.GetById(id);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> Delete(int id)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                result.Result = service.Delete(id);
                if (!result.Result)
                    result.Errors = new string[] { $"Compra {id} não existe ou não pode ser deletado" };
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }
    }
}
