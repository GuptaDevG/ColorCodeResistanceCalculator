using System;
using EColorCodeResistanceCalculator.Models;

namespace EColorCodeResistanceCalculator.Calculator
{
    public class OhmValueCalculator : IOhmValueCalculator
    {
        public CalculatorResult CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            BandColorCodeMapper bandColorCodeMapper = new BandColorCodeMapper();

            //Map inputs from color code dictionry
            int ohmValue = Convert.ToInt32(string.Format("{0}{1}", bandColorCodeMapper.significantFigures[bandAColor], 
                bandColorCodeMapper.significantFigures[bandBColor]));
            int multiplier = bandColorCodeMapper.multiplier[bandCColor];
            double tolerance = bandColorCodeMapper.tolerance[bandDColor];

            // max and min resistance values
            double resultMax = (ohmValue * Math.Pow(10, multiplier) * (1 + tolerance));
            double resultMin = (ohmValue * Math.Pow(10, multiplier) * (1 - tolerance));

            //Result
            CalculatorResult result = new CalculatorResult();
            result.MinResistance = FormatResistance(resultMin);
            result.MaxResistance = FormatResistance(resultMax);
            
            return result;
        }


        /// <summary>
        /// Format large resistance values
        /// </summary>
        /// <param name="num"> resistance value </param>
        /// <returns>formatted string of a resistance value</returns>
        public string FormatResistance(double num)
        {
            //to show in Mega format 
            if (num >= 100000000)
                return (num / 1000000).ToString("#,0M");

            else if (num >= 10000000)
                return (num / 1000000).ToString("0.#") + "M";
            //to show in Kilo format
            else if (num >= 100000)
                return (num / 1000).ToString("#,0K");

            else if (num >= 10000)
                return (num / 1000).ToString("0.#") + "K";

            else
                return num.ToString();

        }
    }


}
