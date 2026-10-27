namespace Products.Domain.Products;

/// <summary>One name and value pair of a product. Compared by content; kept in the product's jsonb attribute list, not a table of its own.</summary>
public sealed record ProductAttribute(string Name, string Value);
