using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XmlProvider
{
    public class Utility
    {
        public static Utility Create()
        {
            return new Utility();
        }

        private string RoleFile
        {
            get { return System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Roles.xml"); }
        }

        public XDocument RoleDocument
        {
            get { return XDocument.Load(RoleFile); }
        }

        public void SaveRoleDocument(XDocument doc)
        {
            doc.Save(this.RoleFile);
        }


        private string MembershipFile
        {
            get { return System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Users.xml"); }
        }

        public XDocument MembershipDocument
        {
            get { return XDocument.Load(MembershipFile); }
        }

        public void SaveMembershipDocument(XDocument doc)
        {
            doc.Save(this.MembershipFile);
        }

        public  XElement UserElement(XDocument doc, string username)
        {
            var result = from user in doc.Element("Users").Elements("User")
                         where user.Element("Username").Value == username
                         select user;
            return result.SingleOrDefault();
        }
    }
}
