using System;
using System.Collections.Generic;

namespace BenchmarkApp
{
    public static class LevenshteinAlgorithm
    {
        /// <summary>
        /// Вычисляет расстояние Левенштейна рекурсивно с использованием кеша (мемоизации).
        /// </summary>
        /// <param name="s1">Первая строка (исходная)</param>
        /// <param name="s2">Вторая строка (целевая)</param>
        /// <returns>Минимальное количество операций</returns>
        public static int Compute(string s1, string s2)
        {
            // Обработка пустых строк (базовые случаи)
            if (string.IsNullOrEmpty(s1)) return s2?.Length ?? 0;
            if (string.IsNullOrEmpty(s2)) return s1.Length;

            // Создаем словарь для мемоизации (кеша).
            // Ключ - кортеж (i, j), значение - вычисленное расстояние.
            var memo = new Dictionary<(int, int), int>();

            // Запускаем рекурсию с конца строк
            return Recursive(s1, s2, s1.Length, s2.Length, memo);
        }

        private static int Recursive(string s1, string s2, int i, int j, Dictionary<(int, int), int> memo)
        {
            // 1. Проверяем кеш
            if (memo.TryGetValue((i, j), out int cachedResult))
            {
                return cachedResult;
            }

            // 2. Базовые случаи (если одна из строк закончилась)
            if (i == 0) return j; // Только вставки
            if (j == 0) return i; // Только удаления

            // 3. Вычисляем стоимость замены
            int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;

            // 4. Рекурсивные вызовы для трех операций
            int deletion = Recursive(s1, s2, i - 1, j, memo) + 1;
            int insertion = Recursive(s1, s2, i, j - 1, memo) + 1;
            int substitution = Recursive(s1, s2, i - 1, j - 1, memo) + cost;

            // 5. Находим минимум
            int min = Math.Min(Math.Min(deletion, insertion), substitution);

            // 6. Сохраняем в кеш и возвращаем
            memo[(i, j)] = min;
            return min;
        }
    }
}
