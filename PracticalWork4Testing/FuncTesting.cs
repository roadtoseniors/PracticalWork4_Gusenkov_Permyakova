using System;
using PracticalWork4_Gusenkov_Permyakova.Pages;

namespace PracticalWork4Testing;

[TestClass]
public sealed class FuncOneTesting
{
    /// <summary>
    /// Положительный тест 1.
    /// Проверяет, что при корректных значениях x=2, y=3, z=1
    /// метод возвращает true.
    /// </summary>
    [TestMethod]
    public void Calculate_ValidInputs_ReturnsTrue()
    {
        // Act
        bool success = FuncOneCalculator.TryCalculate(2m, 3m, 1m, out _);

        // Assert
        Assert.IsTrue(success, "TryCalculate должен вернуть true для корректных входных данных.");
    }

    /// <summary>
    /// Положительный тест 2.
    /// Проверяет точность вычисления при x=2, y=3, z=1 —
    /// результат должен совпадать с эталоном с погрешностью не более 0.0001.
    /// </summary>
    [TestMethod]
    public void Calculate_ValidInputs_ResultMatchesExpected()
    {
        // Arrange
        double p1 = Math.Abs(Math.Pow(2, 3) - Math.Pow(3.0 / 2.0, 1.0 / 3.0));
        double p2 = (3 - 2) * ((Math.Cos(3) - 1.0 / (3 - 2)) / (1 + Math.Pow(3 - 2, 2)));
        decimal expected = (decimal)(p1 + p2);

        // Act
        FuncOneCalculator.TryCalculate(2m, 3m, 1m, out decimal actual);

        // Assert
        Assert.AreEqual(expected, actual, 0.0001m, "Результат не совпадает с ожидаемым значением.");
    }

    /// <summary>
    /// Отрицательный тест.
    /// Проверяет, что при x=0 метод возвращает false (деление на ноль).
    /// </summary>
    [TestMethod]
    public void Calculate_XIsZero_ReturnsFalse()
    {
        // Act
        bool success = FuncOneCalculator.TryCalculate(0m, 5m, 3m, out _);

        // Assert
        Assert.IsFalse(success, "TryCalculate должен вернуть false, когда x равен нулю.");
    }
}

[TestClass]
public sealed class FuncTwoTesting
{
    /// <summary>
    /// Положительный тест 1.
    /// </summary>
    [TestMethod]
    public void Calculate_XGreaterThanAbsP_ReturnsCorrectResult()
    {
        // Arrange
        double x = 10, p = 3, fx = 2;
        double expected = 2 * Math.Pow(fx, 3) + 3 * Math.Pow(p, 2);

        // Act
        bool success = FuncTwoCalculator.TryCalculate(x, p, fx, out double result);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(expected, result, 0.000001, "Результат для ветки x > |p| не совпадает.");
    }

    /// <summary>
    /// Положительный тест 2.
    /// </summary>
    [TestMethod]
    public void Calculate_XBetween3AndAbsP_ReturnsAbsDiff()
    {
        // Arrange
        double x = 4, p = 10, fx = 7;
        double expected = Math.Abs(fx - p); // = 3

        // Act
        bool success = FuncTwoCalculator.TryCalculate(x, p, fx, out double result);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(expected, result, 0.000001, "Результат для ветки 3 < x < |p| не совпадает.");
    }

    /// <summary>
    /// Отрицательный тест.
    /// Проверяет, что при x=1 (не больше 3 и не равен |p|)
    /// метод возвращает false.
    /// </summary>
    [TestMethod]
    public void Calculate_XNotMatchingAnyCondition_ReturnsFalse()
    {
        // Arrange
        double x = 1, p = 5, fx = 2;

        // Act
        bool success = FuncTwoCalculator.TryCalculate(x, p, fx, out _);

        // Assert
        Assert.IsFalse(success, "TryCalculate должен вернуть false, когда x не попадает ни в одно условие.");
    }
}

[TestClass]
public sealed class FuncThreeTesting
{
    [TestMethod]
    public void Calculate_ValidInputs_ReturnsCorrectYValue()
    {
        // Arrange
        double b = 2, x0 = 1, xk = 1, dx = 1;
        double expectedY = 0.0025 * b * Math.Pow(1, 3) + Math.Sqrt(1) + Math.Pow(Math.E, 0.82);

        // Act
        bool success = FuncThreeCalculator.TryCalculate(b, x0, xk, dx, out var xValues, out var yValues);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(1, yValues.Count, "Должна быть ровно одна точка.");
        Assert.AreEqual(expectedY, yValues[0], 0.000001, "Значение y не совпадает с ожидаемым.");
    }
    
    [TestMethod]
    public void Calculate_NegativeXSkipped_OnlyNonNegativePointsReturned()
    {
        // Arrange
        double b = 1, x0 = -1, xk = 1, dx = 1;

        // Act
        bool success = FuncThreeCalculator.TryCalculate(b, x0, xk, dx, out var xValues, out var yValues);

        // Assert
        Assert.IsTrue(success);
        Assert.AreEqual(2, xValues.Count, "Должны быть только точки x=0 и x=1.");
        Assert.AreEqual(0.0, xValues[0], 0.000001);
        Assert.AreEqual(1.0, xValues[1], 0.000001);
    }
    
    [TestMethod]
    public void Calculate_DxIsZero_ReturnsFalse()
    {
        // Arrange
        double b = 2, x0 = 0, xk = 5, dx = 0;

        // Act
        bool success = FuncThreeCalculator.TryCalculate(b, x0, xk, dx, out _, out _);

        // Assert
        Assert.IsFalse(success, "TryCalculate должен вернуть false, когда dx равен нулю.");
    }
}

