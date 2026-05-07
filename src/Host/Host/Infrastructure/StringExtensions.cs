using OpenTelemetry.Exporter;

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
}
