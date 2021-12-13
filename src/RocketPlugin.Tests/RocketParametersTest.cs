﻿namespace RocketPlugin.Tests
{
    using BL;

    using NUnit.Framework;

    using System;

    [TestFixture]
    public class RocketParametersTest
    {
        [TestCase(10, TestName = "Проверка Get и Set для BodyLength при значении равному граничному минимальному")]
        [TestCase(15, TestName = "Проверка Get и Set для BodyLength при значении равному определенному выражению в границе допустимых значений")]
        [TestCase(25, TestName = "Проверка Get и Set для BodyLength при значении равному граничному максимальному")]
        public void BodyLength_GetSetValue_Success(double expectedValue)
        {
            RocketParameters rocketParameter = new RocketParameters();

            rocketParameter.BodyLength = expectedValue;

            Assert.AreEqual(expectedValue, rocketParameter.BodyLength);
        }

        [TestCase(9.9, TestName = "Проверка Set для BodyLength при присвоении значения равному меньше минимального возможного")]
        [TestCase(25.1, TestName = "Проверка Set для BodyLength при присвоении значения равному больше максимального возможного")]
        public void BodyLength_SetValue_Failed(double value)
        {
            RocketParameters rocketParameter = new RocketParameters();

            Assert.Throws<ArgumentException>(() => 
            {
                rocketParameter.BodyLength = value;
            });
        }

        [TestCase(2, TestName = "Проверка Get и Set для BodyDiameter при значении равному граничному минимальному")]
        [TestCase(2.5, TestName = "Проверка Get и Set для BodyDiameter при значении равному определенному выражению в границе допустимых значений")]
        [TestCase(3, TestName = "Проверка Get и Set для BodyDiameter при значении равному граничному максимальному")]
        public void BodyDiameter_GetSetValue_Success(double expectedValue)
        {
            RocketParameters rocketParameter = new RocketParameters();

            rocketParameter.BodyDiameter = expectedValue;

            Assert.AreEqual(expectedValue, rocketParameter.BodyDiameter);
        }

        [TestCase(1.9, TestName = "Проверка Set для BodyDiameter при присвоении значения равному меньше минимального возможного")]
        [TestCase(3.1, TestName = "Проверка Set для BodyDiameter при присвоении значения равному больше максимального возможного")]
        public void BodyDiameter_SetValue_Failed(double value)
        {
            RocketParameters rocketParameter = new RocketParameters();

            Assert.Throws<ArgumentException>(() =>
            {
                rocketParameter.BodyDiameter = value;
            });
        }

        [TestCase(4, TestName = "Проверка Get и Set для GuidesInnerRibLength при значении равному граничному минимальному")]
        [TestCase(5, TestName = "Проверка Get и Set для GuidesInnerRibLength при значении равному определенному выражению в границе допустимых значений")]
        [TestCase(6, TestName = "Проверка Get и Set для GuidesInnerRibLength при значении равному граничному максимальному")]
        public void GuidesInnerRibLength_GetSetValue_Success(double expectedValue)
        {
            RocketParameters rocketParameter = new RocketParameters();

            rocketParameter.GuidesInnerRibLength = expectedValue;

            Assert.AreEqual(expectedValue, rocketParameter.GuidesInnerRibLength);
        }

        [TestCase(3.9, TestName = "Проверка Set для GuidesInnerRibLength при присвоении значения равному меньше минимального возможного")]
        [TestCase(6.1, TestName = "Проверка Set для GuidesInnerRibLength при присвоении значения равному больше максимального возможного")]
        public void GuidesInnerRibLength_SetValue_Failed(double value)
        {
            RocketParameters rocketParameter = new RocketParameters();

            Assert.Throws<ArgumentException>(() =>
            {
                rocketParameter.GuidesInnerRibLength = value;
            });
        }

        [TestCase(4, TestName = "Проверка Get и Set для NoseLength при значении равному граничному минимальному")]
        [TestCase(6, TestName = "Проверка Get и Set для NoseLength при значении равному определенному выражению в границе допустимых значений")]
        [TestCase(8, TestName = "Проверка Get и Set для NoseLength при значении равному граничному максимальному")]
        public void NoseLength_GetSetValue_Success(double expectedValue)
        {
            RocketParameters rocketParameter = new RocketParameters();

            rocketParameter.NoseLength = expectedValue;

            Assert.AreEqual(expectedValue, rocketParameter.NoseLength);
        }

        [TestCase(3.9, TestName = "Проверка Set для NoseLength при присвоении значения равному меньше минимального возможного")]
        [TestCase(8.1, TestName = "Проверка Set для NoseLength при присвоении значения равному больше максимального возможного")]
        public void NoseLength_SetValue_Failed(double value)
        {
            RocketParameters rocketParameter = new RocketParameters();

            Assert.Throws<ArgumentException>(() =>
            {
                rocketParameter.NoseLength = value;
            });
        }

        [TestCase(3, TestName = "Проверка Get и Set для WingsLength при значении равному граничному минимальному")]
        [TestCase(5, TestName = "Проверка Get и Set для WingsLength при значении равному определенному выражению в границе допустимых значений")]
        [TestCase(7, TestName = "Проверка Get и Set для WingsLength при значении равному граничному максимальному")]
        public void WingsLength_GetSetValue_Success(double expectedValue)
        {
            RocketParameters rocketParameter = new RocketParameters();

            rocketParameter.WingsLength = expectedValue;

            Assert.AreEqual(expectedValue, rocketParameter.WingsLength);
        }

        [TestCase(2.9, TestName = "Проверка Set для WingsLength при присвоении значения равному меньше минимального возможного")]
        [TestCase(7.1, TestName = "Проверка Set для WingsLength при присвоении значения равному больше максимального возможного")]
        public void WingsLength_SetValue_Failed(double value)
        {
            RocketParameters rocketParameter = new RocketParameters();

            Assert.Throws<ArgumentException>(() =>
            {
                rocketParameter.WingsLength = value;
            });
        }

        [TestCase(0.5, TestName = "Проверка Get и Set для WingsWidth при значении равному граничному минимальному")]
        [TestCase(0.7, TestName = "Проверка Get и Set для WingsWidth при значении равному определенному выражению в границе допустимых значений")]
        [TestCase(1, TestName = "Проверка Get и Set для WingsWidth при значении равному граничному максимальному")]
        public void WingsWidth_GetSetValue_Success(double expectedValue)
        {
            RocketParameters rocketParameter = new RocketParameters();

            rocketParameter.WingsWidth = expectedValue;

            Assert.AreEqual(expectedValue, rocketParameter.WingsWidth);
        }

        [TestCase(0.3, TestName = "Проверка Set для WingsWidth при присвоении значения равному меньше минимального возможного")]
        [TestCase(1.1, TestName = "Проверка Set для WingsWidth при присвоении значения равному больше максимального возможного")]
        public void WingsWidth_SetValue_Failed(double value)
        {
            RocketParameters rocketParameter = new RocketParameters();

            Assert.Throws<ArgumentException>(() =>
            {
                rocketParameter.WingsWidth = value;
            });
        }

        [TestCase(TestName = "Проверка корректности создания объекта используя конструткор по умолчанию")]
        public void Constructor_CorrectCreation_Success()
        {
            var expectedBodyLength = 20;
            var expectedBodyDiameter = 2.5;
            var expectedGuidesCount = 4;
            var expectedGuidesDepth = 0.25;
            var expectedGuidesInnerRibLength = 5;
            var expectedGuidesOuterRibLength = 2.5;
            var expectedGuidesWidth = 0.5;
            var expectedNoseLength = 6;
            var expectedWingsCount = 4;
            var expectedWingsDepth = 0.25;
            var expectedWingsLength = 4;
            var expectedWingsWidth = 0.625;

            RocketParameters rocketParameters = new RocketParameters();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedBodyLength, rocketParameters.BodyLength);
                Assert.AreEqual(expectedBodyDiameter, rocketParameters.BodyDiameter);
                Assert.AreEqual(expectedGuidesCount, rocketParameters.GuidesCount);
                Assert.AreEqual(expectedGuidesDepth, rocketParameters.GuidesDepth);
                Assert.AreEqual(expectedGuidesInnerRibLength, rocketParameters.GuidesInnerRibLength);
                Assert.AreEqual(expectedGuidesOuterRibLength, rocketParameters.GuidesOuterRibLength);
                Assert.AreEqual(expectedGuidesWidth, rocketParameters.GuidesWidth);
                Assert.AreEqual(expectedNoseLength, rocketParameters.NoseLength);
                Assert.AreEqual(expectedWingsCount, rocketParameters.WingsCount);
                Assert.AreEqual(expectedWingsDepth, rocketParameters.WingsDepth);
                Assert.AreEqual(expectedWingsLength, rocketParameters.WingsLength);
                Assert.AreEqual(expectedWingsWidth, rocketParameters.WingsWidth);
            });
        }
    }
}
