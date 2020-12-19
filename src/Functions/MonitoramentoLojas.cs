using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using InstaShop.DbContext.Monitoramento;
using InstaShop.Sender.Perfil;

namespace InstaShop.Functions.Coletadores.Timer
{
    public static class MonitoramentoLojas
    {
        [FunctionName("MonitoramentoLojas")]
        public static void Run([TimerTrigger("0 */58 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var lojasMonitoramento = new MonitoramentoDbContext();
            var perfilSender = new PerfilSender();
            var lojas = lojasMonitoramento.ObterLojasMonitoramento();

            foreach (var loja in lojas)
            {
                try
                {
                    perfilSender.PostMessage(loja);
                }
                catch (Exception ex)
                {
                    log.LogError($"MONITORAMENTO LOJAS: {loja} ERRO:{ex.ToString()}");
                }
            }
        }
    }
}
