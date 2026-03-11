using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using WpfLikeAvaloniaNavigation;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

/// <summary>
/// Содержит чистую математическую логику вычисления функции FuncTwo,
/// изолированную от UI. Используется как в <see cref="FuncTwoPage"/>,
/// так и в unit-тестах.
/// </summary>
public static class FuncTwoCalculator
{
    /// <summary>
    /// Вычисляет значение функции l по условиям
    /// </summary>
    public static bool TryCalculate(double x, double p, double fx, out double result)
    {
        result = 0;
        double absP = Math.Abs(p);

        if (x > absP)
        {
            result = 2 * Math.Pow(fx, 3) + 3 * Math.Pow(p, 2);
        }
        else if (Math.Abs(x - absP) < 0.0001)
        {
            result = Math.Pow(fx - p, 2);
        }
        else if (x > 3 && x < absP)
        {
            result = Math.Abs(fx - p);
        }
        else
        {
            return false;
        }

        return true;
    }
}


public partial class FuncTwoPage : Page
{
    /// <summary>
    /// Инициализирует новый экземпляр страницы <see cref="FuncTwoPage"/>.
    /// </summary>
    public FuncTwoPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Вычисляет значение функции l по трём аргументам и отображает результат в UI.
    /// </summary>
    public bool Calculate(double x, double p, double fx)
    {
        if (!FuncTwoCalculator.TryCalculate(x, p, fx, out double result))
        {
            ShowDialog("x не попадает ни в одно условие (x должен быть > 3)");
            return false;
        }

        FuncTwo_result.Text = result.ToString("F6");
        return true;
    }

    /// <summary>
    /// Отображает модальный диалог с указанным сообщением поверх главного окна приложения.
    /// </summary>
    private void ShowDialog(string message)
    {
        var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        var dialog = new SimplePage(message);
        dialog.ShowDialog(mainWindow);
    }

    /// <summary>
    /// Обработчик нажатия кнопки «Найти».
    /// Считывает x, p, fx из текстовых полей и вызывает <see cref="Calculate"/>.
    /// </summary>
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

    /// <summary>
    /// Обработчик нажатия кнопки «Очистить».
    /// Сбрасывает содержимое всех полей ввода и результата.
    /// </summary>
    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        FuncTwo_x.Clear();
        FuncTwo_p.Clear();
        FuncTwo_fx.Clear();
        FuncTwo_result.Text = "";
    }

    /// <summary>
    /// Обработчик нажатия кнопки «Назад».
    /// Выполняет навигацию на страницу <see cref="NavigationPage"/>.
    /// </summary>
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        this.NavigationService.Navigate(new NavigationPage());
    }
}
