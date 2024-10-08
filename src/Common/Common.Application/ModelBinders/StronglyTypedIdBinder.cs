using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Application.ModelBinders;
public class StronglyTypedIdBinder<TId> : IModelBinder where TId : IStronglyTypedId, new()
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var modelType = bindingContext.ModelType;

        if (modelType is not TId)
        {
            bindingContext.ModelState.TryAddModelError(modelName, "Types are not matching.");
            return Task.CompletedTask;
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        if (!DefaultIdType.TryParse(valueProviderResult.FirstValue, out var guid))
        {
            bindingContext.ModelState.TryAddModelError(modelName, $"Can't bind to a {nameof(DefaultIdType)}.");
            return Task.CompletedTask;
        }

        var stronglyTypedId = new TId() { Value = guid };

        bindingContext.Result = ModelBindingResult.Success(stronglyTypedId);

        return Task.CompletedTask;
    }
}
