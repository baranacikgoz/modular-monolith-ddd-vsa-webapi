using Amazon.Runtime;
using Amazon.S3;
using Common.Application.Options;
using Common.Application.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Storage;

public static class Setup
{
    /// <summary>
    ///     Registers a single S3-compatible client plus the public/private object store abstractions. The client
    ///     is provider-agnostic (endpoint, credentials and path-style all come from <see cref="ObjectStorageOptions" />
    ///     at runtime), so swapping the storage engine behind it needs no code change here.
    ///     Nothing eagerly connects: registration only builds the client lazily on first resolution, so this is
    ///     safe to register unconditionally even before any module consumes it.
    /// </summary>
    public static IServiceCollection AddCommonObjectStorage(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ObjectStorageOptions>>().Value;

            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceUrl,
                ForcePathStyle = options.ForcePathStyle,
                AuthenticationRegion = options.Region,
                RetryMode = RequestRetryMode.Standard,
                MaxErrorRetry = options.MaxErrorRetry,
                Timeout = TimeSpan.FromSeconds(options.AttemptTimeoutSeconds)
            };

            var credentials = new BasicAWSCredentials(options.AccessKey, options.SecretKey);
            return new AmazonS3Client(credentials, config);
        });

        services.AddSingleton(sp => new S3ObjectStoreCore(
            sp.GetRequiredService<IAmazonS3>(),
            sp.GetRequiredService<IOptions<ObjectStorageOptions>>().Value));

        services.AddSingleton<IPublicObjectStore, S3PublicObjectStore>();
        services.AddSingleton<IPrivateObjectStore, S3PrivateObjectStore>();

        return services;
    }
}
