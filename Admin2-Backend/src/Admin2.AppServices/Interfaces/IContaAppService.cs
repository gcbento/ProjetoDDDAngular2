using Admin2.AppServices.Results;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.AppServices.Interfaces
{
    public interface IContaAppService
    {
        GenericResult<Conta> Save(Conta model);
        GenericResult<IEnumerable<Conta>> List(ContaFilter filter);
        GenericResult<IEnumerable<SelectModel>> GetSelectJogosConta();
        GenericResult<IEnumerable<Conta>> GetDesativar();
        GenericResult<Conta> GetById(int id);
        GenericResult<bool> Delete(int id);
        GenericResult<bool> AddJogo(JogosConta jc);
        GenericResult<bool> RemoverJogo(JogosConta jc);
        GenericResult<bool> SaveCodigosConta(List<CodigosConta> codigos);
        GenericResult<bool> UpdateCodigosConta(CodigosConta codigo);
        GenericResult<bool> DeleteCodigos(int contaId);
    }
}
