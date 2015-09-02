using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactWebLibrary;

namespace ContactWeb.Models
{
    public class ContactListAlphaViewModel
    {
        public string SelectedLetter { get; set; }
        public List<Contact> Contacts { get; set; }
        private Dictionary<string, int> _letterDictionary = null;
        public Dictionary<string, int> LetterDictionary
        {
            get
            {
                if(_letterDictionary == null)
                {
                    _letterDictionary = new Dictionary<string, int>();
                    foreach (var letter in "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(','))
                        _letterDictionary[letter] = 0;
                }
                return _letterDictionary;
            }
        }
    }
}