using Microsoft.AspNetCore.Http;

namespace Common.Core.Interfaces;

public interface IProblemDetailsFactory
{
    IResult Create(
        int status,
        string title,
        string type,
        string instance,
        string requestId,
        IEnumerable<string> errors);
}
