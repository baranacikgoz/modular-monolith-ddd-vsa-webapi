using Common.Infrastructure.Security;
using Common.InterModuleRequests.IAM;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Tests.InterModuleRequestHandlers;

/// <summary>Builds a <see cref="ProvisionUserRequest" /> the way a caller does: the password encrypted with the host's <see cref="SecretProtector" />.</summary>
internal static class ProvisionUserTestRequests
{
    internal static ProvisionUserRequest Create(
        IServiceProvider services, string email, string phone, string password, string firstName = "Ada", string lastName = "Lovelace")
    {
        return new ProvisionUserRequest(
            email, phone, services.GetRequiredService<SecretProtector>().Encrypt(password), firstName, lastName);
    }
}
