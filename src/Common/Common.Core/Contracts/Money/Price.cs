using Common.Core.Contracts.Results;

namespace Common.Core.Contracts.Money;

public enum Currency
{
    TRY,
    USD,
    EUR
}

public class Price : ValueObject
{
    private Price(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Currency Currency { get; }
    public decimal Amount { get; }

    public static Price Create(decimal amount, Currency currency = Currency.TRY) => new(amount, currency);
    public Result EnsureSameCurrency(Price other) => Currency == other.Currency ? Result.Success : Errors.CurrenciesAreNotTheSame;
    public Result EnsureAmountsAreNotTheSame(Price other) => Amount == other.Amount ? Errors.AmountsAreTheSame : Result.Success;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override int CompareTo(ValueObject? other)
    {
        if (other is not Price otherMoney)
        {
            throw new ArgumentException("Object is not a Money");
        }

        if (Currency != otherMoney.Currency)
        {
            throw new InvalidOperationException($"Cannot compare money of different currencies: {Currency} and {otherMoney.Currency}");
        }

        return Amount.CompareTo(otherMoney.Amount);
    }

#pragma warning disable CS8618
    public Price() { } // Orms need a parameterless constructor
#pragma warning restore CS8618
}
