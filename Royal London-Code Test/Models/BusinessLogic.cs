using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.Configuration;

namespace Royal_London_Code_Test.Models
{
    public class BusinessLogic : IBusinessLogic
    {
        #region Fields
        private static BusinessLogic _instance;
        readonly DocumentGenerator _documentGeneratorInstance = DocumentGenerator.Instance;
        readonly PremiumCalculator _premiumCalculatorInstance = PremiumCalculator.Instance;
        readonly FileManager _fileManagerInstance = FileManager.Instance;
        private readonly string _folderDirectory = WebConfigurationManager.AppSettings["FolderLocation"];
        #endregion

        #region Contructors
        private BusinessLogic()
        {
        }
        #endregion

        #region Properties
        public static BusinessLogic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BusinessLogic();
                return _instance;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generate Renewal Letter
        /// </summary>
        /// <returns></returns>
        public string RenewalGenerator()//(string sheetName, string path)
        {
            const string sheetName = "in";
            string path = _folderDirectory + "Customer.xlsx";
            var dt = new DataTable();


            using (OleDbConnection conn = new OleDbConnection())
            {

                string importFileName = path;
                string fileExtension = Path.GetExtension(importFileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (var comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";
                    comm.Connection = conn;
                    using (var da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                    }
                }
            }

            string excelValidation = ValidateExcelData(dt);
            if (excelValidation == string.Empty)
            {
                var customerInputList = (from rw in dt.AsEnumerable()
                                         select new User()
                                         {
                                             Id = Convert.ToInt32(rw["ID"]),
                                             Title = Convert.ToString(rw["Title"]),
                                             FirstName = Convert.ToString(rw["FirstName"]),
                                             SurName = Convert.ToString(rw["SurName"]),
                                             Product = new Product(Convert.ToString(rw["ProductName"]),
                                                 Convert.ToDecimal(rw["PayoutAmount"]), Convert.ToDecimal(rw["AnnualPremium"]))
                                         }).ToList();



                foreach (var customer in customerInputList)
                {
                    //Premium Calculation for Each Customer
                    _premiumCalculatorInstance.CalculatePremium(customer);
                    //Document Generation for Each Customer
                    bool result = _documentGeneratorInstance.GenerateRenewalLetter(customer);
                }

            }
            else
            {
                return excelValidation;
            }

            return "Valid";

        }

        /// <summary>
        /// Validate the Excel Data
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string ValidateExcelData(DataTable dt)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            var titleList = new List<string>();
            titleList.Add("Miss");
            titleList.Add("Mr");
            titleList.Add("Mrs");

            foreach (var row in dt.AsEnumerable())
            {
                int outId;
                var isIdValid = int.TryParse(Convert.ToString(row["ID"]), out outId);

                if (!isIdValid)
                    builder.Append("Id,");

                var isTitleValid = titleList.Contains(Convert.ToString(row["Title"]));
                if (!isTitleValid)
                    builder.Append(" Title,");

                decimal outPayoutamount;
                var isPayoutAmountValid = decimal.TryParse(Convert.ToString(row["PayoutAmount"]), out outPayoutamount);

                decimal outAnnualPremium;
                var isAnnualPremiumValid = decimal.TryParse(Convert.ToString(row["AnnualPremium"]), out outAnnualPremium);

                if (!isPayoutAmountValid)
                    builder.Append(" PayOut Amount,");

                if (!isAnnualPremiumValid)
                    builder.Append(" Annual Premium,");

                if (!isTitleValid || !isPayoutAmountValid || !isAnnualPremiumValid)
                    builder.Append("is not valid for the Customer ID  " + row["ID"] + "." + Environment.NewLine);

                bool userExist = _fileManagerInstance.ValidateFile(Convert.ToInt32(row["ID"]), Convert.ToString(row["FirstName"]), Convert.ToString(row["SurName"]));
                if (userExist)
                {
                    builder.Append(" File Already Exist for the Customer ID " + row["ID"] + ".");
                }
            }

            return builder.ToString().TrimStart(',');

        }
        #endregion
    }
}