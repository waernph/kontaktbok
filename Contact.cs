//using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

//using Tmds.DBus.Protocol;

namespace kontaktbok;

public class Contact : INotifyPropertyChanged
{
    private string name;
    private string adress;
    private string zipCode;
    private string city;
    private string phone;
    private string eMail;
    private readonly string filePath = "Contacts.json";

    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public string Adress
    {
        get => adress;
        set
        {
            if (adress != value)
            {
                adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }
    }

    public string ZipCode
    {
        get => zipCode;
        set
        {
            if (zipCode != value)
            {
                zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }
    }

    public string City
    {
        get => city;
        set
        {
            if (city != value)
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
    }

    public string Phone
    {
        get => phone;
        set
        {
            if (phone != value)
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
    }

    public string EMail
    {
        get => eMail;
        set
        {
            if (eMail != value)
            {
                eMail = value;
                OnPropertyChanged(nameof(EMail));
            }
        }
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static void SaveToFile(ObservableCollection<Contact> contactItem)
    {
        /* var contactItem = new
        {
            Name,
            Adress,
            ZipCode,
            City,
            Phone,
            EMail,
        }; */

        string fileName = "Contacts.json";
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        };
        string json = JsonSerializer.Serialize(contactItem, options);

        using (StreamWriter writer = new StreamWriter(fileName, false))
        {
            writer.WriteLine(json);
        }
    }

    public static ObservableCollection<Contact> SearchContact(string userInput)
    {
        var loadedContactList = LoadAllContacts();
        ObservableCollection<Contact> searchResult = new() { };
        if (userInput == null || userInput == "")
        {
            return loadedContactList;
        }
        var result = (
            from contact in loadedContactList
            where
                contact.Name.Contains(userInput)
                || contact.Adress.Contains(userInput)
                || contact.ZipCode.Contains(userInput)
                || contact.City.Contains(userInput)
                || contact.Phone.Contains(userInput)
                || contact.EMail.Contains(userInput)
            select contact
        ).ToList();

        foreach (var contact in result)
        {
            searchResult.Add(contact);
        }
        return searchResult;
    }

    public static ObservableCollection<Contact> LoadAllContacts() //Metod för att ladda alla kontakter från Contacts.json
    {
        var contractsFileExists = File.Exists("Contacts.json");
        if (!contractsFileExists)
        {
            // Create Contacts file.
        }

        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            IncludeFields = true,
            AllowTrailingCommas = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        };

        string contactJsonData = File.ReadAllText("Contacts.json");

        ObservableCollection<Contact>? contactData = JsonSerializer.Deserialize<
            ObservableCollection<Contact>
        >(contactJsonData, options);

        /* if (contactData == null)
        {
            throw new Exception("Unhandled error - when deserializing file"); //Hantera med try catch i program
        } */

        return contactData;
    }

    public static void SaveOnExit(ObservableCollection<Contact> updatedContactList)
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            IncludeFields = true,
            AllowTrailingCommas = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        };
        string contactJsonData = JsonSerializer.Serialize(updatedContactList, options);
        File.WriteAllTextAsync("Contacts.json", contactJsonData);
    }
}
