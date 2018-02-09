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
            double tempInputDouble;
            int tempInputInt;
            int runtType;
            string userInput = "xyz";
            Console.WriteLine("Type exit to close the console");

            while (!userInput.ToLower().Equals("exit"))
            {
                Console.WriteLine();
                Console.Write("Enter 1 to compute price 2 to compute yield to maturity: ");
                userInput = Console.ReadLine();

                if (userInput.ToLower().Equals("exit"))
                    break;

                double coupon;
                int years;
                double face;
                double rate;
                double price;


                if (int.TryParse(userInput, out runtType))
                {
                    try
                    {
                        if (runtType > 2 || runtType < 1)
                        {
                            throw new Exception("Please enter either 1 or 2");
                        }

                        #region Face
                        Console.Write("Enter Face Amount: ");
                        userInput = Console.ReadLine();
                        if (double.TryParse(userInput, out tempInputDouble))
                        {
                            face = tempInputDouble;
                        }
                        else
                        {
                            throw new Exception("Please enter a valid face amount.");
                        }
                        #endregion

                        #region Years to maturity
                        Console.Write("Enter Years To Maturity: ");
                        userInput = Console.ReadLine();
                        if (int.TryParse(userInput, out tempInputInt))
                        {
                            years = tempInputInt;
                        }
                        else
                        {
                            throw new Exception("Please enter a valid years to maturity.");
                        }
                        #endregion

                        #region Coupon
                        Console.Write("Enter Coupon(%): ");
                        userInput = Console.ReadLine();
                        if (double.TryParse(userInput, out tempInputDouble))
                        {
                            coupon = tempInputDouble;
                        }
                        else
                        {
                            throw new Exception("Please enter a valid coupon.");
                        }
                        #endregion


                        Calculator calculator = new Calculator();
                        calculator.Message += Calculator_Message;

                        if (runtType == 1)
                        {
                            Console.Write("Enter Discount Rate(%): ");
                            userInput = Console.ReadLine();
                            if (double.TryParse(userInput, out tempInputDouble))
                            {
                                rate = tempInputDouble;
                            }
                            else
                            {
                                throw new Exception("Please enter a valid discount rate.");
                            }

                            calculator.CalcPrice(coupon, years, face, rate);
                        }

                        if (runtType == 2)
                        {
                            Console.Write("Enter Price: ");
                            userInput = Console.ReadLine();
                            if (double.TryParse(userInput, out tempInputDouble))
                            {
                                price = tempInputDouble;
                            }
                            else
                            {
                                throw new Exception("Please enter a valid price");
                            }

                            calculator.CalcYield(coupon, years, face, price);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                }

            }
            Console.WriteLine("Presess any key to continue");
            Console.ReadLine();

        }

        private static void Calculator_Message(string message)
        {
            Console.WriteLine(message);
        }

        private static void C_Message(string message)
        {
            Console.WriteLine(message);
        }

    }
}
