﻿using System.Threading.Tasks;
using AspNetStandard.Diagnostics.HealthChecks;

namespace WebApi.HealthChecks.Test.Implementations
{
    public class HealthyCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync()
        {
            return await Task.FromResult(new HealthCheckResult(HealthStatus.Healthy));
        }
    }
}