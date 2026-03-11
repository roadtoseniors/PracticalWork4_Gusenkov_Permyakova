using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

public static class FuncOneCalculator
{
    /// <summary>
    /// Вычисляет значение функции
    /// </summary>
    public static bool TryCalculate(decimal x, decimal y, decimal z, out decimal result)
    {
        result = 0;

        if (x == 0 || y - x == 0)
            return false;

        decimal part1 = (decimal)Math.Abs(
            Math.Pow((double)x, (double)y) -
            Math.Pow((double)y / (double)x, 1.0 / 3.0));

        decimal part2 = (y - x) * (decimal)(
            (Math.Cos((double)y) - ((double)z / ((double)y - (double)x))) /
            (1 + Math.Pow((double)y - (double)x, 2)));

        result = part1 + part2;
        return true;
    }
}

public partial class FuncOnePage : Page
{
    
    public FuncOnePage()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// Вычисляет значение функции по формуле
    /// </summary>
    
    public bool Calculate(decimal x, decimal y, decimal z)
    {
        if (!FuncOneCalculator.TryCalculate(x, y, z, out decimal result))
        {
            ShowDialog("Деление на 0");
            return false;
        }
        FuncOne_result.Text = result.ToString();
        LastResult = result;
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
    /// Считывает значения x, y, z из текстовых полей, проверяет их корректность
    /// и вызывает <see cref="Calculate"/>.
    /// Показывает диалог с ошибкой, если поля пусты или содержат не числовые данные.
    /// </summary>

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
        
    /// <summary>
    /// Обработчик нажатия кнопки «Очистить».
    /// Сбрасывает содержимое полей ввода x, y, z,
    /// если хотя бы одно из них не пусто.
    /// </summary>

    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(FuncOne_x.Text) || !string.IsNullOrWhiteSpace(FuncOne_y.Text) || !string.IsNullOrWhiteSpace(FuncOne_z.Text))
        {
            FuncOne_x.Clear();
            FuncOne_y.Clear();
            FuncOne_z.Clear();
        }
    }

    /// <summary>
    /// Обработчик нажатия кнопки «Назад».
    /// Выполняет навигацию на страницу <see cref="NavigationPage"/>.
    /// </summary>
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new NavigationPage());
    }
        
    /// <summary>
    /// Последний вычисленный результат. Используется для тестирования.
    /// </summary>
    public decimal LastResult { get; private set; }    
}