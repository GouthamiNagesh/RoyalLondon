using System.IO;
using System.Web.Configuration;

namespace Royal_London_Code_Test.Models
{
    public sealed class FileManager
    {
        #region Fields
        private static FileManager _instance;
        private readonly string _folderDirectory = WebConfigurationManager.AppSettings["FolderLocation"];
        #endregion

        #region Contructors
        private FileManager()
        {
        }
        #endregion

        #region Properties
        public static FileManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FileManager();
                return _instance;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Create Folder Directory if not exist
        /// </summary>
        private void CreateFolder()
        {
            bool folderExists = Directory.Exists(_folderDirectory);
            if (!folderExists)
                Directory.CreateDirectory(_folderDirectory);
        }

        /// <summary>
        /// Validate If the Renewal Letter already generated and same file already Exist.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        /// <returns></returns>
        public bool ValidateFile(int id, string firstName, string surName)
        {
            CreateFolder();
            string fileName = id + "_" + firstName + "_" + surName;
            string filePath = _folderDirectory + fileName + ".docx";
            return (File.Exists(filePath) ? true : false);
        }

        /// <summary>
        /// Get the File path location where the Renewal Letter is located
        /// </summary>
        /// <returns></returns>
        public string GetFilePath()
        {
            return _folderDirectory;
        }
        #endregion
    }
}