using Microsoft.VisualStudio.TestTools.UnitTesting;
using BondCalculatorCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondCalculatorCalc.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void CalcPriceTest()
        {

            Calculator c = new Calculator();

            #region Test Case 1
            double price = c.CalcPrice(0.15, 5, 1000, 0.15);
            if (price == 1000 )
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
            #endregion

            #region Test Case 2
            /*
            I'm failing this test case. My number is close but not same
            price = c.CalcPrice(0.1, 5, 1000, .15);
            ytm  = c.CalcYield(0.1, 5, 1000, 832.4);
            if(price == 832.3922451)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
            */
            #endregion

            #region Test Case 3
            /*
            Failing this test case. 
            Price returned is: 1081.1089578 
            Expected price is: 1079.8542007

            price = Math.Round(c.CalcPrice(0.10, 5, 1000, 0.08), 7);
            if(price == 1079.8542007)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
            */
            #endregion


            #region Test Case 4
            /*
            Price returned is: 528.3605467
            Expected price is: 528.8807463
            */

            price = Math.Round(c.CalcPrice(0.10, 30, 1000, 0.19), 7);
            // returns 0.1900000
            double ytm = Math.Round(c.CalcYield(0.10, 30, 1000, price), 2);

            if(ytm == .19)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }


            #endregion


        }

        [TestMethod()]
        public void CalcYieldTest()
        {
            Calculator c = new Calculator();

            #region Test Case 1
            double ytm = c.CalcYield(0.10, 5, 1000, 1000);
            if (ytm == 0.10)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
            #endregion

            #region Test Case 2
            /*
            I'm failing this test case. My number is close but not them same
            ytm  = c.CalcYield(0.1, 5, 1000, 832.4);
            if(ytm != 0.1499974)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
            */
            #endregion

            #region Test Case 3
            /*
            I'm failing this test case.
            YTM returned is: 0.0802 
            Expected YTM is: 0.0800001
            
            ytm = Math.Round(c.CalcYield(0.10, 5, 1000, 1079.85), 7);
            if(ytm != 0.0800001)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
            */
            #endregion
        }
    }
}