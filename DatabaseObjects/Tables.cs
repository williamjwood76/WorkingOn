using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paychex_SimpleTimeClock.DatabaseObjects
{
    public class AvailableShifts
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvailableShiftID { get; set; }

        [Required, MaxLength(50)]
        public string AvailableShiftName { get; set; }

        [Required]
        public bool Active { get; set; }
    }

    public class AvailableBreaks
    {
        [Key, Required]
        public int AvailableBreakID { get; set; }

        [Required]
        public int AvailableShiftID { get; set; }

        [Required, MaxLength(50)]
        public string AvailableBreakName { get; set; }

        [Required]
        public bool Active { get; set; }
    }

    public class Roles
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        [Required]
        public bool Active { get; set; }
    }

    public class Users
    {
        [Key, Required]
        public int UserID { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public int UserRoleID { get; set; }

        [Required]
        public bool Active { get; set; }
    }

    public class UserBreaks
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserBreakID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int AvailableBreakID { get; set; }

        [Required]
        public DateTime UserBreakStart { get; set; }

        [Required]
        public DateTime UserBreakEnd { get; set; }

    }

    public class UserShifts
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserShiftID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int AvailableShiftID { get; set; }

        [Required]
        public DateTime UserShiftStart { get; set; }

        public DateTime? UserShiftEnd { get; set; }

    }
}
