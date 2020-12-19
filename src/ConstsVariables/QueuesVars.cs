using System;
using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InstaShop.ConstsVariables
{
    public static class QueuesVars
    {
        public const string FILA_POSTAGENS = "fila-insta-postagens";
        public const string FILA_PERFIS = "fila-insta-perfis";
    }
}