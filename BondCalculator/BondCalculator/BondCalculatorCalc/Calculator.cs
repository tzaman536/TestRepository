using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondCalculatorCalc
{
    public class Calculator
    {
        public double CalcPrice(double coupon, int years, double face, double rate)
        {
            
            double presentValueOfCashFlow = 0;
            double presentValueOfFaceAmount = 0;

            try {
                double c = (face * coupon) / 2;
                double n = years * 2;
                double i = rate / 2;
                
                presentValueOfCashFlow = c * (((1 - (1 / Math.Pow(1 + i, n))) / i));
                presentValueOfFaceAmount = face * (1 / Math.Pow(1 + i, n));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


            return Math.Round(presentValueOfCashFlow + presentValueOfFaceAmount,7);
        }


        public double CalcYield(double coupon, int years, double face, double price)
        {
            double presentValueOfCashFlow = 0;
            double presentValueOfFaceAmount = 0;

            try
            {
                double c = (face * coupon) / 2;
                double n = years * 2;
                double i = coupon / 2;

                presentValueOfCashFlow = c * (((1 - (1 / Math.Pow(1 + i, n))) / i));
                presentValueOfFaceAmount = face * (1 / Math.Pow(1 + i, n));

                double tempPrice = presentValueOfCashFlow + presentValueOfFaceAmount;
                double factor = .005;
                string factorDirection = string.Empty;

                Dictionary<double, string> visitedPrice = new Dictionary<double, string>();

                int attemptCount = 0; 
                while (Math.Round(tempPrice) != Math.Round(price))
                {
                    attemptCount++;
                    Console.WriteLine("Attempt {0} to find YTM for price {1}", attemptCount, price );
                    if(!visitedPrice.ContainsKey(tempPrice))
                    {
                        visitedPrice.Add(tempPrice,null);
                    }
                    else
                    {
                        factor = factor - .0010;

                        if (factor <= 0)
                            factor = .0001;
                    }

                    if (tempPrice > price)
                    {
                        coupon = coupon + factor;
                    }
                    else
                    {
                        coupon = coupon - factor;
                    }

                    n = years * 2;
                    i = coupon / 2;
                    presentValueOfCashFlow = c * (((1 - (1 / Math.Pow(1 + i, n))) / i));
                    presentValueOfFaceAmount = face * (1 / Math.Pow(1 + i, n));
                    tempPrice = presentValueOfCashFlow + presentValueOfFaceAmount;
                }

                return coupon;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



            return 0;
        }
    }
}
