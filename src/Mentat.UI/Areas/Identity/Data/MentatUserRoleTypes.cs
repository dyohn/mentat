using System.Collections.Generic;
using System.Linq;

namespace Mentat.UI.Areas.Identity.Data
{
    public static class MentatUserRolesTypes
    {
        static ICollection<string> _roles;
        static ICollection<string> _privilegedRoles;
        
        static MentatUserRolesTypes()
        {
            _roles = new HashSet<string>()
            {
                MentatUserRolesTypes.Student
            };

            _privilegedRoles = new HashSet<string>()
            {
                MentatUserRolesTypes.Mentor
            };
        }

        public static ICollection<string> AllRoles()
        {            
            return _roles.Concat(_privilegedRoles).ToList<string>();
        }

        public static ICollection<string> PrivilegedRoles()
        {
            return _privilegedRoles.ToList<string>();
        }

        public static bool IsPrivilegedUserRole(string userRolesType)
        {
            return _privilegedRoles.Contains(userRolesType);
        }

        public static string Mentor => "Mentor";

        public static string Student => "Student";
    }
}
