using Common.Core.Contracts.Results;

namespace Common.Core.Contracts;

public interface IErrorTranslator
{
    string Translate(Error error);
}
