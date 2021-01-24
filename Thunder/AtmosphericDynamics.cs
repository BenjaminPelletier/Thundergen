using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Thunder
{
    class AtmosphericDynamics
    {
        AtmosphericConditions Conditions;

        public AtmosphericDynamics(AtmosphericConditions conditions)
        {
            Conditions = conditions;
        }

        /// <remarks>
        /// https://physics.stackexchange.com/questions/134759/propagation-generation-of-sound-is-an-isentropic-process
        /// dT/T = (gamma - 1) / gamma * dP/P
        /// P = 101325 Pa
        /// </remarks>
        public double SteepeningDistance(double sampleRate, double pMin, double pMax, double diMax = 0.5)
        {
            double Pref = Conditions.Pressure;

            double c0 = Conditions.SpeedOfSound();

            double c = Conditions.SpeedOfSound(pMax / Pref);
            double dc = 1 / c - 1 / c0;
            double dh = dc < 0 ? -diMax / sampleRate / dc : double.PositiveInfinity;

            c = Conditions.SpeedOfSound(pMin / Pref);
            dc = 1 / c - 1 / c0;
            double dl = dc > 0 ? diMax / sampleRate / dc : double.PositiveInfinity;

            return Math.Min(dh, dl);
        }


        public Waveform Steepen(Waveform wave, double distance)
        {
            double Pref = Conditions.Pressure;

            var result = new Waveform(wave.SampleRate, wave.Samples.Length, wave.T0);

            double c0 = Conditions.SpeedOfSound();
            double t0 = distance / c0;
            for (int i = 0; i < wave.Samples.Length; i++)
            {
                double c = Conditions.SpeedOfSound((wave.Samples[i] + Pref) / Pref);
                double t1 = c0 / c * t0;
                double it = i + (t1 - t0) * wave.SampleRate;
                int i0 = (int)Math.Floor(it);
                int i1 = i0 + 1;
                double f = it - i0;
                if (i0 >= 0 && i0 < wave.Samples.Length) result.Samples[i0] += wave.Samples[i] * (1 - f);
                if (i1 >= 0 && i1 < wave.Samples.Length) result.Samples[i1] += wave.Samples[i] * f;
            }

            return result;
        }
    }
}
