namespace DataProcessing
{
    public class Notarius
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string MetroStations {  get; set; }
        public Contacts contacts {  get; set; }
        public Notarius() { }
        public Notarius(int number, string name, string metroStations, Contacts contacts)
        {
            Number = number;
            Name = name;
            MetroStations = metroStations;
            this.contacts = new Contacts(contacts.Street, contacts.HomeNumber, contacts.Index, contacts.Phones);
        }
    }
}