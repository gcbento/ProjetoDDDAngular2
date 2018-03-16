using Admin2.AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Admin2.Domain.Repositories;

namespace Admin2.AppServices.AppServices
{
    internal class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository service;


        public ClienteAppService(IClienteRepository service)
        {
            this.service = service;
        }

        public GenericResult<Cliente> Save(Cliente model)
        {
            GenericResult<Cliente> result = new GenericResult<Cliente>();

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

        public GenericResult<IEnumerable<Cliente>> List(ClienteFilter filter)
        {
            GenericResult<IEnumerable<Cliente>> result = new GenericResult<IEnumerable<Cliente>>();
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

        
        public GenericResult<IEnumerable<SelectModel>> GetSelectClientes()
        {
            GenericResult<IEnumerable<SelectModel>> result = new GenericResult<IEnumerable<SelectModel>>();
            try
            {
                List<SelectModel> select = new List<SelectModel>();
                var clientes = service.List(new ClienteFilter());


                foreach (var cliente in clientes)
                {
                    SelectModel model = new SelectModel();
                    model.Value = cliente.Id.ToString();
                    model.Label = cliente.Nome;

                    select.Add(model);
                }

                result.Result = select.AsEnumerable();
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<Cliente> GetById(int id)
        {
            GenericResult<Cliente> result = new GenericResult<Cliente>();

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
                    result.Errors = new string[] { $"Cliente {id} não existe ou não pode ser deletado" };
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }
    }
}
