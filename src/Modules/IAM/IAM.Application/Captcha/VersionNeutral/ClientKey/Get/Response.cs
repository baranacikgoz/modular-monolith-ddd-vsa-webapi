namespace IAM.Application.Captcha.VersionNeutral.ClientKey.Get;

public sealed record class Response
{
    public required string ClientKey { get; init; }
}
