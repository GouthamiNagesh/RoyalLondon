using System;
using NUnit.Framework;
using Royal_London_Code_Test.Models;
using Assert = NUnit.Framework.Assert;

namespace Royal_London_Code_Test.Tests.Models
{
    [TestFixture]
    public class PremiumCalculatorTest
    {
        #region Methods

        /// <summary>
        /// Calculate the Premium
        /// </summary>
        [Test]
        public void ShouldCalculatePremium()
        {
            const int id = 10;
            const string title = "Mrs";
            const string firstName = "Gouthami";
            const string surName = "Arumugam";
            const string productName = "Standard Cover";
            const decimal payoutAmount = 1000M;
            const decimal annualPremium = 500M;
            var product = new Product(productName, payoutAmount, annualPremium);
            var user = new User(id, title, firstName, surName, product);

            var expectedCreditCharge = (annualPremium * 5) / 100;
            var expectedTotalDebitPremium = annualPremium + expectedCreditCharge;
            var expectedAverageMonthlyPremium = expectedTotalDebitPremium / 12;
            decimal expectedInitialMonthlyPaymentAmount = 0;
            decimal expectedOtherMonthlyPaymentAmount = 0;

            var totalCost = Math.Truncate(100 * expectedAverageMonthlyPremium) / 100;
            var eachElevenMonthPremium = expectedTotalDebitPremium - (totalCost * 11);

            if (eachElevenMonthPremium > expectedAverageMonthlyPremium)
            {
                expectedInitialMonthlyPaymentAmount = Convert.ToDecimal($"{eachElevenMonthPremium:0.00}");
                expectedOtherMonthlyPaymentAmount = Convert.ToDecimal($"{totalCost:0.00}");
            }
            else
            {
                expectedInitialMonthlyPaymentAmount = Convert.ToDecimal($"{totalCost:0.00}");
                expectedOtherMonthlyPaymentAmount = Convert.ToDecimal($"{eachElevenMonthPremium:0.00}");
            }

            var premiumCalculator = PremiumCalculator.Instance;
            premiumCalculator.CalculatePremium(user);

            Assert.AreEqual(expectedCreditCharge, user.Product.CreditCharge);
            Assert.AreEqual(expectedTotalDebitPremium, user.Product.TotalDebitPremium);
            Assert.AreEqual(expectedAverageMonthlyPremium, user.Product.AverageMonthlyPremium);
            Assert.AreEqual(expectedInitialMonthlyPaymentAmount, user.Product.InitialMonthlyPaymentAmount);
            Assert.AreEqual(expectedOtherMonthlyPaymentAmount, user.Product.OtherMonthlyPaymentAmount);

        }

        #endregion
    }
}