using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

public partial class NavigationPage : Page
{
    public NavigationPage()
    {
        InitializeComponent();
    }

    private void FuncOne_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new FuncOnePage());
    }

    private void FuncTwo_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new FuncTwoPage());
    }

    private void FuncThree_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new FuncThreePage());
    }
}