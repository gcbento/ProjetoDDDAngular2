using Admin2.AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Admin2.Domain.Repositories;

namespace Admin2.AppServices.AppServices
{
    internal class ContaAppService : IContaAppService
    {
        private readonly IContaRepository service;


        public ContaAppService(IContaRepository service)
        {
            this.service = service;
        }

        public GenericResult<Conta> Save(Conta conta)
        {
            GenericResult<Conta> result = new GenericResult<Conta>();

            try
            {
                result.Result = service.Save(conta);

                if (result.Result.Id > 0)
                {
                    List<JogosConta> listJogosConta = new List<JogosConta>();

                    conta.Jogos.ForEach(jo =>
                    {
                        JogosConta jc = new JogosConta();
                        jc.ContaId = result.Result.Id;
                        jc.JogoId = jo.Id;

                        listJogosConta.Add(jc);
                    });

                    service.SaveJogosConta(listJogosConta);

                    conta.Codigos.ForEach(co => { co.ContaId = result.Result.Id; co.Status = true; });
                    service.SaveCodigosConta(conta.Codigos);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }



        public GenericResult<IEnumerable<Conta>> List(ContaFilter filter)
        {
            GenericResult<IEnumerable<Conta>> result = new GenericResult<IEnumerable<Conta>>();
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

        public GenericResult<IEnumerable<Conta>> GetDesativar()
        {
            GenericResult<IEnumerable<Conta>> result = new GenericResult<IEnumerable<Conta>>();
            try
            {
                result.Result = service.GetDesativar();
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<SelectModel>> GetSelectJogosConta()
        {
            GenericResult<IEnumerable<SelectModel>> result = new GenericResult<IEnumerable<SelectModel>>();
            try
            {
                List<SelectModel> select = new List<SelectModel>();
                var compras = service.GetSelectJogosConta();


                foreach (var compra in compras)
                {
                    SelectModel model = new SelectModel();
                    model.Value = compra.Id.ToString();
                    model.Label = compra.Descricao;

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

        public GenericResult<Conta> GetById(int id)
        {
            GenericResult<Conta> result = new GenericResult<Conta>();

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
                    result.Errors = new string[] { $"Conta {id} não existe ou não pode ser deletado" };
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> AddJogo(JogosConta jc)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                result.Result = service.AddJogo(jc);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> RemoverJogo(JogosConta jc)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                result.Result = service.RemoverJogo(jc);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> SaveCodigosConta(List<CodigosConta> codigos)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                service.SaveCodigosConta(codigos);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> UpdateCodigosConta(CodigosConta codigo)
        {
            GenericResult<bool> result = new GenericResult<bool>();
            var listCodigos = new List<CodigosConta>();

            try
            {
                listCodigos.Add(codigo);
                service.SaveCodigosConta(listCodigos);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> DeleteCodigos(int contaId)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                service.DeleteCodigos(contaId);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

    }
}
