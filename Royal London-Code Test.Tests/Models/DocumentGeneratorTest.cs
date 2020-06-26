using System.Web.Configuration;
using NUnit.Framework;
using Royal_London_Code_Test.Models;
using Assert = NUnit.Framework.Assert;

namespace Royal_London_Code_Test.Tests.Models
{
    [TestFixture]
    public class DocumentGeneratorTest
    {
        private string _folderDirectory = WebConfigurationManager.AppSettings["FolderLocation"];
        
        #region Methods

        /// <summary>
        /// Calculate the Premium
        /// </summary>
        [Test]
        public void ShouldGenerateLetter()
        {
            const int id = 10;
            const string title = "Mrs";
            const string firstName = "Priyanka";
            const string surName = "Deshpande";
            const string productName = "Standard Cover";
            const decimal payoutAmount = 1000M;
            const decimal annualPremium = 500M;
            var product = new Product(productName, payoutAmount, annualPremium);
            var user = new User(id, title, firstName, surName, product);
            const bool expectedResult = true;

            var documentGenerator = DocumentGenerator.Instance;
            var actualResult = documentGenerator.GenerateRenewalLetter(user);
            Assert.AreEqual(expectedResult, actualResult);

        }
        #endregion
    }
}