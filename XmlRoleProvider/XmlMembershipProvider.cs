using System;
using System.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Security;
using System.Web.Hosting;
using System.Web.Management;
using System.Security.Permissions;
using System.Web;
using System.Xml.Linq;

namespace XmlProvider
{
    public class XmlMembershipProvider : MembershipProvider
    {
        private Utility _utility = null;

        private Utility Util
        {
            get
            {
                if (_utility == null)
                    _utility = Utility.Create();
                return _utility;
            }
        }

        // MembershipProvider Properties
        public override string ApplicationName
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotSupportedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotSupportedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotSupportedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotSupportedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        // MembershipProvider Methods
        //public override void Initialize(string name,
        //    NameValueCollection config)
        //{
        //    base.Initialize(name, config);
        //}

        public override bool ValidateUser(string username, string password)
        {

            try
            {
                var doc = Util.MembershipDocument;
                var userElement = Util.UserElement(doc, username);
                if (userElement != null && userElement.Element("Password").Value == password)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override MembershipUser GetUser(string username,
            bool userIsOnline)
        {

            var doc = Util.MembershipDocument;
            var userElement = Util.UserElement(doc, username);
            if(userElement == null)
                return null;
            else
            {
                return FromXElement(userElement);
            }
        }

        private MembershipUser FromXElement(XElement elem)
        {
            return new MembershipUser(
                              Name,                       // Provider name
                              elem.Element("Username").Value, // Username
                              null,                       // providerUserKey
                              elem.Element("Email").Value,    // Email
                              String.Empty,               // passwordQuestion
                              elem.Element("Password").Value, // Comment
                              true,                       // isApproved
                              false,                      // isLockedOut
                              DateTime.Now,               // creationDate
                              DateTime.Now,               // lastLoginDate
                              DateTime.Now,               // lastActivityDate
                              DateTime.Now, // lastPasswordChangedDate
                              new DateTime(1980, 1, 1)    // lastLockoutDate
                          );          
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex,
            int pageSize, out int totalRecords)
        {


            MembershipUserCollection users =
                new MembershipUserCollection();

            var doc = Util.MembershipDocument;
            var userElements = from u in doc.Descendants("User")
                               select u;
            foreach(var elem in userElements)
            {
                users.Add(FromXElement(elem));
            }
            totalRecords = users.Count;
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override bool ChangePassword(string username,
            string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool
            ChangePasswordQuestionAndAnswer(string username,
            string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser CreateUser(string username,
            string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey,
            out MembershipCreateStatus status)
        {
            var xDoc = Util.MembershipDocument;
            var node = new XElement("User",
                                    new XElement("Username", username),
                                    new XElement("Password", password),
                                    new XElement("Email", email),
                                    new XElement("Roles", String.Empty)
                );
            var usersNode = xDoc.Element("Users");
            usersNode.Add(node);
            Util.SaveMembershipDocument(xDoc);
            status = MembershipCreateStatus.Success;
            return this.GetUser(username, false);
        }

        public override bool DeleteUser(string username,
            bool deleteAllRelatedData)
        {
            var xDoc = Util.MembershipDocument;
            var elem = Util.UserElement(xDoc, username);
            elem.Remove();
            Util.SaveMembershipDocument(xDoc);
            return true;
        }

        public override MembershipUserCollection
            FindUsersByEmail(string emailToMatch, int pageIndex,
            int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection
            FindUsersByName(string usernameToMatch, int pageIndex,
            int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(object providerUserKey,
            bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override string ResetPassword(string username,
            string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

    }

}