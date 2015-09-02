using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactWebLibrary;

namespace ContactWeb.Models
{
    public class MobileContactViewModel
    {
        public Contact Contact { get; set; }
        public Dictionary<int, int> FriendsCount { get; set; }

        public int NumberOfFriends(Contact contact)
        {
            if (!FriendsCount.ContainsKey(contact.Id))
                return 0;

                return FriendsCount[contact.Id];
        }

        public string Theme(Contact contact)
        {
            switch(NumberOfFriends(contact))
            {
                case 0:
                    return "data-theme=a";
                    break;
                case 1:
                case 2:
                case 3:
                    return String.Empty;
                    break;
                default:
                    return "data-theme=b";
            }
        }

    }
}