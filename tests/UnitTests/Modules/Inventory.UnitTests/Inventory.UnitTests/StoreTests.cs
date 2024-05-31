using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using FluentAssertions;
using Inventory.Domain.Stores;
using Inventory.Domain.Stores.DomainEvents;
using UnitTests.Common;
using Xunit;

namespace Inventory.UnitTests;

public class StoreTests : AggregateTests<Store, StoreId>
{
    private static readonly ApplicationUserId _ownerId = ApplicationUserId.New();
    private const string Name = "Store Name";
    private const string Description = "Store Description";
    private static readonly Uri _logoUrl = new("https://store.com/logo.png");

    [Fact]
    public void CreateStoreShouldRaiseStoreCreatedDomainEvent()
    {
        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .Then<StoreCreatedDomainEvent>(
                @event => @event.OwnerId.Should().Be(_ownerId),
                @event => @event.Name.Should().Be(Name),
                @event => @event.Description.Should().Be(Description),
                @event => @event.LogoUrl.Should().Be(_logoUrl));
    }

    [Fact]
    public void UpdateStoreNameShouldRaiseStoreNameUpdatedDomainEvent()
    {
        const string newName = "New Store Name";

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: newName, description: null))
            .Then<StoreNameUpdatedDomainEvent>(
                @event => @event.OldName.Should().Be(Name),
                @event => @event.NewName.Should().Be(newName));
    }

    [Fact]
    public void UpdateStoreDescriptionShouldRaiseStoreDescriptionUpdatedDomainEvent()
    {
        const string newDescription = "New Store Description";

        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: null, description: newDescription))
            .Then<StoreDescriptionUpdatedDomainEvent>(
                @event => @event.OldDescription.Should().Be(Description),
                @event => @event.NewDescription.Should().Be(newDescription));
    }

    [Fact]
    public void UpdateStoreNameShouldReturnSameValueErrorWhenSameValueIsGiven()
    {
        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: Name, description: null))
            .ThenError<Error>(
                error => error.ShouldBeSameValueError(parameterName: nameof(Store.Name), value: Name));
    }

    [Fact]
    public void UpdateStoreDescriptionShouldReturnSameValueErrorWhenSameValueIsGiven()
    {
        Given(() => Store.Create(_ownerId, Name, Description, _logoUrl))
            .When(store => store.Update(name: null, description: Description))
            .ThenError<Error>(
                error => error.ShouldBeSameValueError(parameterName: nameof(Store.Description), value: Description));
    }

}
