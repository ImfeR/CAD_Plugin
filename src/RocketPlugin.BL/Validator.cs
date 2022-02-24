namespace RocketPlugin.BL
{
    /// <summary>
    /// Класс, отвечающий за валидацую параметров.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Проверяет значение на корректность.
        /// </summary>
        /// <param name="min">Минимальное значение параметра.</param>
        /// <param name="max">Максимальное значение параметра.</param>
        /// <param name="value">Проверяемое значение.</param>
        /// <returns>Результат проверки.</returns>
        public static bool ValidateValue(double min, double max, double value)
        {
            return value <= max && value >= min;
        }
    }
}
