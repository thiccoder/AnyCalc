using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyCalc
{
    class Number
    {
        public static char[] digitSet = new char[36];
        public static bool Fits(char sym, int Base) => (digitSet.Contains(sym) && Array.IndexOf(digitSet, sym) < Base);
        public static bool Fits(string text, int Base) 
        {
            foreach (char sym in text) 
            {
                if (!Fits(sym,Base)) return false;
            }
            return (Formal(text, Base) == text);
        }
        public static string Formal(string text,int Base) => new Number(text,Base).ToString();
        public static void Init() 
        {
            for (int i = 0; i < 10; i++) 
            {
                digitSet[i] = i.ToString()[0];
            }
            for (int i = 0; i < 26; i++)
            {
                digitSet[i + 10] = (char)(i + 65);
            }
        }
        public int[] Digits { get { return digits.ToArray(); } }
        private readonly List<int> digits = new();
        public int Base { get; private set; }
        private readonly bool sign = false;
        public static Number Zero(int Base=10)
        {
            return new Number("0", Base);
        }
        private static List<int> Prettify(List<int> digits, int Base)
        {
            bool needPrettify = false;
            foreach (int sym in digits)
            {
                if (sym >= Base || sym < 0)
                {
                    needPrettify = true;
                    break;
                }
            }
            if (needPrettify)
            {
                int l = digits.Count;
                for (int i = l - 1; i > 0; i--)
                {
                    if (digits[i] >= Base || digits[i] < 0)
                    {
                        digits[i - 1] += digits[i] / Base;
                        digits[i] = digits[i] % Base;
                    }
                }
                if (digits[0] >= Base)
                {
                    digits.Insert(0, digits[0] / Base);
                    digits[1] = digits[1] % Base;
                }
            }
            while (digits.Count > 1 && digits.First() == 0) { digits = digits.GetRange(1,digits.Count - 1); }
            return digits;
        }

        private bool CheckErrors(Number b)
        {
            if (b.Base != Base)
            {
                throw new ArgumentException("bases do not match");
            }
            else
            {
                return true;
            }
        }

        public static Number FromVals(List<int> digits, int Base = 10, bool sign = false)
        {
            string str = (sign ? "-" : "") + string.Join("", digits.Select(x => digitSet[x]));
            return new Number(str, Base);
        }

        public override bool Equals(object obj)
        {
            return CheckErrors(obj as Number) && ToString() == obj.ToString();
        }

        public Number Abs()
        {
            return FromVals(digits, Base);
        }

        public override string ToString()
        {
            if (digits.Count == 1 && digits[0] == 0)
            {
                return "0";
            }
            return (sign ? "-" : "") + string.Join("", digits.Select(x => digitSet[x]));
        }

        public override int GetHashCode()
        {
            int res = int.Parse(ToString()) + Base * 2;
            return res.GetHashCode();
        }

        public Number(string str, int Base = 10)
        {
            sign = false;
            if ((str[0] == '-') || (str[0] == '+'))
            {
                sign = str[0] == '-';
                str = str[1..];
            }
            digits = new List<int>(0);
            foreach (char sym in str)
            {
                digits.Add(Array.FindIndex(digitSet, x => x == sym));
            }
            this.Base = Base;
            digits = Prettify(digits,Base);
        }
        public static Number operator <<(Number a, int newBase)
        {
            const int BitsInLong = 64;
            int decimalNumber = (int)a;
            if (newBase < 2 || newBase > digitSet.Length)
                throw new ArgumentException("newBase must be between 2 and " + digitSet.Length.ToString());

            if (decimalNumber == 0)
                return Zero(newBase);

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % newBase);
                charArray[index--] = digitSet[remainder];
                currentNumber /= newBase;
            }

            string res = new(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                res = "-" + res;
            }

            return new Number(res, newBase);
        }
        public static bool operator ==(Number a, Number b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Number a, Number b)
        {
            return !a.Equals(b);
        }

        public static bool operator >(Number a, Number b)
        {
            if (a.CheckErrors(b))
            {
                if (a.sign != b.sign)
                {
                    return (a.sign ? 0 : 1) > (b.sign ? 0 : 1);
                }
                List<int> aDigits = new();
                List<int> bDigits = new();
                aDigits.AddRange(a.Digits);
                bDigits.AddRange(b.Digits);
                if (aDigits.Count > bDigits.Count)
                {
                    return true;
                }
                else if (aDigits.Count > bDigits.Count)
                {
                    return false;
                }
                else
                {
                    int cnt = 0;
                    for (int i = 0; i < aDigits.Count; i++)
                    {
                        if (aDigits[i] > bDigits[i])
                        {
                            cnt += 1;
                        }
                    }
                    if (cnt == aDigits.Count)
                    {
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool operator <(Number a, Number b)
        {
            return !(a > b || a == b);
        }
        public static bool operator >=(Number a, Number b)
        {
            return a > b || a == b;
        }
        public static bool operator <=(Number a, Number b)
        {
            return a < b || a == b;
        }

        public static Number operator -(Number a)
        {
            return FromVals(a.digits, a.Base, !a.sign);
        }

        public static Number operator +(Number a, Number b)
        {
            if (a.CheckErrors(b))
            {
                List<int> aDigits = new();
                List<int> bDigits = new();
                bool aSign = a.sign;
                bool bSign = b.sign;
                bool rSign = aSign;
                aDigits.AddRange(a.Digits);
                bDigits.AddRange(b.Digits);
                bool swap = a < b;
                if (aDigits.Count > bDigits.Count)
                {
                    for (int i = bDigits.Count; i < aDigits.Count; i++)
                    {
                        bDigits.Insert(0, 0);
                    }
                }
                else if (aDigits.Count < bDigits.Count)
                {
                    for (int i = aDigits.Count; i < bDigits.Count; i++)
                    {
                        aDigits.Insert(0, 0);
                    }
                }
                if (swap) 
                {
                    var tmpDigits = aDigits;
                    aDigits = bDigits;
                    bDigits = tmpDigits;
                    var tmpSign = aSign;
                    aSign = bSign;
                    bSign = tmpSign;
                    rSign = aSign;

                }
                var rDigits = new List<int>(aDigits.Count);
                rDigits.AddRange(Enumerable.Repeat(0, aDigits.Count));
                for (int i = 0; i < aDigits.Count; i++)
                {
                    int aDigit = aDigits[i] * (aSign ? -1 : 1);
                    int bDigit = bDigits[i] * (bSign ? -1 : 1);
                    rDigits[i] = aDigit + bDigit;
                }
                rDigits = Prettify(rDigits, a.Base);
                while (rDigits.First() < 0)
                {
                    rDigits = Prettify(rDigits, a.Base);
                    rSign = !rSign;
                    for (int i  = 0; i < rDigits.Count;i++) 
                        rDigits[i] *= -1;
                } 
                return FromVals(rDigits, a.Base, rSign);
            }
            else return Zero(a.Base);
        }

        public static Number operator -(Number a, Number b)
        {
            return a + (-b);
        }

        public static Number operator *(Number a, Number b)
        {
            Number res = Zero(a.Base);
            for (int i = 0; i < (int)b.Abs(); i++)
                res += a;
            return res;
        }
        public static Number operator ^(Number a,Number b)
        {
            Number res = new("1", a.Base);
            for (int i = 0; i < (int)b.Abs(); i++)
                res *= a;
            return res;
        }
        public static Number operator /(Number a, Number b)
        {
            if (b == Zero(b.Base))
                throw new DivideByZeroException();
            Number res = Zero(a.Base);
            Number cnt = Zero(a.Base);
            Number one = new("1", a.Base);
            while (res <= a)
            {
                res += b;
                cnt += one;
            }
            cnt -= one;
            return cnt;
        }

        public static Number operator %(Number a, Number b)
        {
            return a - (a / b * b);
        }

        public static explicit operator Number(int[] value)
        {
            if (value.Length != 2)
            {
                throw new ArgumentException(null, nameof(value));
            }
            else
            {
                return new Number(value[0].ToString(), value[1]);
            }
        }
        public static explicit operator int(Number a) 
        {
            if (a == Zero(a.Base))
            {
                return 0;
            }
            int res = 0;
            int multiplier = 1;
            foreach (int digit in a.Digits)
            {
                res += digit * multiplier;
                multiplier *= a.Base;
            }

            return res;
        }
    }
}
