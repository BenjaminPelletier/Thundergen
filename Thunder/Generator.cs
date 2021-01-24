using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Thundergen.Lightning;

namespace Thundergen.Thunder
{
    public class Generator
    {
        public class Config
        {
            public Vector3 ObserverLocation = Vector3.Zero;

            /// <summary>
            /// Initial maximum pressure amplitude of acoustic wave (dBSPL)
            /// </summary>
            public double InitialVolume = 180;

            /// <summary>
            /// Distance from bolt at which InitialVolume applies (meters)
            /// </summary>
            public double R0 = 10;

            public FollowingStroke[] FollowingStrokes = null;

            public AtmosphericConditions Conditions = null;

            public bool NoAtmosphericAttenuation = false;

            /// <summary>
            /// The maximum change in sample index for a propagation step due to non-linear steepening.
            /// A value of zero disables application of the steepening effect.
            /// </summary>
            public double MaximumSteepeningIndexShift = 0.5;
        }

        public class ProgressEventArgs : EventArgs
        {
            public int TotalSegments;
            public int SegmentsComplete;

            public ProgressEventArgs(int totalSegments, int segmentsComplete)
            {
                TotalSegments = totalSegments;
                SegmentsComplete = segmentsComplete;
            }
        }

        /// <remarks>
        /// https://en.wikipedia.org/wiki/Sound_pressure
        /// dBSPL = 20 * log_10(p / p0)
        /// p0 = 20e-6 Pa
        /// </remarks>
        public static Waveform Generate(IEnumerable<Vector3> bolt, Config config, EventHandler<ProgressEventArgs> progress = null)
        {
            AtmosphericAttenuation attenuation = config.NoAtmosphericAttenuation ? null : new AtmosphericAttenuation(config.Conditions);
            AtmosphericDynamics dynamics = config.MaximumSteepeningIndexShift == 0 ? null : new AtmosphericDynamics(config.Conditions);
            double R0 = config.R0;
            const double Pref = 20e-6; // Pa
            double C0 = config.Conditions != null ? config.Conditions.SpeedOfSound() : 343; // meters / s

            //TODO: Include ground reflection?

            var thunder = new Waveform(44100, 0, 0.0);
            double delay = double.PositiveInfinity;
            TimeSpan UPDATE_PERIOD = TimeSpan.FromSeconds(1);
            DateTime nextUpdate = DateTime.UtcNow + UPDATE_PERIOD;
            int segmentsCompleted = 0;

            var boltPoints = bolt.ToArray();
            Parallel.For(1, boltPoints.Length, i =>
            //for (int i = 1; i < boltPoints.Length; i++)
            {
                Vector3 p0 = boltPoints[i - 1];
                Vector3 p = boltPoints[i];
                double totalDistance = ((p + p0) / 2 - config.ObserverLocation).Length() - R0;
                double l = (p - p0).Length();
                double initialPressure = Math.Pow(10, config.InitialVolume / 20) * Pref;
                double A = initialPressure * R0 / (2 * l);
                Waveform wm = Waveform.WMWave(p0, p, config.ObserverLocation, r0: R0, A: A);
                wm.Pad((int)Math.Pow(2, Math.Ceiling(Math.Log(wm.Samples.Length) / Math.Log(2)) + 1));
                double distanceTraveled = R0;
                while (distanceTraveled < totalDistance)
                {
                    double distanceStep = totalDistance - distanceTraveled;
                    if (dynamics != null)
                    {
                        double pMin = double.PositiveInfinity;
                        double pMax = double.NegativeInfinity;
                        for (int s = 0; s < wm.Samples.Length; s++)
                        {
                            double v = wm.Samples[s];
                            if (v < pMin) pMin = v;
                            if (v > pMax) pMax = v;
                        }
                        distanceStep = Math.Min(distanceStep, dynamics.SteepeningDistance(wm.SampleRate, pMin, pMax, config.MaximumSteepeningIndexShift));
                        wm = dynamics.Steepen(wm, distanceStep);
                    }
                    if (attenuation != null)
                    {
                        wm.Filter(attenuation.GetScaling(distanceStep));
                    }
                    wm.Scale(distanceTraveled / (distanceTraveled + distanceStep));
                    wm.T0 += distanceStep / C0;
                    distanceTraveled += distanceStep;
                }
                bool updateProgress = false;
                lock (thunder)
                {
                    thunder.Add(wm);
                    if (progress != null && DateTime.UtcNow >= nextUpdate)
                    {
                        updateProgress = true;
                        while (DateTime.UtcNow >= nextUpdate)
                        {
                            nextUpdate += UPDATE_PERIOD;
                        }
                    }
                    segmentsCompleted++;
                }
                if (updateProgress)
                {
                    progress.Invoke(null, new ProgressEventArgs(boltPoints.Length, segmentsCompleted));
                }
                delay = Math.Min(delay, wm.T0);
            });
            thunder.TruncateBefore(delay);

            if (config.FollowingStrokes == null || config.FollowingStrokes.Length == 0)
            {
                thunder.Normalize();
                return thunder;
            }

            var composite = new Waveform(44100, thunder.Samples.Length, thunder.T0);
            composite.Add(thunder);
            foreach (FollowingStroke stroke in config.FollowingStrokes)
            {
                thunder.T0 += stroke.Interval;
                composite.Add(thunder, stroke.RelativeAmplitude);
            }
            composite.Normalize();
            return composite;
        }
    }
}
