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
    
    [TestMethod]
    
    [TestMethod]
}

