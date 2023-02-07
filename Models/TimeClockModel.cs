using Paychex_SimpleTimeClock.DatabaseObjects;
using System.ComponentModel.DataAnnotations;

namespace Paychex_SimpleTimeClock.Models
{
    public class TimeClockModel
    {
        public List<AvailableShifts>? availableShifts { get; set; }

        public List<AvailableBreaks>? availableBreaks { get; set; }

        public List<Roles>? roles { get; set; }

        public List<Users>? users { get; set; }

        public List<UserBreaks>? userBreaks { get; set; }

        public List<UserShifts>? userShifts { get; set; }

    }
}
