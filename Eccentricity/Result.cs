namespace Eccentricity
{
    public class Result
    {
        public int b { get; set; }
        public int d { get; set; }

        public List<List<Fraction>> eCoefficients;

        public Result(int b, int d)
        {
            this.b = b;
            this.d = d;

            eCoefficients = new List<List<Fraction>>()
            {
                new List<Fraction>(),
                new List<Fraction>(),
                new List<Fraction>(),
                new List<Fraction>(),
            };
        }
    }
}
