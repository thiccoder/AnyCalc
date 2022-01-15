using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCalc
{
    readonly struct StoredObject
    {
        public readonly bool IsOperator;
        private readonly Number Num;
        private readonly string Op;

        public StoredObject(Number num)
        {
            IsOperator = false;
            Num = num;
            Op = "";
        }
        public StoredObject(string op)
        {
            IsOperator = true;
            Num = null;
            Op = op;
        }
        public static implicit operator Number(StoredObject a) 
        {
            if (!a.IsOperator)
            {
                return a.Num;
            }
            else 
            {
                throw new ArgumentException("'a' is not storing a Number");
            }
        }
        public static implicit operator string(StoredObject a)
        {
            if (a.IsOperator)
            {
                return a.Op;
            }
            else
            {
                throw new ArgumentException("'a' is not storing an operator");
            }
        }
        public override string ToString() 
        {
            if (IsOperator)
            {
                return Op;
            }
            else 
            {
                return Num.ToString();
            }
        }
    }
}
