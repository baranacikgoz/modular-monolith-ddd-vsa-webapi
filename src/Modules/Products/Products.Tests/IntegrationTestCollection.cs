using Xunit;

namespace Products.Tests;

[CollectionDefinition("IntegrationTestCollection")]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>
{
}
