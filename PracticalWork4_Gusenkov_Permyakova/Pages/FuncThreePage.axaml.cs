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

public partial class FuncThreePage : Page
{
    public ObservableCollection<ISeries> Series { get; set; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }

    public FuncThreePage()
    {
        InitializeComponent();
        InitializeChart();
        DataContext = this;
    }

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

        StringBuilder results = new StringBuilder();
        results.AppendLine("╔════════════╦════════════════╗");
        results.AppendLine("║     x      ║       y        ║");
        results.AppendLine("╠════════════╬════════════════╣");

        List<double> xValues = new List<double>();
        List<double> yValues = new List<double>();

        for (double x = x0; x <= xk; x += dx)
        {
            if (x < 0)
            {
                results.AppendLine($"║ {x,10:F2} ║ Не определено  ║");
                continue;
            }

            double y = 0.0025 * b * Math.Pow(x, 3) + Math.Sqrt(x) + Math.Pow(Math.E, 0.82);
            results.AppendLine($"║ {x,10:F2} ║ {y,14:F6} ║");

            xValues.Add(x);
            yValues.Add(y);
        }

        results.AppendLine("╚════════════╩════════════════╝");
        results.AppendLine();
        results.AppendLine($"Формула: y = 0.0025 * {b} * x³ + √x + e^0.82");
        results.AppendLine($"Диапазон: x ∈ [{x0}; {xk}], шаг = {dx}");

        FuncThree_result.Text = results.ToString();
        UpdateChart(xValues, yValues);

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

    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        FuncThree_b.Text = "2.3";
        FuncThree_x0.Text = "-1";
        FuncThree_xk.Text = "4";
        FuncThree_dx.Text = "0.5";
        FuncThree_result.Text = "";
        Series.Clear();
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new NavigationPage());
    }
}