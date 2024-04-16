using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public class Contacts
    {
        public string Street {  get; set; }
        public string HomeNumber {  get; set; }
        public string Index {  get; set; }
        public string Phones { get; set; }
        public Contacts() { }
        public Contacts(string street, string homeNumber, string index, string phones)
        {
            Street = street;
            HomeNumber = homeNumber;
            Index = index;
            Phones = phones;
        }
    }
}
