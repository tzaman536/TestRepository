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


            Bond.ParValue = 1000;
            Bond.Coupon = 5;
            Bond.RequiredYield = 10;
            Bond.PaymentFrequency = "Semiannually";
        }


        private void calcButton_Click(object sender, RoutedEventArgs e)
        {
            Calculator c = new Calculator();
            //c.CalcPrice();
        }

        private void restButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
