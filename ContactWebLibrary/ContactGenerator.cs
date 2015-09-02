using System;
using System.Collections.Generic;
using System.Linq;


namespace ContactWebLibrary
{
    public class ContactGenerator
    {
        public static int Seed = 0;
        public char RandomLetter()
        {
            var _random = new Random(Seed++);
            int num = _random.Next(0, 26); // Zero to 25
            char let;
            //do
            //{
                let = (char) ('A' + num);
            //}while(!(new char[] {'A', 'O', 'I', 'O', 'U'}.Contains(let)));
            return let;
        }

        public char Vowel()
        {
            var _random = new Random(Seed++);
            return new char[] {'a', 'e', 'i', 'o', 'u'}[_random.Next(0, 5)];
        }

        public void Populate()
        {
            var logic = new ContactBUS();
            //var oldContacts = logic.GetContacts();

            var friends = new FriendsBUS();

            friends.Clear();
            logic.Clear();

            //foreach (var contact in oldContacts)
            //{
            //    friends.SetFriends(contact, new List<Contact>());
            //    logic.DeleteContact(contact);
            //}
            var _contacts = new List<Contact>();
            var firstNames = new List<string> {"orticia","tewey","eter", "oe", "arry", "urly", "ert", "rnie", "ard", "ack", "christie","orbus","ill" };
            var lastNames = new List<string> {"riffin", "tevens", "leaver", "rummond", "unster", "etri", "artridge","ith","ein", "ian" };
            var domains = new List<string> { "gmail.com", "yahoo.com", "msn.com", "hotmail.com", "aol.com", "mac.com", "israel.net", "tishabov.us" };

            foreach (var lastName in lastNames)
                foreach (var firstName in firstNames)
                    foreach (var domain in domains)
                    {
                        var first_ltr = RandomLetter();
                        var last_ltr = RandomLetter();
                        var vowel = Vowel();
                        _contacts.Add(new Contact
                                          {
                                              Email = string.Format("{3}{0}_{4}{1}{5}@{2}", firstName, lastName, domain, first_ltr, last_ltr, vowel),
                                              FirstName = String.Format("{0}{1}", first_ltr, firstName),
                                              LastName = String.Format("{0}{1}{2}", last_ltr, lastName, vowel)
                                          });
                    }

            foreach (var contact in _contacts)
            {
                var username = String.Format("{0}{1}", contact.FirstName.Substring(0, 1), contact.LastName);
                var seed = 0;
                while (_contacts.Where(c => String.Equals(c.Username, username, StringComparison.CurrentCultureIgnoreCase)).Any())
                    username = String.Format("{0}{1}", username, new Random((int)DateTime.Now.Ticks*++seed).Next(9));
                contact.Username = username;
            }
            logic.CreateContact(_contacts);
            PopulateFriends();
        }

        void PopulateFriends()
        {
            var friendsBUS = new FriendsBUS();
            var contacts = new ContactBUS().GetContacts();
            foreach(var contact in contacts)
            {
                var numberOfFriends = new Random(Seed++).Next(0, 6);
                var friends = new List<Contact>();
                for(var i = 0 ; i < numberOfFriends; i++)
                {
                    var index = new Random(Seed++).Next(0, contacts.Count);
                    if(!friends.Contains(contacts[index]) && contacts[index].Id != contact.Id)
                        friends.Add(contacts[index]);
                }
                contact.Friends = friends;
            }
            friendsBUS.SetFriends(contacts);
        }
    }
}