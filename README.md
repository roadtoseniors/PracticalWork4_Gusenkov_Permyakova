# Практическая работа 6.2

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Avalonia](https://img.shields.io/badge/Avalonia-6A1B9A?style=for-the-badge&logo=avalonia&logoColor=white)

Практическая работа №6.2 по дисциплине **"Поддержка и тестирование программного обеспечения"**

**Авторы:** Гусенков В.А., Пермякова М.И.  
**Группа:** 3ИСИП-323  
**Преподаватель:** Аксенова Т.Г.   
**Учебное заведение:** КИПФИН

---

## Описание проекта

Проект `PracticalWork4Testing` содержит unit-тесты для математической логики трёх страниц приложения. Тестирование реализовано с использованием **MSTest** (`Microsoft.VisualStudio.TestTools.UnitTesting`).

Каждая страница имеет соответствующий `Calculator`-класс, изолирующий вычисления от UI Avalonia, что позволяет запускать тесты без инициализации графического интерфейса.

---

## FuncOneTesting

### Описание функции

```
result = |x^y - (y/x)^(1/3)| + (y - x) * (cos(y) - z/(y-x)) / (1 + (y-x)^2)
```

Возвращает `false` если `x = 0` или `y = x` (деление на ноль).

### Тесты

| № | Название теста | Тип | Ожидаемый результат |
|---|---------------|-----|---------------------|
| 1 | `Calculate_ValidInputs_ReturnsTrue` | ✅ Позитивный | Метод возвращает `true` при x=2, y=3, z=1 |
| 2 | `Calculate_ValidInputs_ResultMatchesExpected` | ✅ Позитивный | Результат совпадает с эталоном с точностью 0.0001 |
| 3 | `Calculate_XIsZero_ReturnsFalse` | ❌ Негативный | Метод возвращает `false` при x=0 |

---

## FuncTwoTesting

### Описание функции

Вычисляет значение `l` по условию:

- `x > |p|` → `l = 2·fx³ + 3·p²`
- `x ≈ |p|` (точность 0.0001) → `l = (fx - p)²`
- `3 < x < |p|` → `l = |fx - p|`
- иначе → возвращает `false`

### Тесты

| № | Название теста | Тип | Ожидаемый результат |
|---|---------------|-----|---------------------|
| 1 | `Calculate_XGreaterThanAbsP_ReturnsCorrectResult` | ✅ Позитивный | x=10, p=3, fx=2 → l = 2·8 + 3·9 = **43** |
| 2 | `Calculate_XBetween3AndAbsP_ReturnsAbsDiff` | ✅ Позитивный | x=4, p=10, fx=7 → l = \|7-10\| = **3** |
| 3 | `Calculate_XNotMatchingAnyCondition_ReturnsFalse` | ❌ Негативный | x=1, p=5 → возвращает `false` |

---

## FuncThreeTesting

### Описание функции

Строит таблицу значений для диапазона x ∈ [x0; xk] с шагом dx:

```
y = 0.0025 * b * x³ + √x + e^0.82
```

Точки с `x < 0` пропускаются. Возвращает `false` если `dx ≤ 0` или `x0 > xk`.

### Тесты

| № | Название теста | Тип | Ожидаемый результат |
|---|---------------|-----|---------------------|
| 1 | `Calculate_ValidInputs_ReturnsCorrectYValue` | ✅ Позитивный | x=1, b=2 → y совпадает с формулой с точностью 0.000001 |
| 2 | `Calculate_NegativeXSkipped_OnlyNonNegativePointsReturned` | ✅ Позитивный | x0=-1, xk=1, dx=1 → 2 точки: x=0 и x=1 |
| 3 | `Calculate_DxIsZero_ReturnsFalse` | ❌ Негативный | dx=0 → возвращает `false` |

---

## Авторы

| Имя | Роль | Контакты |
|-----|------|----------|
| **Гусенков Вадим Алексеевич** | Разработчик | [@voodeex](https://github.com/voodeex) |
| **Пермякова Мария Ивановна** | Разработчик | [@roadtoseniors](https://github.com/roadtoseniors) |

---

## Контакты

По вопросам работы проекта обращайтесь к авторам или преподавателю.

**GitHub Repository:** [PracticalWork4_Gusenkov_Permyakova](https://github.com/roadtoseniors/PracticalWork4_Gusenkov_Permyakova)

---

<div align="center">
</div>
