using Admin2.Domain.Models;
using Admin2.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Domain.Repositories
{
    public interface IVendaRepository
    {
        Venda Save(Venda model);
        void SaveItensVenda(List<ItemVenda> itens);
        IEnumerable<Venda> List(VendaFilter filter);
        IEnumerable<Compra> GetSelectJogosVenda();
        bool EstenderLocacão(ItemVenda item);
        bool FinalizarLocacão(int itemId, DateTime fimLocacao);
        IEnumerable<ItemVenda> GetItensByVendaId(int vendaId);
        IEnumerable<ItemVenda> GetItensByContaId(int contaId);
        IEnumerable<ItemVenda> GetLocacoes(bool fimLocacaoes);
        IEnumerable<Conta> GetContasByJogo(string nomeJogo);
        IEnumerable<ItemVenda> GetContasDisponiveis();
        IEnumerable<ItemVenda> GetItensByItemId(int itemId);
        IEnumerable<ItemVenda> GetItensByMes(int ano, int mes);
        Venda GetById(int id);
        bool Delete(int id);
    }
}
