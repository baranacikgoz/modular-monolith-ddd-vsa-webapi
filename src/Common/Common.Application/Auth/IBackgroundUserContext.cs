using Common.Domain.StronglyTypedIds;

namespace Common.Application.Auth;

/// <summary>
///     The user a background job runs on behalf of. A queue consumer or a recurring job has no <c>HttpContext</c>, so
///     <see cref="ICurrentUser" /> is empty there and every row the job writes would carry no <c>CreatedBy</c> or
///     <c>LastModifiedBy</c>. A job runner sets the user its job row recorded (its <c>RequestedBy</c>) once at the start
///     of its scope; <see cref="ICurrentUser" /> then answers with that user whenever the scope has no authenticated
///     request. Scoped, one job run and one user: a runner that opens a scope per unit of work sets the user in each
///     scope. Only the identity is carried, never roles: a job does not authorize anything.
/// </summary>
public interface IBackgroundUserContext
{
    ApplicationUserId? UserId { get; }

    void Set(ApplicationUserId? userId);
}
