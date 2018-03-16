using Admin2.Domain.Repositories;
using System.Collections.Generic;
using Admin2.Domain.Filters;
using Admin2.Domain.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Linq;
using System.Data;
using System;

namespace Admin2.Data.Repositories
{
    internal class ContaRepository : RepositoryBase, IContaRepository
    {
        public ContaRepository(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public Conta Save(Conta model)
        {
            string sql = "Exec saveConta @id, @email, @senha, @idOnline, @dataNascimento, @tipoContaId, @ativa, @status, @dataDesativacao";

            var parameters = new DynamicParameters(model);
            parameters.Add("@tipoContaId", model.TipoConta.Id);

            model.Id = connection.ExecuteScalar<int>(sql, parameters);
            return model;
        }

        public void SaveJogosConta(List<JogosConta> jogosConta)
        {
            var exists = connection.Query<JogosConta>("select * from jogosConta where contaId = @contaId and jogoId = @jogoId", jogosConta.FirstOrDefault());

            if (exists.Count() <= 0)
            {
                string sql = "Exec SaveJogosConta @id, @contaId, @jogoId";
                connection.Execute(sql, jogosConta);
            }
        }

        public void SaveCodigosConta(List<CodigosConta> codigos)
        {
            string sql = "Exec saveCodigosConta @id, @descricao, @contaId, @status";
            connection.Execute(sql, codigos);
        }

        public IEnumerable<Conta> List(ContaFilter filter)
        {
            var result = connection.Query<Conta, TipoConta, Conta>("Exec GetListConta @text",
                    (conta, tipConta) =>
                    {
                        conta.TipoConta = tipConta;
                        return conta;
                    },
                    filter);

            foreach (var conta in result)
            {
                conta.Jogos = GetJogosByContaId(conta.Id).ToList();
                conta.Codigos = GetCodigosByContId(conta.Id);
            }

            return result;
        }

        public IEnumerable<Compra> GetSelectJogosConta()
        {
            var result = connection.Query<Compra>("Exec GetSelectJogosConta");
            return result;
        }

        public Conta GetById(int id)
        {
            var result = connection.Query<Conta, TipoConta, Conta>("Exec getConta @id",
                   (conta, tipoConta) =>
                   {
                       conta.TipoConta = tipoConta;
                       return conta;
                   },
                   new { id = id }).FirstOrDefault();

            result.Jogos = GetJogosByContaId(result.Id).ToList();

            return result;
        }

        public IEnumerable<Conta> GetByJogo(string nomeJogo)
        {
            var result = connection.Query<Conta>("Exec GetContasByNomeJogo @nomeJogo", new { nomeJogo = nomeJogo });
            return result;
        }

        public IEnumerable<Conta> GetDesativar()
        {
            var result = connection.Query<Conta, TipoConta, Conta>("Exec GetContasDesativar",
                    (conta, tipConta) =>
                    {
                        conta.TipoConta = tipConta;
                        return conta;
                    });

            foreach (var conta in result)
                conta.Jogos = GetJogosByContaId(conta.Id).ToList();

            return result;
        }

        public List<Compra> GetJogosByContaId(int contaId)
        {
            var result = connection.Query<Compra>("Exec GetListJogosConta @contaId", new { contaId = contaId }).ToList();
            return result;
        }

        public List<CodigosConta> GetCodigosByContId(int id)
        {
            var result = connection.Query<CodigosConta>("Exec GetListCodigosConta @contaId", new { contaId = id }).ToList();
            return result;
        }


        public bool Delete(int id)
        {
            var delete = connection.Execute("Exec deleteConta @id", new { id = id });
            return delete > 0;
        }

        public bool AddJogo(JogosConta jc)
        {
            var result = connection.Execute("Exec SaveJogosConta @contaId, @jogoId", jc);
            return result > 0;
        }

        public bool RemoverJogo(JogosConta jc)
        {
            var sql = "select id from jogosConta where contaId = @contaId and jogoId = @jogoId";
            var jogosContaId = connection.Query<JogosConta>(sql, jc).OrderByDescending(x => x.Id).FirstOrDefault();

            var delete = connection.Execute("Exec DeleteJogosConta @id", new { id = jogosContaId.Id });

            return delete > 0;
        }

        public bool DeleteCodigos(int contaId)
        {
            var delete = connection.Execute("Exec DeleteCodigosConta @contaId", new { contaId = contaId });
            return delete > 0;
        }
    }
}
