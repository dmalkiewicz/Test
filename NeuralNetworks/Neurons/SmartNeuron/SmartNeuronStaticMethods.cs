using System;
using System.Linq;
using NeuralNetworks.Neurons.Enums;

namespace NeuralNetworks.Neurons.SmartNeuron
{
    public partial class SmartNeuron : ISmartNeuron
    {
        /// <summary>Calculates the power of the given signal, using the specified measure.</summary>
        /// <param name="signals">The signal whose strength is calculated.</param>
        /// <param name="norm">Norm that strength is measured (Euclidean or Manhattan).</param>
        /// <returns>Strength of the neuron.</returns>
        /// <remarks>
        /// The signal strength affects how it's important it is for this neuron.
        /// Giving the weighting as inputs will produce the memory trace - a value
        /// which indicates how neuron is decided.
        /// </remarks>
        public static double Strength(double[] signals, StrengthNorm norm)
        {
            if (signals == null)
            {
                throw new ArgumentNullException();
            }

            double strength = 0;
            if (norm == StrengthNorm.Euclidean)
            {
                strength += signals.Sum(s => s * s);
                return Math.Sqrt(strength);
            }

            strength += signals.Sum(s => Math.Abs(s));
            return strength;
        }

        /// <summary>Normalizes the specified signals.</summary>
        /// <param name="signals">The signal which is going to be normalized.</param>
        /// <remarks>
        /// The input array is scaled so as to have a strength equals 1 (in accordance with Euclidean norm).
        /// </remarks>
        public static void Normalize(double[] signals)
        {
            var strength = Strength(signals, StrengthNorm.Euclidean);
            for (var i = 0; i < signals.Length; i++)
            {
                signals[i] /= strength;
            }
        }
    }
}
