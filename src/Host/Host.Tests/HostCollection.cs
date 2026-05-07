using Common.Tests;
using Xunit;

namespace Host.Tests;

[CollectionDefinition("Host")]
public class HostCollection : ICollectionFixture<HostTestFactory>;
