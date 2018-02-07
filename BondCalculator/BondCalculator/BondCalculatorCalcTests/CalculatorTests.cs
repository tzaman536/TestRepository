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
            double result = c.CalcPrice(0.1, 5, 1000, .15);

            if(result == 832.3922451)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }


        }

        [TestMethod()]
        public void CalcYieldTest()
        {
            Calculator c = new Calculator();

            double result = c.CalcYield(0.1, 5, 1000, .15);

            if (result == 0.1499974)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}