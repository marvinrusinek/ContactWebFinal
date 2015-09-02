using System.Collections.Generic;
using ekCommonLibs;
using ekCommonLibs.IOC;


namespace ContactWebLibrary
{
    public class FriendsBUS
    {
        private IFriendsDAL _dal = null;

        public FriendsBUS(IFriendsDAL dal)
        {
            _dal = dal;
        }

        public FriendsBUS() : this(ConfigIoC.Instance.Resolve<IFriendsDAL>())
        {

        }

        public List<Contact> GetFriends(Contact contact)
        {
            return _dal.GetFriends(contact);
        }

        public void SetFriends(Contact contact, List<Contact> friends)
        {
            friends.RemoveAll(f => f.Id == contact.Id);
            this._dal.SetFriends(contact, friends);
        }

        public void SetFriends(List<Contact> contacts)
        {
            this._dal.SetFriends(contacts);
        }

        public Dictionary<int, int> FriendsCount()
        {
            return this._dal.FriendsCountMap();
        }

        public void Clear()
        {
            _dal.Clear();
        }
    }
}