using System;

/// <summary>
/// Клас для роботи з вектором беззнакових довгих цілих чисел (ulong).
/// </summary>
class VectorULong
{
    // Поля
    private readonly ulong[] IntArray; // Масив елементів вектора
    private readonly uint size;       // Розмір вектора
    private int codeError;            // Код помилки
    private static uint num_vec = 0;  // Кількість створених векторів

    // Конструктори
    /// <summary>
    /// Конструктор за замовчуванням. Створює вектор розміром 1 із значенням 0.
    /// </summary>
    public VectorULong()
    {
        size = 1;
        IntArray = new ulong[size];
        IntArray[0] = 0;
        codeError = 0;
        num_vec++;
    }

    /// <summary>
    /// Конструктор із заданим розміром. Ініціалізує всі елементи значенням 0.
    /// </summary>
    /// <param name="size">Розмір вектора (повинен бути більше 0)</param>
    /// <exception cref="ArgumentException">Якщо розмір <= 0</exception>
    public VectorULong(uint size)
    {
        this.size = size > 0 ? size : throw new ArgumentException("Розмір вектора має бути більше 0");
        IntArray = new ulong[this.size];
        Array.Fill(IntArray, 0); // Оптимізоване заповнення нулями
        codeError = 0;
        num_vec++;
    }

    /// <summary>
    /// Конструктор із заданим розміром і значенням. Ініціалізує всі елементи заданим значенням.
    /// </summary>
    /// <param name="size">Розмір вектора (повинен бути більше 0)</param>
    /// <param name="value">Значення для ініціалізації елементів</param>
    /// <exception cref="ArgumentException">Якщо розмір <= 0</exception>
    public VectorULong(uint size, ulong value)
    {
        this.size = size > 0 ? size : throw new ArgumentException("Розмір вектора має бути більше 0");
        IntArray = new ulong[this.size];
        Array.Fill(IntArray, value); // Оптимізоване заповнення значенням
        codeError = 0;
        num_vec++;
    }

    // Деструктор (фіналізатор)
    /// <summary>
    /// Фіналізатор для виведення повідомлення про знищення об’єкта та оновлення лічильника.
    /// </summary>
    ~VectorULong()
    {
        Console.WriteLine($"Вектор розміром {size} знищено.");
        num_vec--;
    }

    // Методи
    /// <summary>
    /// Введення елементів вектора з консолі.
    /// </summary>
    public void Input()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Введіть елемент {i}: ");
            if (ulong.TryParse(Console.ReadLine(), out ulong value))
                IntArray[i] = value;
            else
            {
                Console.WriteLine("Помилка введення. Встановлено 0.");
                IntArray[i] = 0;
                codeError = -1;
            }
        }
    }

    /// <summary>
    /// Виведення вектора та коду помилки.
    /// </summary>
    public void Print()
    {
        Console.WriteLine($"Вектор: [{string.Join(", ", IntArray)}]");
        Console.WriteLine($"Код помилки: {codeError}");
    }

    /// <summary>
    /// Присвоєння одного значення всім елементам вектора.
    /// </summary>
    /// <param name="value">Значення для присвоєння</param>
    public void Assign(ulong value)
    {
        Array.Fill(IntArray, value);
        codeError = 0;
    }

    /// <summary>
    /// Повертає кількість створених векторів.
    /// </summary>
    /// <returns>Кількість векторів</returns>
    public static uint GetVectorCount()
    {
        return num_vec;
    }

    // Властивості
    /// <summary>
    /// Отримання розміру вектора (тільки для читання).
    /// </summary>
    public uint Size => size;

    /// <summary>
    /// Отримання та встановлення коду помилки.
    /// </summary>
    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    // Індексатор
    /// <summary>
    /// Доступ до елементів вектора за індексом із перевіркою меж.
    /// </summary>
    /// <param name="index">Індекс елемента</param>
    /// <returns>Значення елемента або 0 при помилці</returns>
    public ulong this[int index]
    {
        get
        {
            if (index >= 0 && index < size)
            {
                codeError = 0;
                return IntArray[index];
            }
            codeError = -1;
            return 0;
        }
        set
        {
            if (index >= 0 && index < size)
            {
                codeError = 0;
                IntArray[index] = value;
            }
            else
            {
                codeError = -1;
            }
        }
    }

    // Перевантаження унарних операторів
    /// <summary>
    /// Збільшує кожен елемент вектора на 1.
    /// </summary>
    public static VectorULong operator ++(VectorULong vec)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] + 1;
        return result;
    }

    /// <summary>
    /// Зменшує кожен елемент вектора на 1 (не нижче 0).
    /// </summary>
    public static VectorULong operator --(VectorULong vec)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] > 0 ? vec.IntArray[i] - 1 : 0;
        return result;
    }

    /// <summary>
    /// Перевіряє, чи всі елементи вектора ненульові.
    /// </summary>
    public static bool operator true(VectorULong vec)
    {
        if (vec.size == 0) return false;
        foreach (var item in vec.IntArray)
            if (item == 0) return false;
        return true;
    }

    /// <summary>
    /// Перевіряє, чи є хоча б один нульовий елемент у векторі.
    /// </summary>
    public static bool operator false(VectorULong vec)
    {
        if (vec.size == 0) return true;
        foreach (var item in vec.IntArray)
            if (item == 0) return true;
        return false;
    }

    /// <summary>
    /// Перевіряє, чи вектор не порожній.
    /// </summary>
    public static bool operator !(VectorULong vec)
    {
        return vec.size != 0;
    }

    /// <summary>
    /// Виконує побітове заперечення для кожного елемента вектора.
    /// </summary>
    public static VectorULong operator ~(VectorULong vec)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = ~vec.IntArray[i];
        return result;
    }

    // Перевантаження арифметичних бінарних операторів
    /// <summary>
    /// Додає два вектори елемент за елементом.
    /// </summary>
    public static VectorULong operator +(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 + val2;
        }
        return result;
    }

    /// <summary>
    /// Додає скаляр до кожного елемента вектора.
    /// </summary>
    public static VectorULong operator +(VectorULong vec, ulong scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] + scalar;
        return result;
    }

    /// <summary>
    /// Віднімає два вектори елемент за елементом (не нижче 0).
    /// </summary>
    public static VectorULong operator -(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 > val2 ? val1 - val2 : 0;
        }
        return result;
    }

    /// <summary>
    /// Віднімає скаляр від кожного елемента вектора (не нижче 0).
    /// </summary>
    public static VectorULong operator -(VectorULong vec, ulong scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] > scalar ? vec.IntArray[i] - scalar : 0;
        return result;
    }

    /// <summary>
    /// Множить два вектори елемент за елементом.
    /// </summary>
    public static VectorULong operator *(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 * val2;
        }
        return result;
    }

    /// <summary>
    /// Множить кожен елемент вектора на скаляр.
    /// </summary>
    public static VectorULong operator *(VectorULong vec, ulong scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] * scalar;
        return result;
    }

    /// <summary>
    /// Ділить два вектори елемент за елементом.
    /// </summary>
    /// <exception cref="DivideByZeroException">Якщо ділене = 0</exception>
    public static VectorULong operator /(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val2 != 0 ? val1 / val2 : (val1 == 0 ? 0 : throw new DivideByZeroException());
        }
        return result;
    }

    /// <summary>
    /// Ділить кожен елемент вектора на скаляр.
    /// </summary>
    /// <exception cref="DivideByZeroException">Якщо скаляр = 0</exception>
    public static VectorULong operator /(VectorULong vec, ulong scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException();
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] / scalar;
        return result;
    }

    /// <summary>
    /// Отримує остачу від ділення двох векторів елемент за елементом.
    /// </summary>
    /// <exception cref="DivideByZeroException">Якщо ділене = 0</exception>
    public static VectorULong operator %(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val2 != 0 ? val1 % val2 : (val1 == 0 ? 0 : throw new DivideByZeroException());
        }
        return result;
    }

    /// <summary>
    /// Отримує остачу від ділення кожного елемента вектора на скаляр.
    /// </summary>
    /// <exception cref="DivideByZeroException">Якщо скаляр = 0</exception>
    public static VectorULong operator %(VectorULong vec, ulong scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException();
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] % scalar;
        return result;
    }

    // Перевантаження побітових бінарних операторів
    /// <summary>
    /// Виконує побітове АБО для двох векторів.
    /// </summary>
    public static VectorULong operator |(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 | val2;
        }
        return result;
    }

    /// <summary>
    /// Виконує побітове АБО для вектора та скаляра.
    /// </summary>
    public static VectorULong operator |(VectorULong vec, ulong scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] | scalar;
        return result;
    }

    /// <summary>
    /// Виконує побітове XOR для двох векторів.
    /// </summary>
    public static VectorULong operator ^(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 ^ val2;
        }
        return result;
    }

    /// <summary>
    /// Виконує побітове XOR для вектора та скаляра.
    /// </summary>
    public static VectorULong operator ^(VectorULong vec, ulong scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] ^ scalar;
        return result;
    }

    /// <summary>
    /// Виконує побітове І для двох векторів.
    /// </summary>
    public static VectorULong operator &(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 & val2;
        }
        return result;
    }

    /// <summary>
    /// Виконує побітове І для вектора та скаляра.
    /// </summary>
    public static VectorULong operator &(VectorULong vec, ulong scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] & scalar;
        return result;
    }

    /// <summary>
    /// Виконує побітовий зсув праворуч для двох векторів.
    /// </summary>
    public static VectorULong operator >>(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 >> (int)(val2 % 64); // Коректне обмеження для ulong
        }
        return result;
    }

    /// <summary>
    /// Виконує побітовий зсув праворуч для вектора на задану кількість біт.
    /// </summary>
    public static VectorULong operator >>(VectorULong vec, uint scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] >> (int)(scalar % 64); // Коректне обмеження для ulong
        return result;
    }

    /// <summary>
    /// Виконує побітовий зсув ліворуч для двох векторів.
    /// </summary>
    public static VectorULong operator <<(VectorULong v1, VectorULong v2)
    {
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            ulong val1 = i < v1.size ? v1.IntArray[i] : 0;
            ulong val2 = i < v2.size ? v2.IntArray[i] : 0;
            result.IntArray[i] = val1 << (int)(val2 % 64); // Коректне обмеження для ulong
        }
        return result;
    }

    /// <summary>
    /// Виконує побітовий зсув ліворуч для вектора на задану кількість біт.
    /// </summary>
    public static VectorULong operator <<(VectorULong vec, uint scalar)
    {
        VectorULong result = new VectorULong(vec.size);
        for (int i = 0; i < vec.size; i++)
            result.IntArray[i] = vec.IntArray[i] << (int)(scalar % 64); // Коректне обмеження для ulong
        return result;
    }

    // Перевантаження операторів порівняння
    /// <summary>
    /// Порівнює два вектори на рівність.
    /// </summary>
    public static bool operator ==(VectorULong v1, VectorULong v2)
    {
        if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null))
            return ReferenceEquals(v1, v2);
        if (v1.size != v2.size) return false;
        for (int i = 0; i < v1.size; i++)
            if (v1.IntArray[i] != v2.IntArray[i])
                return false;
        return true;
    }

    /// <summary>
    /// Порівнює два вектори на нерівність.
    /// </summary>
    public static bool operator !=(VectorULong v1, VectorULong v2)
    {
        return !(v1 == v2);
    }

    /// <summary>
    /// Перевіряє, чи перший вектор більше за другий.
    /// </summary>
    public static bool operator >(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        for (int i = 0; i < minSize; i++)
            if (v1.IntArray[i] <= v2.IntArray[i])
                return false;
        return v1.size > v2.size || (v1.size == v2.size && minSize > 0);
    }

    /// <summary>
    /// Перевіряє, чи перший вектор більше або дорівнює другому.
    /// </summary>
    public static bool operator >=(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        for (int i = 0; i < minSize; i++)
            if (v1.IntArray[i] < v2.IntArray[i])
                return false;
        return v1.size >= v2.size;
    }

    /// <summary>
    /// Перевіряє, чи перший вектор менше за другий.
    /// </summary>
    public static bool operator <(VectorULong v1, VectorULong v2)
    {
        return !(v1 >= v2);
    }

    /// <summary>
    /// Перевіряє, чи перший вектор менше або дорівнює другому.
    /// </summary>
    public static bool operator <=(VectorULong v1, VectorULong v2)
    {
        return !(v1 > v2);
    }

    // Перевизначення Equals і GetHashCode
    /// <summary>
    /// Порівнює поточний об’єкт із іншим об’єктом.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        VectorULong other = (VectorULong)obj;
        return this == other;
    }

    /// <summary>
    /// Повертає хеш-код об’єкта.
    /// </summary>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + size.GetHashCode();
            foreach (var item in IntArray)
                hash = hash * 23 + item.GetHashCode();
            return hash;
        }
    }
}

/// <summary>
/// Клас для запуску програми та тестування класу VectorULong.
/// </summary>
class Program
{
    static void Main()
    {
        try
        {
            // Тестування конструкторів
            Console.WriteLine("Тестування конструкторів:");
            VectorULong v1 = new VectorULong(); // Без параметрів
            Console.Write("v1: ");
            v1.Print();

            VectorULong v2 = new VectorULong(3); // З розміром
            Console.Write("v2: ");
            v2.Print();

            VectorULong v3 = new VectorULong(2, 5); // З розміром і значенням
            Console.Write("v3: ");
            v3.Print();

            Console.WriteLine($"Кількість векторів: {VectorULong.GetVectorCount()}");

            // Тестування введення
            Console.WriteLine("\nТестування введення для v2:");
            v2.Input();
            Console.Write("v2 після введення: ");
            v2.Print();

            // Тестування присвоєння
            Console.WriteLine("\nТестування присвоєння v2 значення 10:");
            v2.Assign(10);
            Console.Write("v2 після присвоєння: ");
            v2.Print();

            // Тестування індексатора
            Console.WriteLine("\nТестування індексатора:");
            v3[0] = 7;
            Console.WriteLine($"v3[0] = {v3[0]}");
            v3[10] = 100; // Неправильний індекс
            Console.WriteLine($"Спроба доступу до v3[10], codeError: {v3.CodeError}");
            Console.WriteLine($"v3[10] = {v3[10]}");

            // Тестування унарних операторів
            Console.WriteLine("\nТестування унарних операторів:");
            v3++;
            Console.Write("v3 після ++: ");
            v3.Print();

            v3--;
            Console.Write("v3 після --: ");
            v3.Print();

            Console.WriteLine($"v3 is true: {(v3 ? "Так" : "Ні")}");
            v3.Assign(0);
            Console.WriteLine($"v3 is true після присвоєння 0: {(v3 ? "Так" : "Ні")}");

            Console.WriteLine($"!v3: {(!v3 ? "Так" : "Ні")}");

            v3.Assign(5);
            var v4 = ~v3;
            Console.Write("v4 = ~v3: ");
            v4.Print();

            // Тестування бінарних арифметичних операторів
            Console.WriteLine("\nТестування бінарних арифметичних операторів:");
            v2.Assign(2);
            v3.Assign(3);
            var sum = v2 + v3;
            Console.Write("v2 + v3: ");
            sum.Print();

            var sumScalar = v2 + 5UL;
            Console.Write("v2 + 5: ");
            sumScalar.Print();

            var diff = v2 - v3;
            Console.Write("v2 - v3: ");
            diff.Print();

            var diffScalar = v2 - 1UL;
            Console.Write("v2 - 1: ");
            diffScalar.Print();

            var prod = v2 * v3;
            Console.Write("v2 * v3: ");
            prod.Print();

            var prodScalar = v2 * 3UL;
            Console.Write("v2 * 3: ");
            prodScalar.Print();

            var div = v2 / v3;
            Console.Write("v2 / v3: ");
            div.Print();

            var divScalar = v2 / 2UL;
            Console.Write("v2 / 2: ");
            divScalar.Print();

            var mod = v2 % v3;
            Console.Write("v2 % v3: ");
            mod.Print();

            var modScalar = v2 % 3UL;
            Console.Write("v2 % 3: ");
            modScalar.Print();

            // Тестування побітових операторів
            Console.WriteLine("\nТестування побітових операторів:");
            v2.Assign(6); // 110 в двійковій
            v3.Assign(3); // 011 в двійковій
            var or = v2 | v3;
            Console.Write("v2 | v3: ");
            or.Print();

            var orScalar = v2 | 4UL;
            Console.Write("v2 | 4: ");
            orScalar.Print();

            var xor = v2 ^ v3;
            Console.Write("v2 ^ v3: ");
            xor.Print();

            var xorScalar = v2 ^ 4UL;
            Console.Write("v2 ^ 4: ");
            xorScalar.Print();

            var and = v2 & v3;
            Console.Write("v2 & v3: ");
            and.Print();

            var andScalar = v2 & 4UL;
            Console.Write("v2 & 4: ");
            andScalar.Print();

            var rightShift = v2 >> v3;
            Console.Write("v2 >> v3: ");
            rightShift.Print();

            var rightShiftScalar = v2 >> 2U;
            Console.Write("v2 >> 2: ");
            rightShiftScalar.Print();

            var leftShift = v2 << v3;
            Console.Write("v2 << v3: ");
            leftShift.Print();

            var leftShiftScalar = v2 << 2U;
            Console.Write("v2 << 2: ");
            leftShiftScalar.Print();

            // Тестування операторів порівняння
            Console.WriteLine("\nТестування операторів порівняння:");
            v2.Assign(5);
            v3.Assign(5);
            Console.WriteLine($"v2 == v3: {v2 == v3}");
            Console.WriteLine($"v2 != v3: {v2 != v3}");

            v3.Assign(4);
            Console.WriteLine($"v2 > v3: {v2 > v3}");
            Console.WriteLine($"v2 >= v3: {v2 >= v3}");
            Console.WriteLine($"v2 < v3: {v2 < v3}");
            Console.WriteLine($"v2 <= v3: {v2 <= v3}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}