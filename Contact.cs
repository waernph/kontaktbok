using System.IO;
using System.Text.Json;

public class Contact
{
    public string Name { get; set; }
    public string Adress { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public string EMail { get; set; }

    public Contact(
        string name,
        string adress,
        string zipCode,
        string city,
        string phone,
        string eMail
    )
    {
        Name = name;
        Adress = adress;
        ZipCode = zipCode;
        City = city;
        Phone = phone;
        EMail = eMail;
    }

    public static void SaveToFile(
        string Name,
        string Adress,
        string ZipCode,
        string City,
        string Phone,
        string EMail
    )
    {
        var contactItem = new
        {
            Name,
            Adress,
            ZipCode,
            City,
            Phone,
            EMail,
        };
        string fileName = "Contacts.json";
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
        };
        string json = JsonSerializer.Serialize(contactItem, options);

        //Console.WriteLine(json);
        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine(json);
        }
    }
}
