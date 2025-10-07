using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace kontaktbok;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();
    public ObservableCollection<Contact> Showcase { get; set; } = new ObservableCollection<Contact>();

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

        Contact.SaveToFile(Contacts);

        DataContext = this;
    }

    private void DeleteOnClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
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
        Contacts.Add(new Contact("New Contact", "", "", "", "", ""));
        UpdateList();
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