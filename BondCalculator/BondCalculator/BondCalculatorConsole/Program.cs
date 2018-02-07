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
            c.Message += C_Message;
            //returns 528.8807463
            double price = Math.Round(c.CalcPrice(0.10, 30, 1000, 0.19),7);
            // returns 0.1900000
            double ytm = Math.Round(c.CalcYield(0.10, 30, 1000, price),2); 

            /*
            double yieldDiscount = Math.Round(c.CalcYield(0.06, 18, 1000, 700.89),7);
            double priceWithYieldDiscount = Math.Round(c.CalcPrice(0.06, 18, 1000, yieldDiscount),2);
            

            double yieldPremium = Math.Round(c.CalcYield(0.06, 18, 1000, 1200),7);
            double priceWithYieldPermium = Math.Round(c.CalcPrice(0.06, 18, 1000, yieldPremium),2);
            */


            Console.WriteLine("Presess any key to continue");
            Console.ReadLine();

        }

        private static void C_Message(string message)
        {
            Console.WriteLine(message);
        }

    }
}
