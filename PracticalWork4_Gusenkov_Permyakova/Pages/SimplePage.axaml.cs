using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

public partial class SimplePage : Window
{
    public SimplePage(string message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }

    private void Ok_Click (object? sender, RoutedEventArgs e)
    {
        Close();
    }
}