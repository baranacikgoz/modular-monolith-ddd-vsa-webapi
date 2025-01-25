using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.ProductTemplates;
using Common.Application.CQS;

namespace Products.Application.ProductTemplates.Features.Create;

public sealed class CreateProductTemplateCommandHandler(
    IRepository<ProductTemplate> repository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductTemplateCommand, ProductTemplateId>
{
    public async Task<Result<ProductTemplateId>> Handle(CreateProductTemplateCommand command, CancellationToken cancellationToken)
        => await Result<ProductTemplate>
            .Create(() => ProductTemplate.Create(command.Brand, command.Model, command.Color))
            .Tap(product => repository.Add(product))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken))
            .MapAsync(product => product.Id);
}
