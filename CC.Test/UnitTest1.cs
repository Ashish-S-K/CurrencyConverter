using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_OneDollar()
        {
            //Arrange
            string inputValue = "1";
            string expectedValue = "one dollar";            
            CurrencyConverterService.CurrencyConverter currencyConverter = new CurrencyConverterService.CurrencyConverter();

            //Act            
            string actualValue = currencyConverter.GetResult(inputValue);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_DollarWIthCents()
        {
            //Arrange
            string inputValue = "25,1";
            string expectedValue = "twenty-five dollars and ten cents";            
            CurrencyConverterService.CurrencyConverter currencyConverter = new CurrencyConverterService.CurrencyConverter();

            //Act
            string actualValue = currencyConverter.GetResult(inputValue);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_NegativeInput()
        {
            //Arrange
            string inputValue = "-25";
            string expectedValue = "Input in not is correct format";            
            CurrencyConverterService.CurrencyConverter currencyConverter = new CurrencyConverterService.CurrencyConverter();

            //Act
            string actualValue = currencyConverter.GetResult(inputValue);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_OnlyDollars()
        {
            //Arrange
            string inputValue = "45100";
            string expectedValue = "forty-five thousand one hundred  dollars";            
            CurrencyConverterService.CurrencyConverter currencyConverter = new CurrencyConverterService.CurrencyConverter();

            //Act
            string actualValue = currencyConverter.GetResult(inputValue);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_OutOfRange()
        {
            //Arrange
            string inputValue = "25,9876";
            string expectedValue = "Input is Out of Range";            
            CurrencyConverterService.CurrencyConverter currencyConverter = new CurrencyConverterService.CurrencyConverter();

            //Act
            string actualValue = currencyConverter.GetResult(inputValue);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_ZeroDollars()
        {
            //Arrange
            string inputValue = "0";
            string expectedValue = "zero dollars";
            CurrencyConverterService.CurrencyConverter currencyConverter = new CurrencyConverterService.CurrencyConverter();

            //Act
            string actualValue = currencyConverter.GetResult(inputValue);

            //Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
