using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace kontaktbok;

public class Contact : INotifyPropertyChanged
{
    private string name;
    private string adress;
    private string zipCode;
    private string city;
    private string phone;
    private string eMail;

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
