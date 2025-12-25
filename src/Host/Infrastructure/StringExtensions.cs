using OpenTelemetry.Exporter;
using Serilog.Sinks.OpenTelemetry;

namespace Host.Infrastructure;

internal static class StringExtensions
{
    public static OtlpExportProtocol ToOtlpExportProtocol(this string protocol)
    {
        return protocol switch
        {
            "HttpProtobuf" => OtlpExportProtocol.HttpProtobuf,
            "Grpc" => OtlpExportProtocol.Grpc,
            _ => throw new InvalidOperationException($"Unknown OTLP export protocol: {protocol}")
        };
    }

    public static OtlpProtocol ToOtlpProtocol(this string protocol)
    {
        return protocol switch
        {
            "HttpProtobuf" => OtlpProtocol.HttpProtobuf,
            "Grpc" => OtlpProtocol.Grpc,
            _ => throw new InvalidOperationException($"Unknown OTLP protocol: {protocol}")
        };
    }
}
