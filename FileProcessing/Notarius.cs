using System.Text.RegularExpressions;

namespace DataProcessing
{
    public class Notarius : IComparable<Notarius>
    {
        private string _streets;
        public string Number { get; set; }
        public string Name { get; set; }
        public string MetroStations { get; set; }
        public Contacts Contacts { get; set; }
        public Notarius() { }

        /// <summary>
        /// This constructor initializes fields using composition.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="metroStations"></param>
        /// <param name="address"></param>
        /// <param name="phones"></param>
        public Notarius(string number, string name, string metroStations, string address, string phones)
        {
            Number = number;
            Name = name;
            MetroStations = metroStations;
            Contacts = new Contacts(address, phones);
        }

        /// <summary>
        /// This method implements comparing interface.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Notarius? other)
        {
            return String.Compare(this.Street, other.Street);
        }
        /// <summary>
        /// This property extract street from address field of data.
        /// </summary>
        public string Street
        {
            get
            {
                // Getting address.
                string address = this.Contacts.Address;

                string[] data = address.Split(',', StringSplitOptions.RemoveEmptyEntries);

                // If second value is city, not street.
                if (data[1].Contains("г."))
                {
                    for (int j = 0; j < data[2].Length; j++)
                    {
                        // If there are no street, only home after city value.
                        if (Char.IsDigit(data[2][j]))
                        {
                            _streets = null;
                            break;
                        }
                    }
                    int k = 0;
                    // Comparing names of streets, that starts with upper case letter.
                    while (k < data[2].Length && !Char.IsUpper(data[2][k]))
                        k++;
                    // If there are np street value.
                    if (k >= data[2].Length)
                        return null;
                    _streets = data[2][k..];
                }
                else
                {
                    // Comparing names of streets, that starts with upper case letter.
                    int k = 0;
                    while (k < data[1].Length && !Char.IsUpper(data[1][k]))
                        k++;
                    if (k >= data[1].Length)
                        return null;
                    _streets = data[1][k..];
                }
                return _streets;
            }
        }
        /// <summary>
        /// This override method shows info about object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // If there are several phone numbers add "".
            if(this.Contacts.Phones.Split(",").Length > 1)
            {
                // If there are several metro stations add "".
                if(this.MetroStations.Split(",").Length >1)
                    return $"{Number},{Name},\"{Contacts.Phones}\",\"{Contacts.Address}\",\"{MetroStations}\"";
                else
                    return $"{Number},{Name},\"{Contacts.Phones}\",\"{Contacts.Address}\",{MetroStations}";
            }
            else
            {
                if (this.MetroStations.Split(",").Length > 1)
                    return $"{Number},{Name},{Contacts.Phones},\"{Contacts.Address}\",\"{MetroStations}\"";
                else
                    return $"{Number},{Name},{Contacts.Phones},\"{Contacts.Address}\",{MetroStations}";
            }
        }
    }
}