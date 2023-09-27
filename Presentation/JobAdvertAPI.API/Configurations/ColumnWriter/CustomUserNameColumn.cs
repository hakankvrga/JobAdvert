using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Linq;

namespace JobAdvertAPI.API.Configurations.ColumnWriter
{
    public class CustomUserNameColumn : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var userNameProperty = logEvent.Properties.FirstOrDefault(p => p.Key == "UserName");
            if (userNameProperty.Value is ScalarValue scalarValue)
            {
                var userName = scalarValue.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("UserName", new ScalarValue(userName)));
                }
            }
        }
    }
}
