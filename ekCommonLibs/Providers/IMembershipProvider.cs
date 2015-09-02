using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ekCommonLibs.Providers
{
    public interface IMembershipProvider
    {
        bool UserExists(string username);
        bool CreateUser(string username, string password, string email);
        void DeleteUser(string username);
        bool Authenticate(string username, string password);
    }
}
