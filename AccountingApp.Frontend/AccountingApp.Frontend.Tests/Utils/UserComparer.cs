using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AccountingApp.Frontend.Tests.Utils
{
    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User u1, User u2)
        {
            return u1.Email == u2.Email &&
                   u1.Password == u2.Password;
        }

        public int GetHashCode([DisallowNull] User u)
        {
            return HashCode.Combine(u.Email, u.Password);
        }
    }
}
