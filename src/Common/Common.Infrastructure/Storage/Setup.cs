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
            CreateS3Client(sp.GetRequiredService<IOptions<ObjectStorageOptions>>().Value));

        // Not a shared S3ObjectStoreCore: each store builds its own internally, so the public and private buckets
        // get independent circuit breakers over the one shared IAmazonS3 client (see each store's constructor doc).
        services.AddSingleton<IPublicObjectStore, S3PublicObjectStore>();
        services.AddSingleton<IPrivateObjectStore, S3PrivateObjectStore>();

        return services;
    }

    internal static AmazonS3Client CreateS3Client(ObjectStorageOptions options)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = options.ServiceUrl,
            ForcePathStyle = options.ForcePathStyle!.Value,
            AuthenticationRegion = options.Region,
            RetryMode = options.RetryMode switch
            {
                ObjectStorageRetryMode.Adaptive => RequestRetryMode.Adaptive,
                _ => RequestRetryMode.Standard
            },
            MaxErrorRetry = options.MaxErrorRetry!.Value,
            Timeout = TimeSpan.FromSeconds(options.AttemptTimeoutSeconds)
        };

        return new ChunkEncodingFreeS3Client(new BasicAWSCredentials(options.AccessKey, options.SecretKey), config);
    }
}
