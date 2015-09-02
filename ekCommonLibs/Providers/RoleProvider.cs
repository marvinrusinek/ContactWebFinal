using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace ekCommonLibs.Providers
{
    class RoleProvider : IRoleProvider
    {
        
        public List<string> GetRoles()
        {
            return Roles.GetAllRoles().ToList();
        }

        public bool Exists(string role)
        {
            return Roles.RoleExists(role);
        }

        public void Insert(string role)
        {
            Roles.CreateRole(role);
        }

        public void Delete(string role)
        {
            Roles.DeleteRole(role);
        }

        public void AddUserToRole(string username, string role)
        {
            Roles.AddUserToRole(username, role);
        }

        public void RemoveUserFromRole(string username, string role)
        {
            Roles.RemoveUserFromRole(username, role);
        }

        public bool UserHasRole(string username, string role)
        {
            if (username == null && Membership.GetUser() == null)
                return false;
            return Roles.IsUserInRole(username ?? Membership.GetUser().UserName, role);
        }

        public List<string> GetRolesForUser(string username)
        {
            return Roles.GetRolesForUser(username).ToList();
        }
    }
}
