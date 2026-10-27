using Common.Domain;
using Common.Domain.ResultMonad;
using Products.Domain.Products.Errors;

namespace Products.Domain.Products;

/// <summary>
///     Size and weight of a product as shipped. A value object: two instances with the same values are the same value, it has
///     no identity and no table of its own (the product's row holds its columns). Built only through <see cref="Create" />,
///     which rounds to the stored precision and refuses values outside what a package can be.
/// </summary>
public sealed class PackageDimensions : ValueObject
{
    private const int SideDecimals = 2;
    private const int WeightDecimals = 3;

    private PackageDimensions()
    {
    } // ORMs need a parameterless ctor

    public decimal LengthCm { get; private set; }
    public decimal WidthCm { get; private set; }
    public decimal HeightCm { get; private set; }
    public decimal WeightKg { get; private set; }

    private decimal VolumeCm3 => LengthCm * WidthCm * HeightCm;

    public static Result<PackageDimensions> Create(decimal lengthCm, decimal widthCm, decimal heightCm, decimal weightKg)
    {
        var length = decimal.Round(lengthCm, SideDecimals, MidpointRounding.AwayFromZero);
        var width = decimal.Round(widthCm, SideDecimals, MidpointRounding.AwayFromZero);
        var height = decimal.Round(heightCm, SideDecimals, MidpointRounding.AwayFromZero);
        var weight = decimal.Round(weightKg, WeightDecimals, MidpointRounding.AwayFromZero);

        if (!IsValidSide(length) || !IsValidSide(width) || !IsValidSide(height) || weight <= 0 || weight > Constants.PackageWeightMaxKg)
        {
            return ProductErrors.InvalidPackageDimensions;
        }

        return new PackageDimensions { LengthCm = length, WidthCm = width, HeightCm = height, WeightKg = weight };
    }

    private static bool IsValidSide(decimal side)
    {
        return side > 0 && side <= Constants.PackageSideMaxCm;
    }

    /// <summary>Bigger volume sorts after, equal volumes by weight.</summary>
    public override int CompareTo(ValueObject? other)
    {
        if (other is not PackageDimensions dimensions)
        {
            return 1;
        }

        var byVolume = VolumeCm3.CompareTo(dimensions.VolumeCm3);
        return byVolume != 0 ? byVolume : WeightKg.CompareTo(dimensions.WeightKg);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LengthCm;
        yield return WidthCm;
        yield return HeightCm;
        yield return WeightKg;
    }
}
