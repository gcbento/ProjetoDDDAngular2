using Admin2.AppServices.Interfaces;
using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Admin2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.AppServices
{
    internal class VendaAppService : IVendaAppService
    {
        private readonly IVendaRepository service;
        private readonly IClienteRepository serviceCliente;

        public VendaAppService(IVendaRepository service, IClienteRepository serviceCliente)
        {
            this.service = service;
            this.serviceCliente = serviceCliente;
        }

        public GenericResult<Venda> Save(Venda venda)
        {
            GenericResult<Venda> result = new GenericResult<Venda>();
            Cliente cliente = new Cliente();

            try
            {
                if (venda.Cliente.Id <= 0)
                {
                    cliente = serviceCliente.Save(venda.Cliente);
                    venda.Cliente.Id = cliente.Id;
                }

                venda.Valor = venda.ItensVenda.Sum(x => x.Valor);
                result.Result = service.Save(venda);

                if (result.Result.Id > 0)
                {
                    venda.ItensVenda.ForEach(item =>
                    {

                        item.Venda.Id = result.Result.Id;

                        if (item.TipoVenda.Id == 1)
                        {
                            item.InicioLocacao = DateTime.Parse("1900-01-01");
                            item.FimLocacao = DateTime.Parse("1900-01-01");
                        }

                        else
                        {
                            if (item.TipoConta.Id == 3)
                            {
                                if (item.DiasGratis)
                                    item.InicioLocacao = result.Result.DataVenda.AddDays(2);
                                else
                                    item.InicioLocacao = result.Result.DataVenda;

                                item.FimLocacao = item.InicioLocacao.AddDays(item.qtdeDias);
                            }

                            else if (item.TipoConta.Id == 2)
                            {
                                DateTime dataFim = DateTime.MinValue;
                                item.InicioLocacao = result.Result.DataVenda;

                                switch (item.TipoPeriodo.Trim().ToLower())
                                {
                                    case "dias":
                                        dataFim = item.InicioLocacao.AddDays(item.qtdeDias);
                                        break;
                                    case "mes":
                                        dataFim = item.InicioLocacao.AddMonths(item.qtdeDias);
                                        break;
                                }

                                item.FimLocacao = dataFim;
                            }
                        }
                    });

                    service.SaveItensVenda(venda.ItensVenda);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<Venda>> List(VendaFilter filter)
        {
            GenericResult<IEnumerable<Venda>> result = new GenericResult<IEnumerable<Venda>>();

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

        public GenericResult<IEnumerable<ItemVenda>> GetItensVendaByContaId(int contaId)
        {
            GenericResult<IEnumerable<ItemVenda>> result = new GenericResult<IEnumerable<ItemVenda>>();

            try
            {
                result.Result = service.GetItensByContaId(contaId);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<ItemVenda>> GetLocacoes(bool fimLocacoes)
        {
            GenericResult<IEnumerable<ItemVenda>> result = new GenericResult<IEnumerable<ItemVenda>>();

            try
            {
                result.Result = service.GetLocacoes(fimLocacoes);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<List<IEnumerable<ItemVenda>>> GetItensByMes(int ano, int mes)
        {
            GenericResult<List<IEnumerable<ItemVenda>>> result = new GenericResult<List<IEnumerable<ItemVenda>>>();
            var listVendasMensal = new List<IEnumerable<ItemVenda>>();

            try
            {
                var vendas = service.GetItensByMes(ano, mes);
                var jogos = vendas.Select(x => new { x.Conta.Id }).GroupBy(x => x.Id);
                foreach (var item in jogos)
                {
                    var contaId = int.Parse(item.Key.ToString());
                    var vendasConta = vendas.Where(x => x.Conta.Id == contaId);

                    listVendasMensal.Add(vendasConta);
                }

                result.Result = listVendasMensal;

            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> EstenderLocacão(ItemVenda model)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                if (model.Id > 0)
                {
                    var item = service.GetItensByItemId(model.Id).FirstOrDefault();

                    item.Valor = item.Valor + model.Valor;
                    item.FimLocacao = item.FimLocacao.AddDays(model.qtdeDias);
                    item.qtdeDias = item.qtdeDias + model.qtdeDias;

                    result.Result = service.EstenderLocacão(item);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<bool> FinalizarLocacão(int itemId)
        {
            GenericResult<bool> result = new GenericResult<bool>();

            try
            {
                if (itemId > 0)
                {
                    var fimLocacao = DateTime.Now.AddDays(-1);
                    result.Result = service.FinalizarLocacão(itemId, fimLocacao);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<SelectModel>> GetSelectJogosVenda()
        {
            GenericResult<IEnumerable<SelectModel>> result = new GenericResult<IEnumerable<SelectModel>>();

            try
            {
                List<SelectModel> select = new List<SelectModel>();
                var jogos = service.GetSelectJogosVenda();

                foreach (var jogo in jogos)
                {
                    SelectModel model = new SelectModel();
                    model.Value = jogo.Descricao;
                    model.Label = jogo.Descricao;

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

        public GenericResult<Venda> GetById(int id)
        {
            GenericResult<Venda> result = new GenericResult<Venda>();

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

        public GenericResult<IEnumerable<Conta>> GetContasByJogo(string nomeJogo)
        {
            GenericResult<IEnumerable<Conta>> result = new GenericResult<IEnumerable<Conta>>();

            try
            {
                result.Result = service.GetContasByJogo(nomeJogo);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<ItemVenda>> GetContasDisponiveis()
        {
            GenericResult<IEnumerable<ItemVenda>> result = new GenericResult<IEnumerable<ItemVenda>>();

            try
            {
                result.Result = service.GetContasDisponiveis();
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
                    result.Errors = new string[] { $"Venda {id} não existe ou não pode ser deletado" };
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }
    }
}
