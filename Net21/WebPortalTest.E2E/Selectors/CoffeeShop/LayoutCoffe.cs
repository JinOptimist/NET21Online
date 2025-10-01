using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.CoffeeShop
{
    public class LayoutCoffe
    {
        public static By PageAdminCoffe => By.Id("user-icon");

        public static By PageAddCoffe => By.CssSelector(".add-coffe");

    }
}
