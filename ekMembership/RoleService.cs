using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ekCommonLibs.IOC;

namespace ekCommonLibs.Providers
{
    public class RoleService
    {
        private IRoleProvider _dal = null;

        public RoleService(IRoleProvider dal)
        {
            this._dal = dal;
        }

        public RoleService(): this(ConfigIoC.Instance.Resolve<IRoleProvider>())
        {
            
        }

        public List<string> GetRoles()
        {
            return this._dal.GetRoles();
        }

        public bool Exists(string role)
        {
            return this._dal.Exists(role);
        }

        public void Insert(string role)
        {
            this._dal.Insert(role);
        }

        public void Delete(string role)
        {
            this._dal.Delete(role);
        }

        public void AddUserToRole(string username, string role)
        {
            this._dal.AddUserToRole(username, role);
        }

        public void RemoveUserFromRole(string username, string role)
        {
            this._dal.RemoveUserFromRole(username, role);
        }

        public bool UserHasRole(string role)
        {
            return this._dal.UserHasRole(null, role);
        }

        public bool UserHasRole(string username, string role)
        {
            return this._dal.UserHasRole(username, role);
        }

        public List<string> GetRolesForUser(string username)
        {
            return _dal.GetRolesForUser(username);
        }
    }
}
