using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.Auditing;
public static class Setup
{
    public static IServiceCollection AddAuditingInterceptors(this IServiceCollection services)
        => services
            .AddScoped<ApplyAuditingInterceptor>();
}
