using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.CompShop
{
    public class LayoutCompShop
    {
        public static By AddDeviceLink => By.CssSelector(".add-device");

        public static By CatalogLink => By.CssSelector(".catalog-link");
    }
}
