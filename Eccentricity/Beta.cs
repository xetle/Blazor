namespace Eccentricity
{
    /// <summary>
    /// Beta classes show the coefficients of e terms got by raising Beta to the powers of 0,1,2,3 i.e. h+l. 
    /// Higher powers aren't covered as this would lead to e^4 terms
    /// </summary>
    public abstract class BetaBase
    {
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

    public class Beta0 : BetaBase
    {
        public override Fraction Term0
        {
            get { return new Fraction(1, 1); }
        }
    }

    public class Beta1 : BetaBase
    {
        public override Fraction Term1
        {
            get { return new Fraction(1, 2); }
        }
        public override Fraction Term3
        {
            get { return new Fraction(1, 8); }
        }
    }

    public class Beta2 : BetaBase
    {
        public override Fraction Term2
        {
            get { return new Fraction(1, 4); }
        }
    }

    public class Beta3 : BetaBase
    {
        public override Fraction Term3
        {
            get { return new Fraction(1, 8); }
        }
    }
}
