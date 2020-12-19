using System;
using System.Collections.Generic;
using Azure.Storage.Queues;
using InstaShop.DbContext.Base;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InstaShop.DbContext.Erros
{
    public class ErrosDbContext
    {
        private BaseDbContext lojaDbContext { get; }

        public ErrosDbContext()
        {
            this.lojaDbContext = new BaseDbContext();
        }

        public void InserirLogErro(string function, string chave, string maquina, string exception)
        {
            var query = @"insert into TB_LOG_ERROS (dt_erro, DS_CHAVE , ds_function, ds_maquina, ds_exception ) 
                        values (getdate(), @chave , @function, @maquina, @exception) ";
            var parametros = new { function = function, chave = chave, maquina = maquina, exception = exception };
            this.lojaDbContext.Executar(query, parametros);
        }
    }
}