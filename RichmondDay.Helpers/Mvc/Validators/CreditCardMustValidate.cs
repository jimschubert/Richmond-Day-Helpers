using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RichmondDay.Helpers {
    public class CreditCardMustValidate : ValidationAttribute {
        private const int _defaultMinLength = 13;
        private const int _maxLength = 19;
        private int _minLength = _defaultMinLength;
        private string _regex = @"^\d{{{0},{1}}}$";
        private const int _zero = '0';

        public CreditCardMustValidate() : base() { ErrorMessage = "Please enter a valid credit card number"; }

        public int MinLength {
            get {
                return _minLength;
            }
            set {
                if ((value < 8) || (value > _maxLength)) {
                    _minLength = _defaultMinLength;
                }
                else {
                    _minLength = value;
                }
            }
        }

        public override bool IsValid(object value) {
            string pan = value as string;

            if (String.IsNullOrEmpty(pan)) {
                return false;
            }

            Regex panRegex = new Regex(String.Format(_regex, MinLength.ToString(CultureInfo.InvariantCulture.NumberFormat), _maxLength.ToString(CultureInfo.InvariantCulture.NumberFormat)));

            if (!panRegex.IsMatch(pan))
                return false;

            // Validate the check digit using the Luhn algorithm
            string reversedPan = new string((pan.ToCharArray()).Reverse().ToArray());
            int sum = 0;
            int multiplier = 0;
            foreach (char ch in reversedPan) {
                int product = (ch - _zero) * (multiplier + 1);
                sum = sum + (product / 10) + (product % 10);
                multiplier = (multiplier + 1) % 2;
            }

            return sum % 10 == 0;
        }
    }
}
