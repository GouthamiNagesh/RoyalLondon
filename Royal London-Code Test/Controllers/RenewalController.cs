using System;
using System.Web.Mvc;
using Royal_London_Code_Test.Models;

namespace Royal_London_Code_Test.Controllers
{
    public class RenewalController : Controller
    {
        #region Fields
        private string _filePath = string.Empty;
        readonly PremiumCalculator _premiumCalculatorInstance = PremiumCalculator.Instance;
        readonly DocumentGenerator _documentGeneratorInstance = DocumentGenerator.Instance;
        readonly FileManager _fileManagerInstance = FileManager.Instance;
        readonly BusinessLogic _businessLogic = BusinessLogic.Instance;


        #endregion

        #region Methods
        // GET: Renewal
        public ActionResult RenewalDetail()
        {
            return View();
        }

        //POST: Generate Renewal Letter
        [HttpPost]
        public ActionResult GenerateLetter()
        {
            try
            {
                //Renewal Letter Generator
                string isValid = _businessLogic.RenewalGenerator();

                if (isValid != "Valid")
                {
                    //ViewBag.FileExist = true;
                    ViewBag.ErrorValidationMessage = isValid;
                    return View("RenewalDetail");
                }

                if (isValid == "Valid")
                {
                    string myString = _fileManagerInstance.GetFilePath();
                    return View("GenerateLetter", (object)myString);
                }
                return View("RenewalDetail");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Renewal", "GenerateLetter"));
            }
        }
        #endregion
    }
}

