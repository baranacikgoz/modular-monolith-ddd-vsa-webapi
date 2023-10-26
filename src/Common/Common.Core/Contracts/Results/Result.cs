using System.Net;

namespace Common.Core.Contracts.Results;

public abstract record Failure(HttpStatusCode StatusCode = HttpStatusCode.BadRequest, IEnumerable<string>? Errors = null);

public readonly struct Result : IEquatable<Result>
{
    private static readonly Result _success = new(null);
    public bool IsSucceeded => Failure is null;
    public Failure? Failure { get; }

    private Result(Failure? failure)
    {
        Failure = failure;
    }

    public static Result Succeeded() => _success;
    public static Result Failed(Failure failure) => new(failure);

#pragma warning disable CA2225
    public static implicit operator Result(Failure failure) => Failed(failure);
#pragma warning restore CA2225

    public override int GetHashCode() => HashCode.Combine(IsSucceeded, Failure);

    public bool Equals(Result other)
    {
        return IsSucceeded == other.IsSucceeded && EqualityComparer<Failure>.Default.Equals(Failure, other.Failure);
    }

    public static bool operator ==(Result left, Result right) => left.Equals(right);

    public static bool operator !=(Result left, Result right) => !left.Equals(right);

    public override bool Equals(object? obj) => obj is Result result && Equals(result);

}

public readonly struct Result<T> : IEquatable<Result<T>>
{
    public T? Value { get; }
    public Failure? Failure { get; }
    public bool IsSucceeded => Failure is null;

    private Result(Failure failure) : this(default, failure) { }

    private Result(T value) : this(value, null) { }

    private Result(T? value, Failure? failure)
    {
        Value = value;
        Failure = failure;
    }

#pragma warning disable CA1000
    public static Result<T> Succeeded(T value) => new(value);
    public static Result<T> Failed(Failure failure) => new(failure);
#pragma warning restore CA1000

#pragma warning disable CA2225
    public static implicit operator Result<T>(T value) => Succeeded(value);
    public static implicit operator Result<T>(Failure error) => Failed(error);
#pragma warning restore CA2225

    public override bool Equals(object? obj) => obj is Result<T> result && Equals(result);

    public override int GetHashCode() => HashCode.Combine(Value, Failure);

    public bool Equals(Result<T> other) => EqualityComparer<T>.Default.Equals(Value, other.Value) && EqualityComparer<Failure>.Default.Equals(Failure, other.Failure);

    public static bool operator ==(Result<T> left, Result<T> right) => left.Equals(right);

    public static bool operator !=(Result<T> left, Result<T> right) => !left.Equals(right);
}
