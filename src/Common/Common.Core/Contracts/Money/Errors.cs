using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts.Results;

namespace Common.Core.Contracts.Money;
public static class Errors
{
    public static readonly Error CurrenciesAreNotTheSame = new() { Key = nameof(CurrenciesAreNotTheSame) };
    public static readonly Error AmountsAreTheSame = new() { Key = nameof(AmountsAreTheSame) };
}
