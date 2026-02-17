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

    private void Find_OnClick(object? sender, RoutedEventArgs e)
    {
        if (String.IsNullOrWhiteSpace(FuncOne_x.Text) || String.IsNullOrWhiteSpace(FuncOne_y.Text) || String.IsNullOrWhiteSpace(FuncOne_z.Text))
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;    var 
                dialogg= new SimplePage("Заполните все поля");    
                dialogg.ShowDialog(mainWindow);
        }
        else
        {
            if (decimal.TryParse(FuncOne_x.Text, out decimal x) && decimal.TryParse(FuncOne_y.Text, out decimal y) && decimal.TryParse(FuncOne_z.Text, out decimal z))
            {
                if (x == 0 || y - x == 0)
                {
                    var mainWindow = (Application.Current.ApplicationLifetime as  IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                    var dilogg = new SimplePage("Деление на 0");
                    dilogg.ShowDialog(mainWindow);
                    return;
                }
                decimal part1 = (decimal)Math.Abs(Math.Pow((double)x,(double)y) - Math.Pow((double)y/(double)x,1.0/3.0));
                decimal part2 = (y - x) * (decimal)((Math.Cos((double)y) - ((double)z / ((double)y - (double)x))) / (1 + Math.Pow((double)y - (double)x, 2)));
                decimal result = part1 + part2;
                FuncOne_result.Text = result.ToString();
            }
            else
            {
                var mainWindow = (Application.Current.ApplicationLifetime
                    as IClassicDesktopStyleApplicationLifetime)?.MainWindow;    var 
                    dialogg= new SimplePage("Введены неверные данные");    
                    dialogg.ShowDialog(mainWindow);
            }
        }
    }

    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        if (String.IsNullOrWhiteSpace(FuncOne_x.Text) || String.IsNullOrWhiteSpace(FuncOne_y.Text) || String.IsNullOrWhiteSpace(FuncOne_z.Text))
        {
            
        }
        else
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