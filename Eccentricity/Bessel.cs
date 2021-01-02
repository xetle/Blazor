using System;

namespace Eccentricity
{
    /// <summary>
    /// Bessel classes show the coefficients of e terms got from Jn(ke) i.e. for different k and for different orders
    /// </summary>
    public abstract class BesselBase
    {
        public int k;
        public BesselBase(int k)
        {
            this.k = k;
        }
        public virtual Fraction Term0
        {
            get { return new Fraction(0, 1); }
        }
        public virtual Fraction Term1
        {
            get { return new Fraction(0, 1); }
        }
        public virtual Fraction Term2
        {
            get { return new Fraction(0, 1); }
        }
        public virtual Fraction Term3
        {
            get { return new Fraction(0, 1); }
        }
    }

    public class Bessel0 : BesselBase
    {
        public Bessel0(int k) : base(k) { }
        public override Fraction Term0
        {
            get
            {
                return new Fraction(1, 1); // true for k=0 as well
            }
        }
        public override Fraction Term2
        {
            get
            {
                if (k == 0)
                    return new Fraction(0, 1);
                else
                    return new Fraction((int)Math.Pow(k, 2) * -1, 4);
            }
        }
    }
    public class Bessel1 : BesselBase
    {
        public Bessel1(int k) : base(k) { }
        public override Fraction Term1
        {
            get
            {
                if (k == 0)
                    return new Fraction(0, 1);
                else
                    return new Fraction(k, 2);
            }
        }
        public override Fraction Term3
        {
            get
            {
                if (k == 0)
                    return new Fraction(0, 1);
                else
                    return new Fraction((int)Math.Pow(k, 3) * -1, 16);
            }
        }
    }

    public class Bessel2 : BesselBase
    {
        public Bessel2(int k) : base(k) { }
        public override Fraction Term2
        {
            get
            {
                if (k == 0)
                    return new Fraction(0, 1);
                else
                    return new Fraction((int)Math.Pow(k, 2) * 1, 8);
            }
        }
    }

    public class Bessel3 : BesselBase
    {
        public Bessel3(int k) : base(k) { }
        public override Fraction Term3
        {
            get
            {
                if (k == 0)
                    return new Fraction(0, 1);
                else
                {
                    var result = (int)Math.Pow(k, 3) * 1;
                    return new Fraction(result, 48);
                }
            }
        }
    }
}
