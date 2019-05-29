﻿using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.HealthChecks.Models;

namespace WebApi.HealthChecks.HttpMessageHandlers
{
    public class HealthHandler : HttpMessageHandler
    {
        private readonly HealthChecksBuilder _healthChecksBuilder;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public HealthHandler(HealthChecksBuilder healthChecksBuilder)
        {
            _healthChecksBuilder = healthChecksBuilder;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await _healthChecksBuilder.GetHealthAsync();

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new ObjectContent<HealthCheckResults>(result,
                    new JsonMediaTypeFormatter {SerializerSettings = _serializerSettings}),
                StatusCode = result.Status == HealthStatus.Healthy
                    ? HttpStatusCode.OK
                    : HttpStatusCode.ServiceUnavailable
            };

            return httpResponseMessage;
        }
    }
}