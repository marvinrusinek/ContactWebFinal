using ContactWebLibrary;
using ekCommonLibs.Providers;

namespace ContactWeb
{
    public class UserHelper
    {
        private RoleService Roles
        {
            get
            {
                return new RoleService();
            }
        }

        private MembershipService Membership
        {
            get
            {
                return new MembershipService();
            }
        }

        public static UserHelper Create()
        {
            return new UserHelper();
        }

        public bool CreateUser(int contactId)
        {
            var logic = new ContactBUS();
            var contact = logic.GetContact(contactId);
            return Membership.CreateUser(contact.Username, "Password", contact.Email);
        }

        public void DeleteUser(int contactId)
        {
            var logic = new ContactBUS();
            var contact = logic.GetContact(contactId);

            Membership.DeleteUser(contact.Username);
        }

        public void AddUserToRole(int contactId, string role)
        {
            var logic = new ContactBUS();
            var contact = logic.GetContact(contactId);
            Roles.AddUserToRole(contact.Username, role);
        }


        public void RemoveUserFromRole(int contactId, string role)
        {
            var logic = new ContactBUS();
            var contact = logic.GetContact(contactId);
            Roles.RemoveUserFromRole(contact.Username, role);
        }
    }
}