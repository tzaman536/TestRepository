using BondCalculator;
using BondCalculatorCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondCalculatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            /*double priceDiscountTest = c.CalcPrice(0.1, 5, 1000, .15);
            double priceDiscountFib = c.CalcPrice(0.09, 20, 1000, .12);
            double pricePermium = c.CalcPrice(0.1, 5, 1000, .08);
            */
            
            double yieldDiscount = Math.Round(c.CalcYield(0.06, 18, 1000, 700.89),7);
            double priceWithYieldDiscount = Math.Round(c.CalcPrice(0.06, 18, 1000, yieldDiscount),2);
            

            double yieldPremium = Math.Round(c.CalcYield(0.06, 18, 1000, 1200),7);
            double priceWithYieldPermium = Math.Round(c.CalcPrice(0.06, 18, 1000, yieldPremium),2);

            Console.WriteLine("Presess any key to continue");
            Console.ReadLine();

        }
    }
}
