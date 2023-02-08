using Paychex_SimpleTimeClock.Authorization.Interface;
using Paychex_SimpleTimeClock.DataAccess.Interface;

namespace Paychex_SimpleTimeClock.Authorization.Repository
{
    public class PermissionProvider : IPermissionProvider
    {
        private readonly IPaychexDataAccess _paychexDataAccess;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionProvider(IPaychexDataAccess paychexDataAccess, IHttpContextAccessor httpContextAccessor)
        {
            _paychexDataAccess = paychexDataAccess;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> IsUserAuthorized(string permission)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext?.Session.GetString("UserID") ?? "-1");
            
            if (userId < 0)
                return false;

            var user = await _paychexDataAccess.GetLoggedInUser(userId);

            if(user == null)
                return false;

            return permission switch
            {
                Permission.Employee => user.UserRoleID == PermissionInt.Employee,
                Permission.Admin => user.UserRoleID == PermissionInt.Admin,
                _ => false
            };
        }
    }
}
