using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Thundergen.Thunder
{
    public class Waveform
    {
        public delegate double FrequencyScaling(double frequency);

        public interface Absorption
        {
            FrequencyScaling GetScaling(double distanceM);
        }

        public double T0;
        public readonly int SampleRate;
        public double[] Samples;

        public Waveform(int sampleRate, double t0, double t1)
        {
            SampleRate = sampleRate;
            T0 = t0;
            Samples = new double[(int)Math.Floor((t1 - t0) * sampleRate) + 1];
        }
        
        public Waveform(int sampleRate, int sampleCount, double t0)
        {
            SampleRate = sampleRate;
            T0 = t0;
            Samples = new double[sampleCount];
        }

        public Waveform(int sampleRate, IEnumerable<double> samples, double t0 = 0)
        {
            SampleRate = sampleRate;
            T0 = t0;
            Samples = samples.ToArray();
        }

        public void Normalize()
        {
            double divisor = Samples.Select(v => Math.Abs(v)).Max();
            for (int i = 0; i < Samples.Length; i++)
            {
                Samples[i] /= divisor;
            }
        }

        public Waveform Normalized()
        {
            var result = new Waveform(SampleRate, Samples, T0);
            result.Normalize();
            return result;
        }

        public void TruncateBefore(double t)
        {
            int i0 = (int)Math.Round((t - T0) * SampleRate);
            var newSamples = new double[Samples.Length - i0];
            Array.Copy(Samples, i0, newSamples, 0, newSamples.Length);
            Samples = newSamples;
            T0 += (double)i0 / SampleRate;
        }

        public WaveStream ToStream()
        {
            var raw = new byte[2 * Samples.Length];

            for (int i = 0; i < Samples.Length; i++)
            {
                var sample = (short)(Math.Min(1, Math.Max(-1, Samples[i])) * Int16.MaxValue);
                var bytes = BitConverter.GetBytes(sample);
                raw[i * 2] = bytes[0];
                raw[i * 2 + 1] = bytes[1];
            }

            var ms = new MemoryStream(raw);
            var rs = new RawSourceWaveStream(ms, new WaveFormat(SampleRate, 16, 1));
            return rs;
        }

        public void WriteToWAV(string filename, bool normalized = true)
        {
            WaveFileWriter.CreateWaveFile(filename, (normalized ? Normalized() : this).ToStream());
        }

        public void Add(Waveform other, double amplitude = 1)
        {
            if (other.SampleRate != this.SampleRate)
            {
                throw new NotImplementedException("Cannot yet add waveforms with different sampler rates");
            }

            if (other.T0 < this.T0)
            {
                int newSamples = (int)Math.Ceiling((this.T0 - other.T0) * this.SampleRate);
                var newOrigin = new Waveform(this.SampleRate, this.Samples.Length + newSamples, this.T0 - (double)newSamples / this.SampleRate);
                Array.Copy(this.Samples, 0, newOrigin.Samples, newSamples, this.Samples.Length);
                this.Samples = newOrigin.Samples;
                this.T0 = newOrigin.T0;
            }

            int i0 = (int)Math.Round((other.T0 - this.T0) * this.SampleRate);
            int i1 = i0 + other.Samples.Length;
            if (i1 > Samples.Length)
            {
                var newSamples = new double[i1];
                Array.Copy(Samples, 0, newSamples, 0, Samples.Length);
                Samples = newSamples;
            }
            for (int i = i0; i < i1; i++)
            {
                this.Samples[i] += other.Samples[i - i0] * amplitude;
            }
        }

        public void Filter(FrequencyScaling freqScaling, double pad = 0, bool expand = false)
        {
            int n = (int)Math.Pow(2, Math.Ceiling(Math.Log((2 * pad + 1) * Samples.Length) / Math.Log(2)));
            var signal = new System.Numerics.Complex[n];
            int i0 = (n - Samples.Length) / 2;
            for (int i = 0; i < Samples.Length; i++)
            {
                signal[i0 + i] = new System.Numerics.Complex(Samples[i], 0);
            }
            Accord.Math.FourierTransform.FFT(signal, Accord.Math.FourierTransform.Direction.Forward);

            for (int i = 1; i < signal.Length / 2; i++)
            {
                double freq = i * SampleRate / signal.Length;
                double scale = freqScaling(freq);
                signal[i] *= scale;
                signal[signal.Length - i] *= scale;
            }

            Accord.Math.FourierTransform.FFT(signal, Accord.Math.FourierTransform.Direction.Backward);

            if (expand)
            {
                T0 -= i0 / (double)SampleRate;
                Samples = signal.Select(v => v.Real).ToArray();
            }
            else
            {
                for (int i = 0; i < Samples.Length; i++)
                {
                    Samples[i] = signal[i0 + i].Real;
                }
            }
        }

        public Waveform Filtered(FrequencyScaling freqScaling, double pad = 0)
        {
            var result = new Waveform(SampleRate, Samples, T0);
            result.Filter(freqScaling, pad);
            return result;
        }

        public void Pad(int newLength)
        {
            if (newLength < Samples.Length)
            {
                throw new ArgumentException("Cannot shorten a Waveform with Pad");
            }
            var newSamples = new double[newLength];
            int i0 = (newLength - Samples.Length) / 2;
            for (int i = 0; i < Samples.Length; i++)
            {
                newSamples[i + i0] = Samples[i];
            }
            Samples = newSamples;
            T0 -= (double)i0 / SampleRate;
        }

        public void Scale(double f)
        {
            for (int i = 0; i < Samples.Length; i++)
            {
                Samples[i] *= f;
            }
        }

        public Waveform Scaled(double f)
        {
            var result = new Waveform(SampleRate, Samples, T0);
            for (int i = 0; i < Samples.Length; i++)
            {
                result.Samples[i] *= f;
            }
            return result;
        }

        public static Waveform WMWave(Vector3 p0, Vector3 p1, Vector3 observer, double T0 = 0.0025, double r0 = 0, double A = 1)
        {
            const double c = 343; // speed of sound, meters per second
            Vector3 directionSegment = p1 - p0;
            double l = directionSegment.Length();
            Vector3 center = (p0 + p1) / 2;
            Vector3 directionListener = observer - center;
            double r = r0 == 0 ? directionListener.Length() : r0;
            double phi = c * T0 / l;
            directionListener /= directionListener.Length();
            directionSegment /= directionSegment.Length();
            double sinTheta = Math.Sin(Math.Acos(Vector3.Dot(directionListener, directionSegment)));
            double B = A * l * l / (2 * r * c * T0);
            double tau0 = -phi - sinTheta;
            double tau3 = phi + sinTheta;
            double tau1, tau2;
            if (sinTheta < phi)
            {
                tau1 = -phi + sinTheta;
                tau2 = phi - sinTheta;

            }
            else
            {
                tau1 = phi - sinTheta;
                tau2 = -phi + sinTheta;
            }
            double t0 = (l * tau0 + r) / c;
            double t1 = (l * tau1 + r) / c;
            double t2 = (l * tau2 + r) / c;
            double t3 = (l * tau3 + r) / c;

            var wav = new Waveform(44100, t0, t3);
            int i = 0;
            for (double t = t0; t <= t3; t += 1.0 / 44100)
            {
                double tau = (c * t - r) / l;
                double v;
                if (t < t1)
                {
                    v = (-B / sinTheta) * (Math.Pow(tau + sinTheta, 2) - phi * phi);
                }
                else if (t < t2)
                {
                    if (sinTheta < phi)
                    {
                        v = -4 * B * tau;
                    }
                    else
                    {
                        v = 0;
                    }
                }
                else
                {
                    v = (B / sinTheta) * (Math.Pow(tau - sinTheta, 2) - phi * phi);
                }
                wav.Samples[i++] = v;
            }
            return wav;
        }
    }
}
