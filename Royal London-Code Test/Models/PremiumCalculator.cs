using System;

namespace Royal_London_Code_Test.Models
{
    public sealed class PremiumCalculator : IPremiumCalculator
    {
        #region Fields
        private static PremiumCalculator _instance;
        #endregion

        #region Contructors
        private PremiumCalculator()
        {
        }
        #endregion

        #region Properties
        public static PremiumCalculator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PremiumCalculator();
                return _instance;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculate the Premium
        /// </summary>
        /// <param name="userDetails"></param>
        public void CalculatePremium(IUser userDetails)
        {
            try
            {
                userDetails.Product.CreditCharge = Math.Round(CreditCharge(userDetails.Product.AnnualPremium),2);
                userDetails.Product.TotalDebitPremium = userDetails.Product.AnnualPremium + userDetails.Product.CreditCharge;
                userDetails.Product.AverageMonthlyPremium = userDetails.Product.TotalDebitPremium / 12;

                //Get exact 2 digits after decimal points
                var monthlyPremium = Math.Truncate(100 * userDetails.Product.AverageMonthlyPremium) / 100;

                //Get one month Premium (Total Premium - Eleven Month Premium)
                var firstMonthPremium = userDetails.Product.TotalDebitPremium - (monthlyPremium * 11);

                userDetails.Product.InitialMonthlyPaymentAmount = Convert.ToDecimal($"{firstMonthPremium:0.00}");
                userDetails.Product.OtherMonthlyPaymentAmount = Convert.ToDecimal($"{monthlyPremium:0.00}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculate the Credit Charge
        /// </summary>
        /// <param name="annualPremium"></param>
        /// <returns></returns>
        private static decimal CreditCharge(decimal annualPremium)
        {
            return 5 * annualPremium / 100;
        }
        #endregion
    }
}