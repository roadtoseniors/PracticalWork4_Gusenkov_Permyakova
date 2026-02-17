using Avalonia.Controls;
using PracticalWork4_Gusenkov_Permyakova.Pages;

namespace PracticalWork4_Gusenkov_Permyakova;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Frame.Navigate(new NavigationPage());
    }
}