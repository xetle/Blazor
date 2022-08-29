namespace Eccentricity
{
    /// <summary>
    /// Calculate Cnk type binomials (obviously involves factorials)
    /// </summary>
    public abstract class BinomialBase
    {
        public abstract Fraction Coeff();

        protected int Factorial(int x)
        {
            int fact = 1;
            while (x > 1)
            {
                fact *= x;
                x--;
            }
            return fact;
        }
    }
    public class PositiveBinomial : BinomialBase
    {
        readonly int n;
        readonly int k;
        public PositiveBinomial(int n, int k)
        {
            if (n > 4 || n < 0)
            {
                throw new Exception("n must be +ve and < 4");
            }
            if (k > 3)
            {
                throw new Exception("k must be <= 3 as we are only calculating e terms up to powers of 3");
            }
            this.n = n;
            this.k = k;
        }

        public override Fraction Coeff()
        {
            if (n < 0)
            {
                throw new Exception("n should be >= 0");
            }
            else if (n == 0 && k != 0)
            {
                return new Fraction(0, 96);
            }
            else if (n == 0)
            {
                return new Fraction(1, 1);
            }
            var numerator = Factorial(n);
            var denom = Factorial(k) * Factorial(n - k);
            return new Fraction(numerator, denom);
        }
    }
    public class NegativeBinomial : BinomialBase
    {
        readonly int n;
        readonly int k;
        public NegativeBinomial(int n, int k)
        {
            if (n > 0)
            {
                throw new Exception("n must be < 0");
            }
            if (k > 3)
            {
                throw new Exception("k must be <= 3 as we are only calculating e terms up to powers of 3");
            }
            this.n = n;
            this.k = k;
        }

        public override Fraction Coeff()
        {
            if (n == 0 && k != 0)
            {
                return new Fraction(0, 96);
            }
            else if (n == 0 || k == 0)
            {
                return new Fraction(1, 1);
            }
            else
            {
                var denominator = Factorial(k);
                var terms = k;
                int fact = n;
                for (int i = 1; i < terms; i++)
                {
                    fact *= (n - i);
                }
                return new Fraction(fact, denominator);
            }
        }
    }
}
