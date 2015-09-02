using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactWebLibrary
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Not a valid email address.")]
        public string Email { get; set; }

        [MaxLength(8, ErrorMessage = "Can not exceed 8 characters")]
        [MinLength(3, ErrorMessage = "Must be at least three characters")]
        [CustomValidation(typeof(Contact), "ValidateUniqueUsername")]
        [Required]
        public string Username { get; set; }

        private List<Contact> _friends = null;
        public List<Contact> Friends
        {
            set { this._friends = value; }
            get
            {
                if (this._friends == null)
                    this.Friends = new FriendsBUS().GetFriends(this);
                return this._friends;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(Email) &&
                       string.IsNullOrEmpty(Username);
            }
        }

        public static ValidationResult ValidateUniqueUsername(string username, ValidationContext context)
        {
            if (username == null)
                return ValidationResult.Success;

            var logic = new ContactBUS();
            var contact = context.ObjectInstance as Contact;
            var oldInfo = logic.GetContact(contact.Id);
            if (oldInfo == null || oldInfo.Username.ToLower() != username.ToLower())
                if(logic.UsernameExists(username))
                    return new ValidationResult("username exists in system");

            return ValidationResult.Success;
        }
    }
}