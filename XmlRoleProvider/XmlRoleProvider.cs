using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Hosting;
using System.Xml;
using System.Security.Permissions;
using System.Web;
using System.Xml.Linq;
using System.Linq;

namespace XmlProvider
{
    public class XmlRoleProvider : RoleProvider
    {

        // RoleProvider properties
        public override string ApplicationName
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        // RoleProvider methods
        //public override void Initialize(string name,
        //    NameValueCollection config)
        //{

        //    // Call the base class's Initialize method
        //    base.Initialize(name, config);


        //}



        private Utility _utility = null;

        private Utility Util
        {
            get
            {
                if(_utility == null)
                    _utility = Utility.Create();
                return _utility;
            }
        }

        private string[] RolesForUserElement(XElement elem)
        {
            var roles = from role in elem.Descendants("Role")
                        select (string)role;
            return roles.ToArray();
        }
 
        public override bool IsUserInRole(string username, string roleName)
        {
            var doc = Util.MembershipDocument;
            var userNode = Util.UserElement(doc, username);
            var roles = RolesForUserElement(userNode);
            return roles.ToArray().Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var doc = Util.MembershipDocument;
            var userNode = Util.UserElement(doc, username);
            var roles = RolesForUserElement(userNode);
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var doc = Utility.Create().MembershipDocument;
            var users = from d in doc.Descendants("Role")
                        where (string) d == roleName
                        select (string) d.Parent.Parent.Element("Username");
            return users.ToArray();
        }

        public override string[] GetAllRoles()
        {
            var doc = Utility.Create().RoleDocument;
            var roles = from r in doc.Descendants("Role")
                        select (string)r;
            return roles.Distinct().ToArray();
        }

        public override bool RoleExists(string roleName)
        {
            return this.GetAllRoles().Contains(roleName);
        }

        public override void CreateRole(string roleName)
        {
            var xDoc = Utility.Create().RoleDocument;
            var root = xDoc.Element("Roles");
            var role = new XElement("Role", roleName);
            root.Add(role);
            Util.SaveRoleDocument(xDoc);
        }

        public override bool DeleteRole(string roleName,
            bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override void AddUsersToRoles(string[] usernames,
            string[] roleNames)
        {
            var doc = Util.MembershipDocument;
            XElement user = null;

            foreach(var username in usernames)
            {
                user = Util.UserElement(doc, username);
                foreach(var role in roleNames)
                    user.Element("Roles").Add(new XElement("Role", role));
            }
            Util.SaveMembershipDocument(doc);
        }

        public override string[] FindUsersInRole(string roleName,
            string usernameToMatch)
        {
            throw new NotSupportedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames,
            string[] roleNames)
        {
            var doc = Utility.Create().MembershipDocument;
            XElement user = null;

            foreach (var username in usernames)
            {
                user = Util.UserElement(doc, username);
                XElement roleNode = null;
                foreach (var role in roleNames)
                {
                    roleNode = (from r in user.Descendants("Role")
                               where (string) r == role
                               select r).SingleOrDefault();
                    roleNode.Remove();
                }
            }
            Util.SaveMembershipDocument(doc);
        }

    }
}

