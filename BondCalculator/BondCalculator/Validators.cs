using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BondCalculator
{
    public class RequiredFiledValidator : ValidationRule
    {
        public override ValidationResult Validate
          (object value, System.Globalization.CultureInfo cultureInfo)
        {
            try {
                if (value == null)
                    return new ValidationResult(false, "Required.");
                else
                {
                    if (String.IsNullOrEmpty(value.ToString()))
                        return new ValidationResult
                        (false, "Required.");
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
