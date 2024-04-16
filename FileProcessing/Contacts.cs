using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    /// <summary>
    /// This class keeps address and phones data.
    /// </summary>
    public class Contacts
    {
        public string Address {  get; set; }
        public string Phones {  get; set; }
        public Contacts() { }
        public Contacts(string address, string phones)
        {
            Address = address;
            Phones = phones;
        }
    }
}
