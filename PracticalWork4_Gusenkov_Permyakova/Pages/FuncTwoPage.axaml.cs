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

    public bool Calculate(double x, double p, double fx)
    {
        double absP = Math.Abs(p);
        double l;

        if (x > absP)
        {
            l = 2 * Math.Pow(fx, 3) + 3 * Math.Pow(p, 2);
        }
        else if (Math.Abs(x - absP) < 0.0001)
        {
            l = Math.Pow(fx - p, 2);
        }
        else if (x > 3 && x < absP)
        {
            l = Math.Abs(fx - p);
        }
        else
        {
            ShowDialog("x не попадает ни в одно условие (x должен быть > 3)");
            return false;
        }

        FuncTwo_result.Text = l.ToString("F6");
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
        if (string.IsNullOrWhiteSpace(FuncTwo_x.Text) ||
            string.IsNullOrWhiteSpace(FuncTwo_p.Text) ||
            string.IsNullOrWhiteSpace(FuncTwo_fx.Text))
        {
            ShowDialog("Заполните все поля");
            return;
        }

        if (double.TryParse(FuncTwo_x.Text, out double x) &&
            double.TryParse(FuncTwo_p.Text, out double p) &&
            double.TryParse(FuncTwo_fx.Text, out double fx))
        {
            Calculate(x, p, fx);
        }
        else
        {
            ShowDialog("Введены неверные данные");
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
