using System;

class DRomb
{
    // Поля
    protected int d1;  // Перша діагональ
    protected int d2;  // Друга діагональ
    protected int color;  // Колір ромба

    // Конструктор
    public DRomb(int d1, int d2, int color)
    {
        this.d1 = d1 > 0 ? d1 : throw new ArgumentException("Діагональ d1 має бути більше 0");
        this.d2 = d2 > 0 ? d2 : throw new ArgumentException("Діагональ d2 має бути більше 0");
        this.color = color;
    }

    // Індексатор
    public object this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return d1;
                case 1: return d2;
                case 2: return color;
                default: throw new IndexOutOfRangeException("Неприпустимий індекс. Дозволені значення: 0 (d1), 1 (d2), 2 (color).");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    d1 = (int)value > 0 ? (int)value : throw new ArgumentException("Діагональ d1 має бути більше 0");
                    break;
                case 1:
                    d2 = (int)value > 0 ? (int)value : throw new ArgumentException("Діагональ d2 має бути більше 0");
                    break;
                case 2:
                    color = (int)value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Неприпустимий індекс. Дозволені значення: 0 (d1), 1 (d2), 2 (color).");
            }
        }
    }

    // Методи
    public void PrintDimensions()
    {
        Console.WriteLine($"Діагоналі ромба: d1 = {d1}, d2 = {d2}");
    }

    public double CalculatePerimeter()
    {
        double a = Math.Sqrt(Math.Pow(d1 / 2.0, 2) + Math.Pow(d2 / 2.0, 2));
        return 4 * a;
    }

    public double CalculateArea()
    {
        return (d1 * d2) / 2.0;
    }

    public bool IsSquare()
    {
        return d1 == d2;
    }

    // Властивості
    public int D1
    {
        get { return d1; }
        set { d1 = value > 0 ? value : throw new ArgumentException("Діагональ d1 має бути більше 0"); }
    }

    public int D2
    {
        get { return d2; }
        set { d2 = value > 0 ? value : throw new ArgumentException("Діагональ d2 має бути більше 0"); }
    }

    public int Color
    {
        get { return color; }
        set { color = value; }
    }

    // Перевантаження операторів
    public static DRomb operator ++(DRomb rhomb)
    {
        rhomb.d1++;
        rhomb.d2++;
        return rhomb;
    }

    public static DRomb operator --(DRomb rhomb)
    {
        if (rhomb.d1 <= 1 || rhomb.d2 <= 1)
            throw new InvalidOperationException("Діагоналі не можуть бути зменшені до 0 або від'ємних значень");
        rhomb.d1--;
        rhomb.d2--;
        return rhomb;
    }

    public static bool operator true(DRomb rhomb) => rhomb.IsSquare();
    public static bool operator false(DRomb rhomb) => !rhomb.IsSquare();

    public static DRomb operator +(DRomb rhomb, int scalar)
    {
        if (scalar < 0 && (rhomb.d1 + scalar <= 0 || rhomb.d2 + scalar <= 0))
            throw new ArgumentException("Скаляр не може призвести до від'ємних або нульових діагоналей");
        return new DRomb(rhomb.d1 + scalar, rhomb.d2 + scalar, rhomb.color);
    }

    // Перетворення типу
    public static explicit operator string(DRomb rhomb)
    {
        return $"{rhomb.d1},{rhomb.d2},{rhomb.color}";
    }

    public static explicit operator DRomb(string str)
    {
        string[] parts = str.Split(',');
        if (parts.Length != 3)
            throw new FormatException("Рядок має бути у форматі 'd1,d2,color'");
        
        if (!int.TryParse(parts[0], out int d1) || d1 <= 0 ||
            !int.TryParse(parts[1], out int d2) || d2 <= 0 ||
            !int.TryParse(parts[2], out int color))
            throw new FormatException("Неправильний формат чисел або від'ємні/нульові діагоналі");

        return new DRomb(d1, d2, color);
    }
}

class Program
{
    static void Main()
    {
        // Тестування класу
        DRomb[] rhombuses = new DRomb[]
        {
            new DRomb(10, 20, 1),
            new DRomb(15, 15, 2),
            new DRomb(25, 30, 3)
        };

        for (int i = 0; i < rhombuses.Length; i++)
        {
            Console.WriteLine($"Ромб з кольором {rhombuses[i].Color}:");
            rhombuses[i].PrintDimensions();
            Console.WriteLine($"Площа: {rhombuses[i].CalculateArea()}");
            Console.WriteLine($"Периметр: {rhombuses[i].CalculatePerimeter()}");
            Console.WriteLine($"Це квадрат: {(rhombuses[i] ? "Так" : "Ні")}");

            // Тестування індексатора
            Console.WriteLine($"Індексатор: d1={rhombuses[i][0]}, d2={rhombuses[i][1]}, color={rhombuses[i][2]}");

            // Тестування ++
            Console.WriteLine("Після ++:");
            rhombuses[i]++;
            rhombuses[i].PrintDimensions();

            // Тестування +
            var added = rhombuses[i] + 5;
            Console.WriteLine("Після +5:");
            added.PrintDimensions();

            // Тестування перетворення в string
            string rhombStr = (string)rhombuses[i];
            Console.WriteLine($"Перетворення в string: {rhombStr}");

            // Тестування перетворення з string
            DRomb fromStr = (DRomb)rhombStr;
            Console.WriteLine($"З рядка: d1={fromStr.D1}, d2={fromStr.D2}, color={fromStr.Color}");

            Console.WriteLine();
        }
    }
}
