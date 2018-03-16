using Admin2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;

namespace Admin2.Data.Repositories
{
    internal class VendaRepository : RepositoryBase, IVendaRepository
    {
        private readonly ContaRepository contaRep;

        public VendaRepository(IConfigurationRoot configuration) : base(configuration)
        {
            contaRep = new ContaRepository(configuration);
        }

        public Venda Save(Venda model)
        {
            string sql = "Exec saveVenda @id, @valor, @clienteId, @dataVenda";

            var parameters = new DynamicParameters(model);
            parameters.Add("@clienteId", model.Cliente.Id);

            model.Id = connection.ExecuteScalar<int>(sql, parameters);

            return model;
        }

        public void SaveItensVenda(List<ItemVenda> itens)
        {
            string sql = "Exec SaveItensVenda @id, @vendaId, @contaId, @tipoContaId, @tipoVendaId, @valor, @qtdeDias, @inicioLocacao,  @fimLocacao, @senha";
            var parameters = new DynamicParameters();

            foreach (var item in itens)
            {
                parameters = new DynamicParameters(item);
                parameters.Add("@vendaId", item.Venda.Id);
                parameters.Add("@contaId", item.Conta.Id);
                parameters.Add("@tipoContaId", item.TipoConta.Id);
                parameters.Add("@tipoVendaId", item.TipoVenda.Id);

                connection.ExecuteScalar<int>(sql, parameters);
            }
        }

        public IEnumerable<Venda> List(VendaFilter filter)
        {
            var result = connection.Query<Venda, Cliente, Venda>("Exec GetListVenda @locacoes, @text",
                 (venda, cliente) =>
                 {
                     venda.Cliente = cliente;
                     return venda;

                 }, filter);

            foreach (var venda in result)
                venda.ItensVenda = GetItensByVendaId(venda.Id).ToList();

            return result;
        }
        private IEnumerable<ItemVenda> GetItensVenda(int vendaId, int contaId = 0, int itemId = 0, bool locacoes = false, int ano = 0, int mes = 0)
        {
            string sql = "Exec GetListItensVenda @vendaId, @contaId, @itemId, @locacoes, @ano, @mes";
            var result = connection.Query<ItemVenda, Venda, Cliente, Conta, TipoVenda, TipoConta, ItemVenda>(sql,
                  (itemVenda, Venda, cliente, conta, tipoVenda, tipoConta) =>
                  {
                      itemVenda.Venda = Venda;
                      itemVenda.Venda.Cliente = cliente;
                      itemVenda.Conta = conta;
                      itemVenda.TipoVenda = tipoVenda;
                      itemVenda.TipoConta = tipoConta;

                      return itemVenda;
                  },
                  new { vendaId = vendaId, contaId = contaId, itemId = itemId, locacoes = locacoes, mes = mes, ano = ano });

            foreach (var item in result)
                item.Conta = contaRep.GetById(item.Conta.Id);

            return result;
        }

        public IEnumerable<ItemVenda> GetItensByVendaId(int vendaId)
        {
            return GetItensVenda(vendaId);
        }

        public IEnumerable<ItemVenda> GetItensByContaId(int contaId)
        {
            return GetItensVenda(0, contaId);
        }

        public IEnumerable<ItemVenda> GetItensByItemId(int itemId)
        {
            return GetItensVenda(0, 0, itemId);
        }

        public IEnumerable<ItemVenda> GetLocacoes(bool fimLocacoes)
        {
            var locacoes = GetItensVenda(0, 0, 0, true);

            if (fimLocacoes)
            {
                var dateHoje = DateTime.Now.Date;
                var dateAmanha = DateTime.Now.AddDays(1).Date;

                var fimLocs = new List<ItemVenda>();
                foreach (var item in locacoes)
                {
                    if (item.FimLocacao.Date == dateHoje || item.FimLocacao.Date == dateAmanha)
                        fimLocs.Add(item);
                }

                return fimLocs.AsEnumerable();
            }

            return locacoes;
        }

        public IEnumerable<ItemVenda> GetItensByMes(int ano, int mes)
        {
            return GetItensVenda(0, 0, 0, false, ano, mes);
        }

        public bool EstenderLocacão(ItemVenda item)
        {
            var result = connection.Execute("Exec estenderLocacao @id, @qtdeDias, @valor, @fimLocacao", item);
            return result > 0;
        }

        public bool FinalizarLocacão(int itemId, DateTime fimLocacao)
        {
            var result = connection.Execute("Exec finalizarLocacao @id, @fimLocacao", new { id = itemId, fimLocacao = fimLocacao });
            return result > 0;
        }

        public IEnumerable<Compra> GetSelectJogosVenda()
        {
            var result = connection.Query<Compra>("Exec GetSelectJogosVenda");
            return result;
        }

        public IEnumerable<Conta> GetContasByJogo(string nomeJogo)
        {
            return contaRep.GetByJogo(nomeJogo);
        }

        public IEnumerable<ItemVenda> GetContasDisponiveis()
        {
            var result = connection.Query<ItemVenda, Conta, TipoConta, ItemVenda>("Exec GetListContaDisponiveis",
                    (itemVenda, conta, tipConta) =>
                    {
                        itemVenda.Conta = conta;
                        itemVenda.Conta.TipoConta = tipConta;
                        return itemVenda;
                    });

            foreach (var item in result)
                item.Conta.Jogos = contaRep.GetJogosByContaId(item.Conta.Id);

            return result;
        }

        public Venda GetById(int id)
        {
            var result = connection.QueryFirstOrDefault<Venda>("Exec get @id", id);
            return result;
        }

        public bool Delete(int id)
        {
            var delete = connection.Execute("Exec deleteVenda @vendaId", new { vendaId = id });
            return delete > 0;
        }
    }
}
