namespace Host.Infrastructure;

internal static class StringExtensions
{
    public static OpenTelemetry.Exporter.OtlpExportProtocol ToOtlpExportProtocol(this string protocol)
        => protocol switch
        {
            "HttpProtobuf" => OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf,
            "Grpc" => OpenTelemetry.Exporter.OtlpExportProtocol.Grpc,
            _ => throw new InvalidOperationException($"Unknown OTLP export protocol: {protocol}")
        };

    public static Serilog.Sinks.OpenTelemetry.OtlpProtocol ToOtlpProtocol(this string protocol)
        => protocol switch
        {
            "HttpProtobuf" => Serilog.Sinks.OpenTelemetry.OtlpProtocol.HttpProtobuf,
            "Grpc" => Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc,
            _ => throw new InvalidOperationException($"Unknown OTLP protocol: {protocol}")
        };
}
