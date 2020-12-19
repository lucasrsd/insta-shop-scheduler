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
using System.Security.Authentication;
using InstaShop.DbContext.Base;

namespace InstaShop.DbContext.Monitoramento
{
    public class MonitoramentoDbContext
    {
        private BaseDbContext lojaDbContext { get; }

        public MonitoramentoDbContext()
        {
            this.lojaDbContext = new BaseDbContext();
        }
        public IEnumerable<string> ObterLojasMonitoramento()
        {
            var query = @" select ds_username  FROM TB_MONITORAMENTO_LOJAS ";
            return this.lojaDbContext.ObterListagem<string>(query);
        }
    }
}
