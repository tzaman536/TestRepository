using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondCalculator
{
    public class InputBond : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double _faceValue;
        public double FaceValue
        {
            get { return _faceValue; }
            set { _faceValue = value; Notify("FaceValue"); }
        }

        private string _paymentFrequency;
        public string PaymentFrequency
        {
            get { return _paymentFrequency; }
            set { _paymentFrequency = value; Notify("PaymentFrequency"); }
        }


        private double _coupon;
        public double Coupon
        {
            get { return _coupon; }
            set { _coupon = value; Notify("Coupon"); }

        }

        private double _requiredYield;
        public double RequiredYield
        {
            get { return _requiredYield; }
            set { _requiredYield = value; Notify("RequiredYield"); }

        }

        private double? _calculatedPrice;
        public double? CalculatedPrice
        {
            get { return _calculatedPrice; }
            set { _calculatedPrice = value; Notify("CalculatedPrice"); }
        }

        private double? _calculatedYTM;
        public double? CalculatedYTM
        {
            get { return _calculatedYTM; }
            set { _calculatedYTM = value; Notify("CalculatedYTM"); }
        }

        private int _yearsToMaturity;
        public int YearsToMaturity
        {
            get { return _yearsToMaturity; }
            set { _yearsToMaturity = value; Notify("YearsToMaturity"); }

        }

        private int? _inputPrice;
        public int? InputPrice
        {
            get { return _inputPrice; }
            set { _inputPrice = value; Notify("InputPrice"); }

        }

        private string _log;
        public string Log
        {
            get { return _log; }
            set { _log = value; Notify("Log"); }

        }


        public void AppendToLog(string message)
        {
            Log = _log + "\r\n" + message;
        }


    }
}
