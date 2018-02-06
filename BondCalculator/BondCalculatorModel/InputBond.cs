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

        private int _parValue;
        public int ParValue
        {
            get { return _parValue; }
            set { _parValue = value; Notify("ParValue"); }
        }

        private string _paymentFrequency;
        public string PaymentFrequency
        {
            get { return _paymentFrequency; }
            set { _paymentFrequency = value; Notify("PaymentFrequency"); }
        }


        private decimal _coupon;
        public decimal Coupon
        {
            get { return _coupon; }
            set { _coupon = value; Notify("Coupon"); }

        }

        private decimal _requiredYield;
        public decimal RequiredYield
        {
            get { return _requiredYield; }
            set { _requiredYield = value; Notify("RequiredYield"); }

        }


    }
}
