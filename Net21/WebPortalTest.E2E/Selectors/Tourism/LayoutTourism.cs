using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.Tourism
{
    public class LayoutTourism
    {
        public static By DropDownMenu => By.CssSelector(".drop-down-menu");
        public static By AddShopItem = By.CssSelector(".add-shop-item");
    }
}
