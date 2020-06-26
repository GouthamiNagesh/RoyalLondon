using System;
using System.Web.Configuration;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Royal_London_Code_Test.Models
{
    public sealed class DocumentGenerator : IDocumentGenerator
    {
        #region Fields
        private static DocumentGenerator _instance;
        private string _folderDirectory = WebConfigurationManager.AppSettings["FolderLocation"];
        #endregion

        #region Constructor
        private DocumentGenerator()
        {

        }
        #endregion

        #region Properties
        public static  DocumentGenerator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DocumentGenerator();
                return _instance;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generate the Renewal Letter
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns>returns bool True If Success-full</returns>
        public bool GenerateRenewalLetter(IUser userDetails)
        {
            try
            {
                //Location Path  
                string fileName = userDetails.Id + "_" + userDetails.FirstName + "_" + userDetails.SurName;
                string customerTitle = userDetails.Title;
                string firstname = userDetails.FirstName;
                string surName = userDetails.SurName;
                string prodName = userDetails.Product.ProductName;
                string payOutAmount = userDetails.Product.PayoutAmount.ToString();
                string annualPremium = userDetails.Product.AnnualPremium.ToString();
                string creditCharge = userDetails.Product.CreditCharge.ToString();
                string annualPremiumplusCreditCharge = userDetails.Product.TotalDebitPremium.ToString();
                string initialMonthlyPayment = userDetails.Product.InitialMonthlyPaymentAmount.ToString();
                string otherMonthlyPayment = userDetails.Product.OtherMonthlyPaymentAmount.ToString();

                string filePath = _folderDirectory + fileName;
                //Title  
                string title = "Renewal Letter";
                //Paragraph Text  
                string textParagraph = "" + DateTime.Now.ToString("dd/MM/yyyy") + Environment.NewLine + "FAO: " + customerTitle + " " + firstname + " " + surName
                    + Environment.NewLine + Environment.NewLine + "RE: Your Renewal"
                    + Environment.NewLine + Environment.NewLine + "Dear " + customerTitle + " " + surName
                    + Environment.NewLine + Environment.NewLine + "We hereby invite you to renew your Insurance Policy,subject to the following terms"
                    + Environment.NewLine + Environment.NewLine + "Your chosen insurance product is " + prodName
                    + Environment.NewLine + Environment.NewLine + "The amount payable to you in the event of a valid claim will be " + payOutAmount
                    + Environment.NewLine + Environment.NewLine + "Your annual premium will be " + annualPremium
                    + Environment.NewLine + Environment.NewLine + "If you choose to pay by Direct Debit, we will add a credit charge of " + creditCharge + ", bringing the total to " + annualPremiumplusCreditCharge + ". This is payable by an initial payment of " + initialMonthlyPayment + ",  followed by 11 payments of " + otherMonthlyPayment + " each"
                    + Environment.NewLine + Environment.NewLine + "Please get in touch with us to arrange your renewal by visiting https://www.regallutoncodingtest.co.uk/renew or calling us on 01625 123456."
                    + Environment.NewLine + Environment.NewLine + "Kind Regards"
                    + Environment.NewLine + "Regal Luton";

                //Formatting Title  
                Formatting titleFormat = new Formatting
                {
                    Bold = true,
                    FontFamily = new Font("Arial"),
                    Size = 18D,
                    Position = 40,
                    FontColor = System.Drawing.Color.Black,
                    UnderlineColor = System.Drawing.Color.Gray,
                    Italic = true
                };

                //Formatting Text Paragraph  
                Formatting textParagraphFormat = new Formatting
                {
                    FontFamily = new Font("Arial"), Size = 10D, Spacing = 2
                };
                
                //Create docx  
                var doc = DocX.Create(filePath);
                //Insert title  
                var paragraphTitle = doc.InsertParagraph(title, false, titleFormat);
                paragraphTitle.Alignment = Alignment.center;
                //Insert text  
                doc.InsertParagraph(textParagraph, false, textParagraphFormat);
                doc.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}