using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.CompShop
{
    public class CompShopAddPage
    {
        public static By AddDeviceName => By.CssSelector("[name=Name]");
        public static By AddDeviceDescription => By.CssSelector("[name=Description]");
        public static By AddDeviceImageUrl => By.CssSelector("[name=Image]");
        public static By AddDevicePrice => By.CssSelector("[name=Price]");
        public static By CreateButton => By.CssSelector("form button");
    }
}
