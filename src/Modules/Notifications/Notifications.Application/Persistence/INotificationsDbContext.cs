using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Devices;

namespace Notifications.Application.Persistence;

public interface INotificationsDbContext : IDbContext
{
    DbSet<DeviceRegistration> DeviceRegistrations { get; }
}
