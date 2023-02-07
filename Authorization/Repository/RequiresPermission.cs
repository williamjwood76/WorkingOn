using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Paychex_SimpleTimeClock.Authorization.Interface;

namespace Paychex_SimpleTimeClock.Authorization.Repository
{
    public class RequiresPermission : TypeFilterAttribute
    {

        public RequiresPermission(params string[] permissions) : base(typeof(RequiresPermissionExecutor))
        {
            Arguments = new[] { new PermissionAuthorization(permissions) };
        }


        private class RequiresPermissionExecutor : Attribute, IAsyncResourceFilter
        {
            private readonly PermissionAuthorization _requiredPermissions;
            private readonly IPermissionProvider _permissionProvider;

            public RequiresPermissionExecutor(PermissionAuthorization requiredPermissions, IPermissionProvider permissionProvider)
            {
                _requiredPermissions = requiredPermissions;
                _permissionProvider = permissionProvider;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate dele)
            {
                var userId = context.HttpContext.Session.GetString("UserID");

                // No logged in user
                if (userId == null)
                    context.HttpContext.Response.Redirect("Error");

                var isInOneOfThisRole = false;

                foreach (var item in _requiredPermissions.RequiredPermissions)
                {
                    isInOneOfThisRole = await _permissionProvider.IsUserAuthorized(item);
                }

                //Not in Role
                if (!isInOneOfThisRole)
                    context.HttpContext.Response.Redirect("Error");
                else
                    await dele();
            }
        }
    }

}
