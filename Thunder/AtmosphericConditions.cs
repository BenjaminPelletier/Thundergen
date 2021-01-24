using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Thunder
{
    public class AtmosphericConditions
    {
        /// <summary>
        /// Ambient atmospheric temperature (degrees K)
        /// </summary>
        public double Temperature;

        /// <summary>
        /// Relative humidity (percent)
        /// </summary>
        public double RelativeHumidity;

        /// <summary>
        /// Ambient atmospheric pressure (kPa)
        /// </summary>
        public double Pressure;

        const double R = 8.314;
        const double GAMMA = 1.4; // Oxygen, Nitrogen

        /// <summary>
        /// Molar mass (kg/mol)
        /// </summary>
        const double M = 0.02895; //TODO: this is not actually a constant

        public AtmosphericConditions(double temperatureK = 293.15, double relativeHumidity = 70, double pressurePa = 101325)
        {
            Temperature = temperatureK;
            RelativeHumidity = relativeHumidity;
            Pressure = pressurePa;
        }

        /// <remarks>
        /// http://hyperphysics.phy-astr.gsu.edu/hbase/Sound/souspe3.html#c1
        /// v_sound = sqrt(gamma * R * T / M)
        /// R = 8.314 J/mol/K
        /// M = 0.02895 kg/mol (dry air)
        /// gamma = 1.4
        /// </remarks>
        public double SpeedOfSound(double pressureRatio = 1.0)
        {
            double dTperT = (GAMMA - 1) / GAMMA * pressureRatio;
            return Math.Sqrt(GAMMA * R * Temperature * (1.0 + dTperT) / M);
        }
    }
}
