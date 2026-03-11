using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using WpfLikeAvaloniaNavigation;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;

namespace PracticalWork4_Gusenkov_Permyakova.Pages;

/// <summary>
/// Содержит чистую математическую логику вычисления функции FuncThree,
/// изолированную от UI. Используется как в <see cref="FuncThreePage"/>,
/// так и в unit-тестах.
/// </summary>
public static class FuncThreeCalculator
{
    /// <summary>
    /// Вычисляет таблицу значений функции y = 0.0025·b·x³ + √x + e^0.82
    /// </summary>
    public static bool TryCalculate(
        double b, double x0, double xk, double dx,
        out List<double> xValues, out List<double> yValues)
    {
        xValues = new List<double>();
        yValues = new List<double>();

        if (dx <= 0)
            return false;

        if (x0 > xk)
            return false;

        for (double x = x0; x <= xk + 1e-10; x += dx)
        {
            if (x < 0) continue;

            double y = 0.0025 * b * Math.Pow(x, 3) + Math.Sqrt(x) + Math.Pow(Math.E, 0.82);
            xValues.Add(x);
            yValues.Add(y);
        }

        return true;
    }
}


public partial class FuncThreePage : Page
{
    /// <summary>Коллекция серий для графика LiveCharts.</summary>
    public ObservableCollection<ISeries> Series { get; set; }

    /// <summary>Настройки оси X графика.</summary>
    public Axis[] XAxes { get; set; }

    /// <summary>Настройки оси Y графика.</summary>
    public Axis[] YAxes { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр страницы <see cref="FuncThreePage"/>.
    /// </summary>
    public FuncThreePage()
    {
        InitializeComponent();
        InitializeChart();
        DataContext = this;
    }

    /// <summary>
    /// Инициализирует оси и коллекцию серий графика.
    /// </summary>
    private void InitializeChart()
    {
        Series = new ObservableCollection<ISeries>();

        XAxes = new Axis[]
        {
            new Axis
            {
                Name = "x",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.LightGray),
                TextSize = 14,
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)) { StrokeThickness = 1 }
            }
        };

        YAxes = new Axis[]
        {
            new Axis
            {
                Name = "y",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.LightGray),
                TextSize = 14,
                SeparatorsPaint = new SolidColorPaint(new SKColor(100, 100, 100)) { StrokeThickness = 1 }
            }
        };
    }

    /// <summary>
    /// Вычисляет таблицу значений y = 0.0025·b·x³ + √x + e^0.82
    /// для x ∈ [x0; xk] с шагом dx и отображает результат в UI.
    /// Делегирует математику в <see cref="FuncThreeCalculator.TryCalculate"/>.
    /// </summary>
    public bool Calculate(double b, double x0, double xk, double dx)
    {
        if (dx <= 0)
        {
            ShowDialog("dx должен быть больше 0");
            return false;
        }

        if (x0 > xk)
        {
            ShowDialog("x₀ должен быть меньше или равен xₖ");
            return false;
        }

        FuncThreeCalculator.TryCalculate(b, x0, xk, dx, out var xValues, out var yValues);

        StringBuilder results = new StringBuilder();
        results.AppendLine("╔════════════╦════════════════╗");
        results.AppendLine("║     x      ║       y        ║");
        results.AppendLine("╠════════════╬════════════════╣");

        int idx = 0;
        for (double x = x0; x <= xk + 1e-10; x += dx)
        {
            if (x < 0)
            {
                results.AppendLine($"║ {x,10:F2} ║ Не определено  ║");
                continue;
            }
            results.AppendLine($"║ {x,10:F2} ║ {yValues[idx],14:F6} ║");
            idx++;
        }

        results.AppendLine("╚════════════╩════════════════╝");
        results.AppendLine();
        results.AppendLine($"Формула: y = 0.0025 * {b} * x³ + √x + e^0.82");
        results.AppendLine($"Диапазон: x ∈ [{x0}; {xk}], шаг = {dx}");

        FuncThree_result.Text = results.ToString();
        UpdateChart(xValues, yValues);

        return true;
    }

    /// <summary>
    /// Отображает модальный диалог с указанным сообщением поверх главного окна.
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
    /// Считывает параметры из полей и вызывает <see cref="Calculate"/>.
    /// </summary>
    private void Find_OnClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FuncThree_b.Text) ||
            string.IsNullOrWhiteSpace(FuncThree_x0.Text) ||
            string.IsNullOrWhiteSpace(FuncThree_xk.Text) ||
            string.IsNullOrWhiteSpace(FuncThree_dx.Text))
        {
            ShowDialog("Заполните все поля");
            return;
        }

        if (!double.TryParse(FuncThree_b.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double b) ||
            !double.TryParse(FuncThree_x0.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double x0) ||
            !double.TryParse(FuncThree_xk.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double xk) ||
            !double.TryParse(FuncThree_dx.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double dx))
        {
            ShowDialog("Введены неверные данные");
            return;
        }

        Calculate(b, x0, xk, dx);
    }

    /// <summary>
    /// Обновляет данные графика новыми точками.
    /// </summary>
    private void UpdateChart(List<double> xValues, List<double> yValues)
    {
        Series.Clear();
        if (xValues.Count == 0 || yValues.Count == 0) return;

        var values = new List<ObservablePoint>();
        for (int i = 0; i < xValues.Count; i++)
            values.Add(new ObservablePoint(xValues[i], yValues[i]));

        Series.Add(new LineSeries<ObservablePoint>
        {
            Values = values,
            Name = "y = 0.0025bx³ + √x + e^0.82",
            Stroke = new SolidColorPaint(SKColors.Cyan) { StrokeThickness = 3 },
            Fill = null,
            GeometrySize = 8,
            GeometryStroke = new SolidColorPaint(SKColors.Cyan) { StrokeThickness = 3 },
            GeometryFill = new SolidColorPaint(SKColors.DarkCyan)
        });
    }

    /// <summary>
    /// Обработчик нажатия кнопки «Очистить».
    /// Сбрасывает поля к значениям по умолчанию и очищает график.
    /// </summary>
    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        FuncThree_b.Text = "2.3";
        FuncThree_x0.Text = "-1";
        FuncThree_xk.Text = "4";
        FuncThree_dx.Text = "0.5";
        FuncThree_result.Text = "";
        Series.Clear();
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