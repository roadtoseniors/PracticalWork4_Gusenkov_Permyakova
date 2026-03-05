using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

public partial class FuncOnePage : Page
{
    
    public FuncOnePage()
    {
        InitializeComponent();
    }

    public bool Calculate(decimal x, decimal y, decimal z)
    {
        if (x == 0 || y - x == 0)
        {
            ShowDialog("Деление на 0");
            return false;
        }
        decimal part1 = (decimal)Math.Abs(
            Math.Pow((double)x, (double)y) -
            Math.Pow((double)y / (double)x, 1.0 / 3.0));

        decimal part2 = (y - x) * (decimal)(
            (Math.Cos((double)y) - ((double)z / ((double)y - (double)x))) /
            (1 + Math.Pow((double)y - (double)x, 2)));

        decimal result = part1 + part2;
        FuncOne_result.Text = result.ToString();

        return true;
    }
    
    private void ShowDialog(string message)
    {
        var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        var dialog = new SimplePage(message);
        dialog.ShowDialog(mainWindow);
    }

    private void Find_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FuncOne_x.Text) ||
            string.IsNullOrWhiteSpace(FuncOne_y.Text) ||
            string.IsNullOrWhiteSpace(FuncOne_z.Text))
        {
            ShowDialog("Заполните все поля");
            return;
        }

        if (decimal.TryParse(FuncOne_x.Text, out decimal x) &&
            decimal.TryParse(FuncOne_y.Text, out decimal y) &&
            decimal.TryParse(FuncOne_z.Text, out decimal z))
        {
            Calculate(x, y, z);
        }
        else
        {
            ShowDialog("Введены неверные данные");
        }
    }
    
    

    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(FuncOne_x.Text) || !string.IsNullOrWhiteSpace(FuncOne_y.Text) || !string.IsNullOrWhiteSpace(FuncOne_z.Text))
        {
            FuncOne_x.Clear();
            FuncOne_y.Clear();
            FuncOne_z.Clear();
        }
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new NavigationPage());
    }
    
}