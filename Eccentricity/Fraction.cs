using System;

namespace Eccentricity
{
    public class Fraction
    {
        public const int LCD = 96;

        public Fraction(int num, int denom)
        {
            Numerator = num;
            Denominator = denom;
        }

        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public bool Exists
        {
            get { return Numerator != 0; }
        }

        public Fraction Multiply(Fraction other)
        {
            var ansDenom = other.Denominator * Denominator;
            var ansNumerator = other.Numerator * Numerator;
            return new Fraction(ansNumerator, ansDenom);
        }

        public Fraction Add(Fraction other)
        {
            if (Denominator != LCD || other.Denominator != LCD)
            {
                throw new Exception("Cannot Sum");
            }
            var ansNumerator = other.Numerator + Numerator;
            return new Fraction(ansNumerator, Denominator);
        }

        public override string ToString()
        {
            var output = string.Format("{0}/{1}", Numerator, Denominator);
            return output;
        }

        /// <summary>
        /// Make denominator 96 so we can add fractions
        /// </summary>
        public Fraction CommonDenominator
        {
            // uses the new - more concise - switch expression in c# 8.0
            // also uses the new discard (unassigned variable)
            get
            {
                return Denominator switch
                {
                    1 => new Fraction(Numerator * LCD, Denominator * LCD),
                    2 => new Fraction(Numerator * 48, Denominator * 48),
                    3 => new Fraction(Numerator * 32, Denominator * 32),
                    4 => new Fraction(Numerator * 24, Denominator * 24),
                    6 => new Fraction(Numerator * 16, Denominator * 16),
                    8 => new Fraction(Numerator * 12, Denominator * 12),
                    12 => new Fraction(Numerator * 8, Denominator * 8),
                    16 => new Fraction(Numerator * 6, Denominator * 6),
                    24 => new Fraction(Numerator * 4, Denominator * 4),
                    32 => new Fraction(Numerator * 3, Denominator * 3),
                    48 => new Fraction(Numerator * 2, Denominator * 2),
                    LCD => this,
                    _ => throw new Exception("Unexpected Denominator"),
                };
            }
        }

        public Fraction LowestDenominator()
        {
            var divisor = GCD(Math.Abs(Numerator), Denominator);
            if (divisor != 1)
            {
                var reducedNumerator = Numerator / divisor;
                var reducedDenominator = Denominator / divisor;
                return new Fraction(reducedNumerator, reducedDenominator);
            }
            else
                return this;
        }

        //defines function that calculates the gcd with parameters a and b
        private static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }
    }
}
