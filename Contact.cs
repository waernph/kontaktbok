namespace kontaktbok;

public class Contact
{
    public string Name { get; set; }
    public string Adress { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public string EMail { get; set; }

    public Contact(string name, string adress, string zipCode, string city, string phone, string eMail)
    {
        Name = name;
        Adress = adress;
        ZipCode = zipCode;
        City = city;
        Phone = phone;
        EMail = eMail;
    }
}