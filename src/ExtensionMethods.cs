using System.Collections.Generic;
using System.Linq;
using API.Database.Entities;

namespace API
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static string GetSalt(this User user) { return user.Password.Substring(0, 27) + '='; }
    }
}