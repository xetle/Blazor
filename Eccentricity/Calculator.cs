using System.Diagnostics;

namespace Eccentricity
{
    public enum Key
    {
        k1,
        k2,
        Sum
    }

    public class Calculator
    {
        const int l = 2;

        public Dictionary<Key, List<Fraction>> Calculate(int p, int k)
        {
            if (p < 0 || p > 2)
            {
                throw new ArgumentException($"p must be 0, 1 or 2");
            }

            // coefficients of powers of e for the beta term
            var betaList = new List<BetaBase>
            {
                new Beta0(),
                new Beta1(),
                new Beta2(),
                new Beta3(),
            };

            var besselList = new List<BesselBase>
            {
                new Bessel0(k),
                new Bessel1(k),
                new Bessel2(k),
                new Bessel3(k),
            };

            Func<int, int, int> besselOrderFuncPosExp = (b, d) => { return k - l + 2 * p - b + d; };
            Func<int, int, int> besselOrderFuncNegExp = (b, d) => { return k + l - 2 * p + b - d; };

            var bUpper = -(4 - 2 * p); // binom upper coeff
            var dUpper = -2 * p; // binom upper coeff

            var bMax = bUpper == 0 ? 0 : 3;
            var dMax = dUpper == 0 ? 0 : 3;

            var resultsPos = new List<Result>();
            var resultsNeg = new List<Result>();
            for (int b = 0; b <= bMax; b++)
            {
                for (int d = 0; d <= dMax; d++)
                {
                    if ((b + d) < 4)
                    {
                        var resultPos = CalculateCoefficient(b, d, besselOrderFuncPosExp, bUpper, dUpper, betaList, besselList);
                        resultsPos.Add(resultPos);

                        var resultNeg = CalculateCoefficient(b, d, besselOrderFuncNegExp, bUpper, dUpper, betaList, besselList);
                        resultsNeg.Add(resultNeg);
                    }
                }
            }

            var answer = $"k={k}, 1st:{WriteOutput(resultsPos)} , 2nd:{WriteOutput(resultsNeg)}";
            Debug.Write(answer);

            var allResults = resultsPos.Union(resultsNeg).ToList();

            var k1Series = GetFinalSeries(resultsPos);
            var k2Series = GetFinalSeries(resultsNeg);

            var sumSeries = GetFinalSeries(allResults);

            var dictResults = new Dictionary<Key, List<Fraction>>
            {
                { Key.k1, k1Series },
                { Key.k2, k2Series },
                { Key.Sum, sumSeries }
            };

            return dictResults;
        }

        public Result CalculateCoefficient(int b, int d, Func<int, int, int> besselOrderFunc, int bUpper, int dUpper, List<BetaBase> betaList, List<BesselBase> besselList)
        {
            var besselOrder = besselOrderFunc(b, d);

            var result = new Result(b, d);

            var coeffB = bUpper < 0 ? new NegativeBinomial(bUpper, b).Coeff() : new PositiveBinomial(bUpper, b).Coeff();
            var coeffD = dUpper < 0 ? new NegativeBinomial(dUpper, d).Coeff() : new PositiveBinomial(dUpper, d).Coeff();

            var bdSum = b + d;
            var sign = (int)Math.Pow(-1, bdSum);

            var betaTerms = new List<Fraction>
            {
                betaList[bdSum].Term0,
                betaList[bdSum].Term1,
                betaList[bdSum].Term2,
                betaList[bdSum].Term3
            };

            int besselOrderAbs = Math.Abs(besselOrder);

            bool negativeBesselOrder = besselOrder < 0;

            if (besselOrderAbs < 4) // only get terms up to powers of 3
            {
                int signBessel = 1;
                if (negativeBesselOrder)
                {
                    signBessel = (int)Math.Pow(-1, besselOrderAbs); // J-n(x) = (-1)^n * Jn(x)
                }
                var besselTerms = new List<Fraction>
                {
                    besselList[besselOrderAbs].Term0,
                    besselList[besselOrderAbs].Term1,
                    besselList[besselOrderAbs].Term2,
                    besselList[besselOrderAbs].Term3
                };

                // There are extra e power terms due to the second term in (1 + e^2/2)
                var betaOrder2Factor = new Fraction(1, 2);

                // the powers of e come from products of the beta and the bessel terms. We are only considering terms up to e^3
                for (int i = 0; i <= 3; i++) // range over beta terms
                {
                    for (int j = 0; j <= 3; j++) // range over bessel terms
                    {
                        var ePower = i + j;
                        if (ePower <= 3)
                        {
                            var btTerms = betaTerms[i];
                            var bsTerms = besselTerms[j];
                            var frac = btTerms.Multiply(bsTerms);
                            frac = frac.Multiply(coeffB).Multiply(coeffD);
                            frac.Numerator *= sign;
                            frac.Numerator *= signBessel;

                            frac.Denominator *= 2;

                            var frac2 = frac.Multiply(betaOrder2Factor);
                            if (frac.Numerator != 0)
                            {
                                switch (ePower)
                                {
                                    case 0:
                                        result.eCoefficients[0].Add(frac);
                                        result.eCoefficients[2].Add(frac2);
                                        break;
                                    case 1:
                                        result.eCoefficients[1].Add(frac);
                                        result.eCoefficients[3].Add(frac2);
                                        break;
                                    case 2:
                                        result.eCoefficients[2].Add(frac);
                                        break;
                                    case 3:
                                        result.eCoefficients[3].Add(frac);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public List<Fraction> GetFinalSeries(List<Result> results)
        {
            var finalSeries = new List<Fraction>(); // holds each of the coefficients of powers of e after processing all the possible h/l combinations

            for (int i = 0; i < 4; i++)
            {
                var fracResult = new Fraction(0, Fraction.LCD);
                var fractions = results.SelectMany(x => x.eCoefficients[i]);

                foreach (var fraction in fractions)
                {
                    if (fraction.Numerator != 0)
                    {
                        fracResult = fracResult.Add(fraction.CommonDenominator);
                    }
                }
                finalSeries.Add(fracResult.LowestDenominator());
            }

            return finalSeries;
        }

        public string WriteOutput(List<Result> results)
        {
            var finalSeries = GetFinalSeries(results);

            string output = string.Empty;
            for (int i = 0; i < finalSeries.Count; i++)
            {
                var term = finalSeries[i];
                if (term.Numerator != 0)
                {
                    output += term.ToString() + "e^" + i + " + ";
                }
            }
            if (!string.IsNullOrEmpty(output))
            {
                output = output.Substring(0, output.Length - 3);
            }

            return output;

        }
    }
}
