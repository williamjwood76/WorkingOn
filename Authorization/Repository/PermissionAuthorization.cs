using Microsoft.AspNetCore.Authorization;

namespace Paychex_SimpleTimeClock.Authorization.Repository
{
    public class PermissionAuthorization : IAuthorizationRequirement
    {
        public IEnumerable<string> RequiredPermissions { get; }

        public PermissionAuthorization(IEnumerable<string> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}
