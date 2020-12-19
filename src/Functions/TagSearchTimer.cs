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
using InstaShop.DbContext.Host;
using InstaShop.DbContext.Erros;

namespace InstaShop.Functions.Coletadores.Timer
{
    public static class TagSearchTimer
    {
        [FunctionName("TagSearchTimer")]
        public static void Run([TimerTrigger("0 30 */1 * * *")] TimerInfo myTimer, ILogger log)
        {
            var tags = new List<string>()
            {
                "lojavirtual",
                "modafeminina",
                "lojainfantil",
                "lojavirtual",
                "lojaonline",
                "lojasderoupasfemininas",
                "lojaderoupas"
            };

            var hostDbContext = new HostDbContext();
            var errosDbContext = new ErrosDbContext();

            foreach (var tag in tags)
            {
                try
                {
                    var host = hostDbContext.GetHost();

                    if (string.IsNullOrEmpty(host))
                    {
                        errosDbContext.InserirLogErro("TagSearchTimer", tag, Environment.MachineName, "SEM HOSTS DISPONIVEIS PARA COLETA");
                        continue;
                    }

                    var url = $"{host}/api/coletador/tag/{tag}/1";
                    log.LogInformation($"Executando TagSearchTimer em {DateTime.Now} - Tag: {tag} - URL: {url}");
                    Console.WriteLine($"Executando TagSearchTimer em {DateTime.Now} - Tag: {tag} - URL: {url}");
                    new WebClient().DownloadString(url);
                }
                catch (Exception ex)
                {
                    errosDbContext.InserirLogErro("TagSearchTimer", tag, Environment.MachineName, ex.ToString());
                    log.LogError($"TAG SEARCH TIMER: {tag} ERRO:{ex.ToString()}");
                }
            }
        }
    }
}
