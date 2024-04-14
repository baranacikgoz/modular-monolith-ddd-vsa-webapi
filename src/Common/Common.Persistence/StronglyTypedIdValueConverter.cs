using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Persistence;
public class StronglyTypedIdValueConverter<TStronglyTypedId> : ValueConverter<TStronglyTypedId, Guid>
    where TStronglyTypedId : IStronglyTypedId, new()
{
    public StronglyTypedIdValueConverter()
        : base(
            id => id.Value,
            value => new TStronglyTypedId() { Value = value })
    {

    }
}
