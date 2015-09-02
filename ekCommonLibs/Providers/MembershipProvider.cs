using System.Web.Security;

namespace ekCommonLibs.Providers
{
    class MembershipProvider : IMembershipProvider
    {
        public bool UserExists(string username)
        {
            return Membership.GetUser(username) != null;
        }


        public bool CreateUser(string username, string password, string email)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(username, password, email, null, null, true,
                                  out status);


            return status != MembershipCreateStatus.Success;

        }


        public void DeleteUser(string username)
        {
            Membership.DeleteUser(username, true);
        }


        public bool Authenticate(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }
    }
}
