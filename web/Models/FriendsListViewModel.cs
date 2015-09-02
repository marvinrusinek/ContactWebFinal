using System.Collections.Generic;
using ContactWebLibrary;

namespace ContactWeb.Models
{
    public class FriendsListViewModel
    {
        public Contact You { get; set; }
        public List<Contact> YourFriends { get; set; }
    }
}