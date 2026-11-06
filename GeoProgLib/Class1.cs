
namespace GeoProgLib
{
    public static class ProgressionChecker
    {
        /// <summary>
        /// явл€етс€ ли последовательность геометрической прогрессией.
        /// </summary>
        public static bool IsGeometric(IReadOnlyList<int> numbers)
        {
            // ћеньше двух чисел Ч прогрессию не определить
            if (numbers is null || numbers.Count < 2)
                return false;

            // «наменатель прогрессии: q = a[1] / a[0]
            double q = (double)numbers[1] / numbers[0];

            //  аждый следующий элемент должен быть в q раз больше предыдущего
            for (int i = 2; i < numbers.Count; i++)
            {
                if (numbers[i] != numbers[i - 1] * q)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// „еловекочитаемый результат проверки.
        /// </summary>
        public static string Describe(IReadOnlyList<int> numbers) =>
            IsGeometric(numbers) ? "Geometric progression" : "Non-geometric progression";
    }
}
