namespace IAM.Application.Auth.Services;

public interface IRoleService
{
    Task<DefaultIdType?> GetRoleIdByName(string roleName, CancellationToken cancellationToken);
}
