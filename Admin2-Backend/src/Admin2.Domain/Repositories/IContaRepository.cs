using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System.Collections.Generic;

namespace Admin2.Domain.Repositories
{
    public interface IContaRepository
    {
        Conta Save(Conta model);
        void SaveJogosConta(List<JogosConta> jogosConta);
        void SaveCodigosConta(List<CodigosConta> codigos);
        IEnumerable<Conta> List(ContaFilter filter);
        IEnumerable<Compra> GetSelectJogosConta();
        IEnumerable<Conta> GetByJogo(string nomeJogo);
        IEnumerable<Conta> GetDesativar();
        Conta GetById(int id);
        bool Delete(int id);
        bool AddJogo(JogosConta jc);
        bool RemoverJogo(JogosConta jc);
        bool DeleteCodigos(int contaId);
    }
}
