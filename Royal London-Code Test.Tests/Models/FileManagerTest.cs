using System.Web.Configuration;
using NUnit.Framework;
using Royal_London_Code_Test.Models;
using Assert = NUnit.Framework.Assert;

namespace Royal_London_Code_Test.Tests.Models
{
    [TestFixture]
    public class FileManagerTest
    {
        private readonly string _folderDirectory = WebConfigurationManager.AppSettings["FolderLocation"];
        
        #region Methods

        /// <summary>
        /// Validate File
        /// </summary>
        [Test]
        public void ShouldValidateFile()
        {
            const int id = 10;
            const string firstName = "Meera";
            const string surName = "Bhai";
            var fileManager = FileManager.Instance;
            var expectedResult = false;
            var actualResult = fileManager.ValidateFile(id, firstName, surName);
            Assert.AreEqual(expectedResult , actualResult);
        }

        /// <summary>
        /// Get File Path
        /// </summary>
        [Test]
        public void ShouldGetFilePath()
        {
            var fileManager = FileManager.Instance;
            var expectedResult = _folderDirectory;
            
            var actualResult = fileManager.GetFilePath();
            Assert.AreEqual(expectedResult, actualResult);
        }
        #endregion
    }
}