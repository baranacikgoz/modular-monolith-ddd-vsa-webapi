using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;

namespace Common.Infrastructure.Storage;

/// <summary>
///     Over plain HTTP the AWS SDK sends every upload (<c>PutObject</c>, <c>UploadPart</c>) as
///     <c>Content-Encoding: aws-chunked</c> with signed chunk headers. Self-hosted S3 engines (SeaweedFS 3.x) store
///     such a body as received, framing included, so the object is unreadable and the framing header is served back
///     to browsers. <c>UseChunkEncoding</c> is the SDK's own switch, but <c>TransferUtility</c> builds its requests
///     internally and a <c>BeforeRequestEvent</c> fires only after the request is marshalled, so the flag is cleared
///     by a pipeline handler placed in front of the marshaller, where every upload of this client passes. Over HTTPS
///     the SDK does not chunk and nothing changes.
/// </summary>
internal sealed class ChunkEncodingFreeS3Client(AWSCredentials credentials, AmazonS3Config config)
    : AmazonS3Client(credentials, config)
{
    protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline)
    {
        base.CustomizeRuntimePipeline(pipeline);
        pipeline.AddHandlerBefore<Marshaller>(new DisableChunkEncodingHandler());
    }

    private sealed class DisableChunkEncodingHandler : PipelineHandler
    {
        public override void InvokeSync(IExecutionContext executionContext)
        {
            Disable(executionContext.RequestContext.OriginalRequest);
            base.InvokeSync(executionContext);
        }

        public override Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            Disable(executionContext.RequestContext.OriginalRequest);
            return base.InvokeAsync<T>(executionContext);
        }

        private static void Disable(AmazonWebServiceRequest request)
        {
            switch (request)
            {
                case PutObjectRequest put:
                    put.UseChunkEncoding = false;
                    break;
                case UploadPartRequest part:
                    part.UseChunkEncoding = false;
                    break;
            }
        }
    }
}
