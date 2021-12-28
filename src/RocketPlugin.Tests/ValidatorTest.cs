namespace RocketPlugin.Tests
{
    using BL;

    using NUnit.Framework;

    [TestFixture]
    public class ValidatorTest
    {
        [TestCase( 5, 10, 7, TestName = "Проверка значения равного серидине диапазона")]
        [TestCase( 5, 10, 5, TestName = "Проверка значения равного минимумиу диапазона")]
        [TestCase( 5, 10, 10, TestName = "Проверка значения равного максимуму диапазона")]
        public void Validate_CheckValue_IsValid(double min, double max, double value)
        {
            Assert.IsTrue(Validator.ValidateValue(min, max, value));
        }

        [TestCase(5, 10, 4.99, TestName = "Проверка значения меньше менимума диапазона")]
        [TestCase(5, 10, 10.01, TestName = "Проверка значения больше максимума диапазона")]
        public void Validate_CheckValue_NotValid(double min, double max, double value)
        {
            Assert.IsFalse(Validator.ValidateValue(min, max, value));
        }
    }
}
