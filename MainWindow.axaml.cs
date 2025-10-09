using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;

namespace kontaktbok;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();
    public ObservableCollection<Contact> Showcase { get; set; } =
        new ObservableCollection<Contact>();

    private Contact? selectedContact;
    public Contact? SelectedContact
    {
        get => selectedContact;
        set
        {
            if (selectedContact != value)
            {
                selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }
    }

    public MainWindow()
    {
        InitializeComponent();
        Contacts = Contact.LoadAllContacts();
        UpdateList();
        DataContext = this;
    }

    public virtual void SaveOnExit()
    {
        Contact.SortContactList(Contacts);
    }

    private void DeleteOnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) //Vasiliki
    {
        if ((sender as Button)?.CommandParameter is Contact contact)
        {
            Contacts.Remove(contact);
            UpdateList();
            Contact.SaveToFile(Contacts);
        }
    }

    private void AddOnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Contacts.Insert(0, new Contact("New Contact", "", "", "", "", ""));
        UpdateList();
        Contact.SaveToFile(Contacts);
    }

    private void SaveOnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Contact.SaveToFile(Contacts);
    }

    private void SearchOnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UpdateList();
    }

    private void ResetOnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SearchField.Text = "";
        UpdateList();
    }

    private void UpdateList()
    {
        Showcase = Contact.SearchContact(Contacts, SearchField.Text);
        OnPropertyChanged(nameof(Showcase));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
