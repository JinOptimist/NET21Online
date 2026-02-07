using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.Tourism
{
    public class ShopPage
    {
        public static By AllToursDivs => By.CssSelector(".shop .product-block:not(.template-car)");
        public static By RemoveButton => By.CssSelector("a.remove-button");
        public static By GetTourDivByTourId(string id)
            => By.CssSelector($".shop .product-block[data-id='{id}']");

    }
}
