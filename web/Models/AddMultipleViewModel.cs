
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ContactWebLibrary;

namespace ContactWeb.Models
{
    public class AddMultipleViewModel : IValidatableObject
    {
        public List<Contact> Contacts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if(Contacts != null && Contacts.Where(c=>!string.IsNullOrEmpty(c.Username)).GroupBy(c=>c.Username.ToLower()).Where(g=>g.Count() > 1).Any())
                yield return new ValidationResult("You can't have duplicate usernames.");
                
        }
    }
}