namespace Products.Endpoints.Products.v1.My.Update;

public sealed record Request(string? Name, string? Description, int? Quantity, decimal? Price);
