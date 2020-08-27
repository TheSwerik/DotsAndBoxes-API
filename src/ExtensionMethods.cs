﻿// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using API.Database.DTOs;
using API.Database.Entities;

namespace API
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> users) { return users.Select(x => x.ToDTO()); }
        public static UserDTO ToDTO(this User user) { return new UserDTO(user.Username); }
        public static string GetSalt(this User user) { return user.Password.Substring(0, 27) + '='; }
        public static bool HasSameUsernameAs(this User user, AuthenticateModel other)
        {
            return user.Username.Equals(other.Username, StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool HasSameUsernameAs(this User user, User other)
        {
            return user.Username.Equals(other.Username, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}