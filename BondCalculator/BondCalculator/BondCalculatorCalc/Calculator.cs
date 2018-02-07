using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondCalculatorCalc
{

    public enum PaymentFrequency
    {
        Semiannually
        ,Quarterly
        ,Annually
    }
    public class Calculator
    {
        public event CalculatorSendMessageHandler Message;

        public delegate void CalculatorSendMessageHandler(string message);

        public double CalcPrice(double coupon, int years, double face, double rate, PaymentFrequency frequency = PaymentFrequency.Semiannually)
        {


            double presentValueOfCashFlow = 0;
            double presentValueOfFaceAmount = 0;

            try {

                int paymentFrequency = 2;
                if (frequency == PaymentFrequency.Quarterly)
                    paymentFrequency = 4;
                if (frequency == PaymentFrequency.Annually)
                    paymentFrequency = 1;


                double c = (face * coupon) / paymentFrequency;
                double n = years * paymentFrequency;
                double i = rate / paymentFrequency;
                
                presentValueOfCashFlow = c * (((1 - (1 / Math.Pow(1 + i, n))) / i));
                presentValueOfFaceAmount = face * (1 / Math.Pow(1 + i, n));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


            return Math.Round(presentValueOfCashFlow + presentValueOfFaceAmount,7);
        }


        public double CalcYield(double coupon, int years, double face, double price, PaymentFrequency frequency = PaymentFrequency.Semiannually)
        {
            double presentValueOfCashFlow = 0;
            double presentValueOfFaceAmount = 0;

            try
            {
                int paymentFrequency = 2;

                if (frequency == PaymentFrequency.Quarterly)
                    paymentFrequency = 4;
                if (frequency == PaymentFrequency.Annually)
                    paymentFrequency = 1;


                double c = (face * coupon) / paymentFrequency;
                double n = years * paymentFrequency;
                double i = coupon / paymentFrequency;

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
                    if(Message != null)
                    {
                        Message(string.Format("Attempt {0} to find YTM for price {1}", attemptCount, price));
                    }

                    if (!visitedPrice.ContainsKey(tempPrice))
                    {
                        visitedPrice.Add(tempPrice,null);
                    }
                    else
                    {
                        // We will keep adjusting discount factor to get closer to the price
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

                    n = years * paymentFrequency;
                    i = coupon / paymentFrequency;
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
