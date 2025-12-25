using Microsoft.EntityFrameworkCore;
using Products.Domain.ProductTemplates;

namespace Products.Infrastructure.Persistence.Seeding;

internal sealed partial class Seeder
{
    private async Task<List<ProductTemplateId>> SeedProductTemplatesAsync(CancellationToken cancellationToken)
    {
        var productIds = new List<ProductTemplateId>(ProductCount);

        for (var i = 0; i < ProductCount; i++)
        {
            var brand = $"Seed Product Template {i + 1}";
            var model = $"Seed Model {i + 1}";
            var color = $"Seed Color {i + 1}";

            productIds.Add(await SeedProductsAsync(brand, model, color, cancellationToken));
        }

        return productIds;
    }

    private async Task<ProductTemplateId> SeedProductsAsync(string brand, string model, string color,
        CancellationToken cancellationToken)
    {
        if (await dbContext.ProductTemplates.SingleOrDefaultAsync(product => product.Brand == brand, cancellationToken)
            is not ProductTemplate productTemplate)
        {
            productTemplate = ProductTemplate.Create(brand, model, color);

            await dbContext.ProductTemplates.AddAsync(productTemplate, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return productTemplate.Id;
    }
}
