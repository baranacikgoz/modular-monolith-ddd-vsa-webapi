namespace Common.Domain.ResultMonad;

public interface IResult
{
    Error? Error { get; set; }
}
