using System.Collections.Generic;


namespace ContactWebLibrary
{
    public interface IFriendsDAL
    {
        List<Contact> GetFriends(Contact contact);
        Dictionary<int, int> FriendsCountMap();
        void SetFriends(Contact contact, List<Contact> contacts);
        void SetFriends(List<Contact> contact);
        void Clear();
    }
}
