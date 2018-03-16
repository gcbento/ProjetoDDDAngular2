using Admin2.AppServices.Interfaces;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Admin2.Domain.Models;
using Admin2.Domain.Filters;
using Admin2.Domain.Repositories;
using Admin2.AppServices.Results;

namespace Admin2.AppServices.AppServices
{
    public class SaldoAppService : ISaldoAppService
    {
        private readonly ISaldoRepository service;


        public SaldoAppService(ISaldoRepository service)
        {
            this.service = service;
        }


        public GenericResult<Estatisticas> GetEstatisticas(SaldoFilter filter)
        {
            GenericResult<Estatisticas> result = new GenericResult<Estatisticas>();

            try
            {
                result.Result = service.GetEstatisticas(filter);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<SaldoConta>> ListSaldoContas(SaldoFilter filter)
        {
            GenericResult<IEnumerable<SaldoConta>> result = new GenericResult<IEnumerable<SaldoConta>>();

            try
            {
                result.Result = service.ListSaldoContas(filter);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<Estatisticas>> GetSaldoMes()
        {
            GenericResult<IEnumerable<Estatisticas>> result = new GenericResult<IEnumerable<Estatisticas>>();

            try
            {
                result.Result = service.GetSaldoMes();
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }

        public GenericResult<IEnumerable<Estatisticas>> GetSaldoMensal(int ano)
        {
            GenericResult<IEnumerable<Estatisticas>> result = new GenericResult<IEnumerable<Estatisticas>>();

            try
            {
                var list = service.GetSaldoMensal(ano);

                list.ToList().ForEach(x =>
                {
                    var dateFormat = new DateTimeFormatInfo();
                    x.Mes = dateFormat.GetMonthName(x.NumeroMes);
                });

                result.Result = list;
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
            }

            return result;
        }
    }
}
