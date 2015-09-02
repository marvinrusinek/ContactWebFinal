using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ekCommonLibs.IOC;

namespace ekCommonLibs.Providers
{
    public class MembershipService
    {
        private IMembershipProvider _dal = null;

        public MembershipService(IMembershipProvider dal)
        {
            this._dal = dal;
        }

        public MembershipService(): this(ConfigIoC.Instance.Resolve<IMembershipProvider>())
        {
            
        }

        public bool UserExists(string username)
        {
            return _dal.UserExists(username);
        }

        public bool CreateUser(string username, string password, string email)
        {
            return _dal.CreateUser(username, password, email);
        }

        public void DeleteUser(string username)
        {
            _dal.DeleteUser(username);
        }

        public bool Authenticate(string username, string password)
        {
            return _dal.Authenticate(username, password);
        }
    }
}
