using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BondCalculator
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate
          (object value, System.Globalization.CultureInfo cultureInfo)
        {
            try {
                if (value == null)
                    return new ValidationResult(false, "value cannot be empty.");
                else
                {
                    if (String.IsNullOrEmpty(value.ToString()))
                        return new ValidationResult
                        (false, "value can not be empty.");
                }
                return ValidationResult.ValidResult;
            }
            catch(Exception ex)
            {
                return new ValidationResult
                (false, ex.Message);
            }
        }
    }
}
