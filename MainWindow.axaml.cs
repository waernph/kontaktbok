using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace kontaktbok;

public partial class MainWindow : Window
{
    public List<Contact> Contacts { get; } = new List<Contact>();
    public MainWindow()
    {
        InitializeComponent();

        Contacts.Add(new Contact("John Doe", "Street 1", "123 45", "Awesome Town", "123 456 789", "john.doe@gmail.com"));
        Contacts.Add(new Contact("John Doe", "Street 2", "123 45", "Awesome Town", "123 456 789", "john.doe@gmail.com"));

        DataContext = this;
    }
}