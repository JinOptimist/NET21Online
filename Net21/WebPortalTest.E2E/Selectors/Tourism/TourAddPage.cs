using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.Tourism
{
    public class TourAddPage
    {
        public static By UserNameInput => By.CssSelector("[name=TourName]");
        public static By UploadImgFile => By.CssSelector("[name=TourImgFile]");
        public static By AddButton => By.CssSelector(".add-button");

    }
}
