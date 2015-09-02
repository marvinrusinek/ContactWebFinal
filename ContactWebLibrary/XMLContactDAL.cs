using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ContactWebLibrary
{
    public class XMLContactDAL : IContactDAL
    {

        XDocument ContactDoc
        {
            get { return XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml")); }
        }

        private XElement MapFromContact(Contact contact)
        {
            return new XElement("Contact",
                             new XElement("Id", contact.Id),
                             new XElement("FirstName", contact.FirstName),
                             new XElement("LastName", contact.LastName),
                             new XElement("Email", contact.Email),
                             new XElement("Username", contact.Username)
                    );
        }


        public void EditContact(Contact contact)
        {
            var doc = ContactDoc;

            var contactToEdit = (from c in doc.Descendants("Contact")
                                 where (int)c.Element("Id") == contact.Id
                                 select c).SingleOrDefault();
            contactToEdit.ReplaceWith(MapFromContact(contact));

            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml"));

        }

        public void CreateContact(Contact contact)
        {
            var doc = ContactDoc;
            var maxId = 0;
            if (doc.Descendants("Id").Count() > 0)
                maxId = (from id in doc.Descendants("Id")
                         select (int)id).Max();
            contact.Id = maxId + 1;

            doc.Element("Contacts").Add(MapFromContact(contact));

            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml"));
        }

        public void CreateContact(List<Contact> contacts)
        {
            var doc = ContactDoc;
            var maxId = 0;
            if (doc.Descendants("Id").Count() > 0)
                maxId = (from id in doc.Descendants("Id")
                         select (int)id).Max();

            foreach (var contact in contacts)
            {
                contact.Id = maxId++;
                doc.Element("Contacts").Add(MapFromContact(contact));
            }

            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml"));
        }


        public void DeleteContact(Contact contact)
        {
            var doc = ContactDoc;
            var contactToDelete = (from c in doc.Descendants("Contact")
                                   where (int)c.Element("Id") == contact.Id
                                   select c).SingleOrDefault();
            contactToDelete.Remove();

            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml"));
        }



        public IQueryable<Contact> Contacts()
        {
            var doc = ContactDoc;
            var results = from contact in doc.Descendants("Contact")
                          select new Contact
                          {
                              Id = (int)contact.Element("Id"),
                              FirstName = (string)contact.Element("FirstName"),
                              LastName = (string)contact.Element("LastName"),
                              Email = (string)contact.Element("Email"),
                              Username = (string)contact.Element("Username")
                          };
            return results.AsQueryable();
        }


        public void Clear()
        {
            var doc = this.ContactDoc;
            doc.Descendants("Contact").Remove();
            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Contacts.xml"));
        }
    }
}