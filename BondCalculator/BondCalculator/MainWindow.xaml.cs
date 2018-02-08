using BondCalculatorCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BondCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public InputBond Bond = new InputBond();



        private void LoadPaymentFrequency()
        {
            // Call DB/Service here to get payment frequency
            cbPaymentFrequency.Items.Add("Semiannually");
            cbPaymentFrequency.Items.Add("Quarterly");
            cbPaymentFrequency.Items.Add("Annually");

           
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadPaymentFrequency();
            DataContext = Bond;



            Bond.FaceValue = 1000;
            Bond.Coupon = 5;
            Bond.RequiredYield = 10;
            Bond.YearsToMaturity = 5;
            Bond.PaymentFrequency = "Semiannually";
            Bond.InputPrice = 640;
            Bond.CalculatedPrice = null;
            Bond.CalculatedYTM = null;
            Bond.Log = "Type price or required yield";
        }


        private void calcPriceButton_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Message += Calculator_Message;
            Bond.Log = null;

            Bond.CalculatedPrice = calculator.CalcPrice(Bond.Coupon/100, Bond.YearsToMaturity, Bond.FaceValue, Bond.RequiredYield/100, ((PaymentFrequency)Enum.Parse(typeof(PaymentFrequency), Bond.PaymentFrequency)));
        }


        private void calcYieldButton_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Message += Calculator_Message;
            Bond.Log = null;
            Bond.CalculatedYTM = calculator.CalcYield(Bond.Coupon/100, Bond.YearsToMaturity, Bond.FaceValue, (double)Bond.InputPrice);

        }


        private void Calculator_Message(string message)
        {
            Bond.AppendToLog(message);   
        }

        
    }
}
