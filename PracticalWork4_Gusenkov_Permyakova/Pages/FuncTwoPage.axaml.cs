using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using WpfLikeAvaloniaNavigation;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

public partial class FuncTwoPage : Page
{
    public FuncTwoPage()
    {
        InitializeComponent();
    }

    private void Find_OnClick(object? sender, RoutedEventArgs e)
    {
        if (String.IsNullOrWhiteSpace(FuncTwo_x.Text) || String.IsNullOrWhiteSpace(FuncTwo_p.Text) || String.IsNullOrWhiteSpace(FuncTwo_fx.Text))
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg = new SimplePage("Заполните все поля");
            dialogg.ShowDialog(mainWindow);
        }
        else
        {
            if (double.TryParse(FuncTwo_x.Text, out double x) && 
                double.TryParse(FuncTwo_p.Text, out double p) && 
                double.TryParse(FuncTwo_fx.Text, out double fx))
            {
                double l;
                double absP = Math.Abs(p);
                
                // Формула из изображения:
                // l = { 2f(x)³ + 3p², x > |p|
                //     { |f(x) - p|, 3 < x < |p|
                //     { (f(x) - p)², x = |p|
                
                if (x > absP)
                {
                    // 2f(x)³ + 3p²
                    l = 2 * Math.Pow(fx, 3) + 3 * Math.Pow(p, 2);
                }
                else if (Math.Abs(x - absP) < 0.0001) // x = |p|
                {
                    // (f(x) - p)²
                    l = Math.Pow(fx - p, 2);
                }
                else if (x > 3 && x < absP)
                {
                    // |f(x) - p|
                    l = Math.Abs(fx - p);
                }
                else
                {
                    var mainWindow = (Application.Current.ApplicationLifetime 
                        as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                    var dilogg = new SimplePage("x не попадает ни в одно условие (x должен быть > 3)");
                    dilogg.ShowDialog(mainWindow);
                    return;
                }
                
                FuncTwo_result.Text = l.ToString("F6");
            }
            else
            {
                var mainWindow = (Application.Current.ApplicationLifetime
                    as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                var dialogg = new SimplePage("Введены неверные данные");
                dialogg.ShowDialog(mainWindow);
            }
        }
    }

    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        FuncTwo_x.Clear();
        FuncTwo_p.Clear();
        FuncTwo_fx.Clear();
        FuncTwo_result.Text = "";
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new NavigationPage());
    }
}
