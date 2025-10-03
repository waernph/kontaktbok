using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.VisualTree;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace kontaktbok;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

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

        Contacts.Add(new Contact("John Doe", "Street 1", "123 45", "Awesome Town", "123 456 789", "john.doe@gmail.com"));
        Contacts.Add(new Contact("John Doe", "Street 2", "123 45", "Awesome Town", "123 456 789", "john.doe@gmail.com"));

        DataContext = this;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}