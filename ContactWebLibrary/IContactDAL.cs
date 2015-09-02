using System.Collections.Generic;
using System.Linq;
using ContactWebLibrary;

namespace ContactWebLibrary
{
    public interface IContactDAL
    {
        void EditContact(Contact contact);
        void CreateContact(Contact contact);
        void CreateContact(List<Contact> contacts);
        void DeleteContact(Contact contact);
        IQueryable<Contact> Contacts();
        void Clear();
    }
}
