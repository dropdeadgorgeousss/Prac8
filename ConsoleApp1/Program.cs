class Program
{
    public static void Main()
    {
        // 1. Читаем числа, пока не встретим 0
        var numbers = new List<int>();
        int n;
        while (Int32.TryParse(Console.ReadLine(), out n) && n != 0)
            numbers.Add(n);

        Console.WriteLine(IsGeometric(numbers)
            ? "Geometric progression"
            : "Non-geometric progression");
    }

    // Является ли последовательность геометрической прогрессией
    static bool IsGeometric(List<int> numbers)
    {
        // Меньше двух чисел — прогрессию не определить
        if (numbers.Count < 2)
            return false;

        // Знаменатель прогрессии: q = a[1] / a[0]
        // Считаем его один раз, а не на каждом шаге
        double q = (double)numbers[1] / numbers[0];

        // Каждый следующий элемент должен быть в q раз больше предыдущего
        for (int i = 2; i < numbers.Count; i++)
        {
            if (numbers[i] != numbers[i - 1] * q)
                return false;
        }

        return true;
    }
}
