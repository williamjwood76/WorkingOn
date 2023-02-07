namespace Paychex_SimpleTimeClock.Controllers
{
    public static class Extensions
    {
        public static string? GetStringValue(this IEnumerable<KeyValuePair<string, object>> list, string keyName)
        {
            if (list.Where(x => x.Key == keyName).Any())
                return list
                     .Where(x => x.Key == keyName)
                     .FirstOrDefault()
                     .Value
                     .ToString();

            return null;
        }
    }
}
