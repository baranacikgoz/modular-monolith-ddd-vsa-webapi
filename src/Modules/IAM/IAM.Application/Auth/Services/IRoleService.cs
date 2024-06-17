namespace IAM.Application.Auth.Services;
public interface IRoleService
{
    Task<Guid?> GetRoleIdByName(string roleName, CancellationToken cancellationToken);
}
