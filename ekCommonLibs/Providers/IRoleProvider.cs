using System.Collections.Generic;

namespace ekCommonLibs.Providers
{
    public interface IRoleProvider
    {
        List<string> GetRoles();
        bool Exists(string role);
        void Insert(string role);
        void Delete(string role);
        void AddUserToRole(string username, string role);
        void RemoveUserFromRole(string username, string role);
        bool UserHasRole(string username, string role);
        List<string> GetRolesForUser(string username);
    }
}
