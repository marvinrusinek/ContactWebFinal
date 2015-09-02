using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactWebLibrary;
using db = SqlContactLibrary;
using model = ContactWebLibrary;

namespace SqlContactLibrary
{
    public class SQLContactDAL : IContactDAL
    {


        private db.Contact MapToDB(model.Contact contact)
        {
            return new db.Contact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Username = contact.Username
            };
        }

        ContactDBDataContext _dataContext
        {
            get
            {
                return new ContactDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["ContactDB"].ConnectionString);
            }
        }

        public void EditContact(model.Contact contact)
        {
            var db = this._dataContext;
            var contactToEdit = db.Contacts.Where(c => c.Id == contact.Id).SingleOrDefault();
            contactToEdit.FirstName = contact.FirstName;
            contactToEdit.LastName = contact.LastName;
            contactToEdit.Username = contact.Username;
            contactToEdit.Email = contact.Email;
            db.SubmitChanges();
        }

        public void CreateContact(model.Contact contact)
        {
            var db = this._dataContext;
            var contactToInsert = MapToDB(contact);
            db.Contacts.InsertOnSubmit(contactToInsert);
            db.SubmitChanges();
        }

        public void CreateContact(List<model.Contact> contacts)
        {
            var db = this._dataContext;
            foreach(var contact in contacts)
                db.Contacts.InsertOnSubmit(MapToDB(contact));
            db.SubmitChanges();
        }

        public void DeleteContact(model.Contact contact)
        {
            var db = this._dataContext;
            var contactToDelete = db.Contacts.Where(c => c.Id == contact.Id).SingleOrDefault();
            db.Contacts.DeleteOnSubmit(contactToDelete);
            db.SubmitChanges();
        }



        public IQueryable<model.Contact> Contacts()
        {
            var db = this._dataContext;
            return from c in db.Contacts
                   select new ContactWebLibrary.Contact
                              {
                                  Id = c.Id,
                                  Email = c.Email,
                                  FirstName = c.FirstName,
                                  LastName = c.LastName,
                                  Username = c.Username,
                                  Friends = (from f in db.Friends
                                             where f.ContactId1 == c.Id || f.ContactId2 == c.Id
                                             select new ContactWebLibrary.Contact
                                                        {
                                                            Id = c.Id,
                                                            Email = c.Email,
                                                            FirstName = c.FirstName,
                                                            LastName = c.LastName,
                                                            Username = c.Username
                                                        }).ToList()
                              };
        }

        public void Clear()
        {
            var db = _dataContext;
            db.Contacts.DeleteAllOnSubmit(db.Contacts);
            db.SubmitChanges();
        }

    }
}
