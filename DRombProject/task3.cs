// Приблизна структура класу MatrixUlong згідно вимог
using System;

public class MatrixUlong
{
    protected ulong[,] ULArray;
    protected uint n, m;
    protected int codeError;
    protected static int num_m = 0;

    public uint N => n;
    public uint M => m;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    public MatrixUlong()
    {
        n = m = 1;
        ULArray = new ulong[1, 1];
        ULArray[0, 0] = 0;
        num_m++;
    }

    public MatrixUlong(uint rows, uint cols)
    {
        n = rows;
        m = cols;
        ULArray = new ulong[n, m];
        num_m++;
    }

    public MatrixUlong(uint rows, uint cols, ulong initialValue)
    {
        n = rows;
        m = cols;
        ULArray = new ulong[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ULArray[i, j] = initialValue;
        num_m++;
    }

    ~MatrixUlong()
    {
        Console.WriteLine("Destructor called for MatrixUlong");
    }

    public static int CountMatrices() => num_m;

    public void Input()
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                Console.Write($"[{i},{j}]: ");
                ULArray[i, j] = Convert.ToUInt64(Console.ReadLine());
            }
    }

    public void Print()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write(ULArray[i, j] + " ");
            Console.WriteLine();
        }
    }

    public void Assign(ulong value)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ULArray[i, j] = value;
    }

    public ulong this[int i, int j]
    {
        get
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ULArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                ULArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    public ulong this[int k]
    {
        get
        {
            int i = k / (int)m;
            int j = k % (int)m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ULArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            int i = k / (int)m;
            int j = k % (int)m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                ULArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    // Подальша реалізація: перевантаження операторів
    // Наприклад: public static MatrixUlong operator +(MatrixUlong a, MatrixUlong b) {...}
}

// Програма тестування
class Program
{
    static void Main()
    {
        MatrixUlong m1 = new MatrixUlong(2, 2, 5);
        MatrixUlong m2 = new MatrixUlong(2, 2, 3);

        m1.Print();
        Console.WriteLine();
        m2.Print();

        // Далі додай тестування перевантажених операторів
    }
}
