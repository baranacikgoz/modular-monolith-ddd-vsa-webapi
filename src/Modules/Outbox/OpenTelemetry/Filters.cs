using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outbox.OpenTelemetry;
public static class Tracing
{
    public static class Filters
    {
        public static IEnumerable<Func<string?, IDbCommand, bool>> EfCoreInstrumentationFilters()
        {
            // This periodic outbox background processor pollutes efcore traces
            yield return (providerName, dbCommand) => !dbCommand.CommandText.Contains("FROM \"Outbox\".", StringComparison.OrdinalIgnoreCase);
        }
    }
}
