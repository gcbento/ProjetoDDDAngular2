using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Interfaces
{
    public interface IVendaAppService
    {
        GenericResult<Venda> Save(Venda model);
        GenericResult<IEnumerable<Venda>> List(VendaFilter filter);
        GenericResult<IEnumerable<ItemVenda>> GetItensVendaByContaId(int contaId);
        GenericResult<IEnumerable<ItemVenda>> GetLocacoes(bool fimLocacoes);
        GenericResult<bool> EstenderLocacão(ItemVenda item);
        GenericResult<bool> FinalizarLocacão(int itemId);
        GenericResult<IEnumerable<SelectModel>> GetSelectJogosVenda();
        GenericResult<IEnumerable<Conta>> GetContasByJogo(string nomeJogo);
        GenericResult<IEnumerable<ItemVenda>> GetContasDisponiveis();
        GenericResult<List<IEnumerable<ItemVenda>>> GetItensByMes(int ano, int mes);
        GenericResult<Venda> GetById(int id);
        GenericResult<bool> Delete(int id);
    }
}
