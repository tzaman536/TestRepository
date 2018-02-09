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

        public InputBond Bond;



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
            Bond = new InputBond();
            DataContext = Bond;



            Bond.FaceValue = 1000;
            Bond.PaymentFrequency = "Semiannually";
            /*
            Bond.Coupon = null;
            Bond.RequiredYield = null;
            Bond.YearsToMaturity = null;
            Bond.InputPrice = null;
            Bond.CalculatedPrice = null;
            Bond.CalculatedYTM = null;
            */
            Bond.Log = "Type price or required yield";
        }

        private bool IsReadyToCalcPrice()
        {

            BindingExpression binding = coupon.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = yearsToMaturity.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = faceValue.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = requiredYield.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            if (Bond.Coupon != null
                && Bond.YearsToMaturity != null
                && Bond.FaceValue != null
                && Bond.RequiredYield != null
            )
            {
                return true;
            }

            return false;
        }


        private void calcPriceButton_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Message += Calculator_Message;
            Bond.Log = null;

            if (IsReadyToCalcPrice())
            {
                Bond.CalculatedPrice = calculator.CalcPrice((double)Bond.Coupon / 100, (int)Bond.YearsToMaturity, (double)Bond.FaceValue, (double)Bond.RequiredYield / 100, ((PaymentFrequency)Enum.Parse(typeof(PaymentFrequency), Bond.PaymentFrequency)));
            }
        }
        private bool IsReadyToCalcYield()
        {
            BindingExpression binding = coupon.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            BindingExpression ytm = yearsToMaturity.GetBindingExpression(TextBox.TextProperty);
            ytm.UpdateSource();

            binding = faceValue.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            binding = inputPrice.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            if (Bond.Coupon != null
                && Bond.YearsToMaturity != null
                && Bond.FaceValue != null
                && Bond.InputPrice != null
            )
            {
                return true;
            }


            return false;
        }


        private void calcYieldButton_Click(object sender, RoutedEventArgs e)
        {

            if (IsReadyToCalcYield())
            {
                Calculator calculator = new Calculator();
                calculator.Message += Calculator_Message;
                Bond.Log = null;
                Bond.CalculatedYTM = calculator.CalcYield((double)Bond.Coupon / 100, (int)Bond.YearsToMaturity, (double)Bond.FaceValue, (double)Bond.InputPrice);
            }
        }


        private void Calculator_Message(string message)
        {
            Bond.AppendToLog(message);   
        }

        
    }
}
