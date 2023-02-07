namespace Paychex_SimpleTimeClock.Authorization.Interface
{
    public interface IPermissionProvider
    {
        public Task<bool> IsUserAuthorized(string permission);
    }
}
