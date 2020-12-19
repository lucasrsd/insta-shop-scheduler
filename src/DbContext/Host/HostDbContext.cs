using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Storage.Queues;
using InstaShop.DbContext.Base;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InstaShop.DbContext.Host
{
    public class HostDbContext
    {
        private BaseDbContext lojaDbContext { get; }

        public HostDbContext()
        {
            this.lojaDbContext = new BaseDbContext();
        }
        public string GetHost()
        {
            var hosts = ObterHostsDisponiveis().ToList();
            if (hosts == null) return null;
            if (!hosts.Any()) return null;

            int r = new Random().Next(hosts.Count);
            return hosts[r];
        }

        public IEnumerable<string> ObterHostsDisponiveis()
        {
            var query = @" select ds_host from TB_CRAWLER_HOSTS where DT_INATIVACAO is  null  ";
            return this.lojaDbContext.ObterListagem<string>(query);
        }
    }
}