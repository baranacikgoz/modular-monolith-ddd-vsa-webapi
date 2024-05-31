using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.ResultMonad;
using FluentAssertions;

namespace UnitTests.Common;
public static class ErrorExtensions
{
    public static void ShouldBeSameValueError(this Error error, string parameterName, object value)
    {
        error.Should().NotBeNull();
        error.Key.Should().Be(nameof(Error.SameValue));
        error.ParameterName.Should().Be(parameterName);
        error.Value.Should().Be(value);
    }
}
