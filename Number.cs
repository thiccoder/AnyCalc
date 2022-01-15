using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace AnyCalc
{
    class Number
    {
        public static char[] vls = new char[36];
        public static bool Fits(char sym, int Base) => (vls.Contains(sym) && Array.IndexOf(vls, sym) < Base);
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
                vls[i] = i.ToString()[0];
            }
            for (int i = 0; i < 26; i++)
            {
                vls[i + 10] = (char)(i + 65);
            }
        }
        public int[] Values { get { return values.ToArray(); } }
        private readonly List<int> values = new();
        public int Base { get; private set; }
        private readonly bool neg = false;
        public static Number operator <<(Number a, int newBase) 
        {
            const int BitsInLong = 64;
            int decimalNumber = (int)a;
            if (newBase < 2 || newBase > vls.Length)
                throw new ArgumentException("newBase must be between 2 and " + vls.Length.ToString());

            if (decimalNumber == 0)
                return Zero(newBase);

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % newBase);
                charArray[index--] = vls[remainder];
                currentNumber /= newBase;
            }

            string res = new(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                res = "-" + res;
            }

            return new Number(res,newBase);
        }
        public static Number Zero(int Base=10)
        {
            return new Number("0", Base);
        }

        private static List<int> Prettify(List<int> vals, int Base)
        {
            bool needPretify = false;
            foreach (int sym in vals)
            {
                if (sym >= Base || sym < 0)
                {
                    needPretify = true;
                    break;
                }
            }
            if (needPretify)
            {
                int l = vals.Count;
                for (int i = l - 1; i > 0; i--)
                {
                    if (vals[i] >= Base || vals[i] < 0)
                    {
                        vals[i - 1] += vals[i] / Base;
                        vals[i] = vals[i] % Base;
                    }
                }
                if (vals[0] >= Base)
                {
                    vals.Insert(0, vals[0] / Base);
                    vals[1] = vals[1] % Base;
                }
            }
            while (vals.Count > 1 && vals.First() == 0) { vals = vals.GetRange(1,vals.Count - 1); }
            return vals;
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

        public static Number FromVals(List<int> vals, int Base = 10, bool neg = false)
        {
            string str = (neg ? "-" : "") + string.Join("", vals.Select(x => vls[x]));
            return new Number(str, Base);
        }

        public override bool Equals(object obj)
        {
            return CheckErrors(obj as Number) && ToString() == obj.ToString();
        }

        public Number Abs()
        {
            return FromVals(values, Base);
        }

        public override string ToString()
        {
            if (values.Count == 1 && values[0] == 0)
            {
                return "0";
            }
            return (neg ? "-" : "") + string.Join("", values.Select(x => vls[x]));
        }

        public override int GetHashCode()
        {
            int res = int.Parse(ToString()) + Base * 2;
            return res.GetHashCode();
        }

        public Number(string str, int Base = 10)
        {
            neg = false;
            if ((str[0] == '-') || (str[0] == '+'))
            {
                neg = str[0] == '-';
                str = str[1..];
            }
            values = new List<int>(0);
            foreach (char sym in str)
            {
                values.Add(Array.FindIndex(vls, x => x == sym));
            }
            this.Base = Base;
            values = Prettify(values,Base);
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
                if (a.neg != b.neg)
                {
                    return (a.neg ? 0 : 1) > (b.neg ? 0 : 1);
                }
                List<int> sv = new();
                List<int> ov = new();
                sv.AddRange(a.Values);
                ov.AddRange(b.Values);
                if (sv.Count > ov.Count)
                {
                    return true;
                }
                else if (sv.Count > ov.Count)
                {
                    return false;
                }
                else
                {
                    int cnt = 0;
                    for (int i = 0; i < sv.Count; i++)
                    {
                        if (sv[i] > ov[i])
                        {
                            cnt += 1;
                        }
                    }
                    if (cnt == sv.Count)
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
            return FromVals(a.values, a.Base, !a.neg);
        }

        public static Number operator +(Number a, Number b)
        {
            if (a.CheckErrors(b))
            {
                List<int> av = new();
                List<int> bv = new();
                bool an = a.neg;
                bool bn = b.neg;
                bool nn = an;
                av.AddRange(a.Values);
                bv.AddRange(b.Values);
                bool swap = a < b;
                if (av.Count > bv.Count)
                {
                    for (int i = bv.Count; i < av.Count; i++)
                    {
                        bv.Insert(0, 0);
                    }
                }
                else if (av.Count < bv.Count)
                {
                    for (int i = av.Count; i < bv.Count; i++)
                    {
                        av.Insert(0, 0);
                    }
                }
                if (swap) 
                {
                    var tmpv = av;
                    av = bv;
                    bv = tmpv;
                    var tmpn = an;
                    an = bn;
                    bn = tmpn;
                    nn = an;

                }
                var nv = new List<int>(av.Count);
                nv.AddRange(Enumerable.Repeat(0, av.Count));
                for (int i = 0; i < av.Count; i++)
                {
                    int ar = av[i] * (an ? -1 : 1);
                    int br = bv[i] * (bn ? -1 : 1);
                    nv[i] = ar + br;
                }
                nv = Prettify(nv, a.Base);
                while (nv.First() < 0)
                {
                    nv = Prettify(nv, a.Base);
                    nn = !nn;
                    for (int i  = 0; i < nv.Count;i++) 
                        nv[i] *= -1;
                } 
                return FromVals(nv, a.Base, nn);
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
                //return Zero(10);
            }
            else
            {
                return new Number(value[0].ToString(), value[1]);
            }
        }
        public static explicit operator int(Number a) 
        {
            string number = a.ToString();
            if (a == Zero(a.Base))
            {
                return 0;
            }

            int res = 0;
            int multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char sym = number[i];
                int digit = Array.FindIndex(vls, x => x == sym);
                res += digit * multiplier;
                multiplier *= a.Base;
            }

            return res;
        }
    }
}
