using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors.CompShop
{
    public class DevicePage
    {
        public static By GetDeviceDivById(int id)
            => By.CssSelector($".blocks .comp[data-id='{id}']");

        public static By AllDeviceDivs => By.CssSelector(".blocks .comp");

        public static By DeleteLink => By.CssSelector("a.delete-link");
    }
}
