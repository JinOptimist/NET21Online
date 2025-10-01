using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.CoffeeShop
{
    public class CoffeAddPage
    {
        public static By ImgInput => By.CssSelector("[name=Img]");
        public static By NameInput => By.CssSelector("[name=Name]");
        public static By CellInput => By.CssSelector("[name=Cell]");

        public static By AuthorIdInput => By.CssSelector("[name=AuthorId]");     
        public static By CreateButton => By.CssSelector("form button");
    }
}
