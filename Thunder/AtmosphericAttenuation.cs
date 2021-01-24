using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Thunder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://www.sengpielaudio.com/AirdampingFormula.htm
    /// </remarks>
    class AtmosphericAttenuation : Waveform.Absorption
    {
        private AtmosphericConditions Conditions;

        /// <summary>
        /// Reference temperature (degrees Kelvin)
        /// </summary>
        const double To = 293.15;

        /// <summary>
        /// Triple-point isotherm temperature (degrees Kelvin)
        /// </summary>
        const double To1 = 273.16;

        /// <summary>
        /// Reference ambient atmospheric pressure (Pa)
        /// </summary>
        const double pr = 101325;

        static double x = 1 / (10 * Math.Pow(Math.Log10(Math.Exp(1)), 2));

        public AtmosphericAttenuation(AtmosphericConditions conditions)
        {
            Conditions = conditions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">Distance through which the sound propagates (m)</param>
        /// <param name="f">Frequency for which to find the scaling factor (Hz)</param>
        /// <returns>Fractional amplitude of specified frequency after specified distance</returns>
        public double FrequencyScale(double s, double f)
        {
            double T = Conditions.Temperature;
            double hr = Conditions.RelativeHumidity;
            double pa = Conditions.Pressure;
            double psat = pr * Math.Pow(10, -6.8346 * Math.Pow(To1 / T, 1.261) + 4.6151); // Saturation vapor pressure
            double h = hr * psat / pa; // Molor concentration of water vapor, as a percentage
            double frN = (pa / pr) * Math.Pow(T / To, -1 / 2) * (9 + 280 * h * Math.Exp(-4.170 * (Math.Pow(T / To, -1 / 3) - 1))); // Nitrogen relaxation frequency
            double frO = (pa / pr) * (24 + 4.04e4 * h * ((0.02 + h) / (0.391 + h))); // Oxygen relaxation frequency
            double z = 0.1068 * Math.Exp(-3352 / T) * Math.Pow(frN + f * f / frN, -1);
            double y = Math.Pow(T / To, -5 / 2) * (0.01275 * Math.Exp(-2239.1 / T) * Math.Pow(frO + f * f / frO, -1) + z);
            double a = 8.686 * f * f * ((1.84e-11 * Math.Pow(pa / pr, -1) * Math.Pow(T / To, 1 / 2)) + y); // Sound attenuation coefficient (dB/m)
            double a_s = a * s; // Total absorption over distance (dB)
            return Math.Exp(-x * a_s);
        }

        public Waveform.FrequencyScaling GetScaling(double distanceM)
        {
            return f => FrequencyScale(distanceM, f);
        }
    }
}
