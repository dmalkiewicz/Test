using System;
using System.Linq;
using NeuralNetworks.UnitTests.Neurons.Abstracts;
using NeuralNetworks.UnitTests.Neurons.Enums;
using NeuralNetworks.UnitTests.Neurons.Interfaces;

namespace NeuralNetworks.UnitTests.Neurons.Concretes
{
    /// <summary>This class represents a basic linear neuron.</summary>
    /// <remarks>
    /// It is a basic element of a neural network.
    /// It is a equivalent of real biological neuron.
    /// Note that this is only a simple model
    /// whose characteristics great reflect real events
    /// that occur in every neuron placed in brain.
    /// This is achieved by remembering the neuron weights for inputs.
    /// Every input has its own weight which clearly is showing how positively or negatively
    /// neuron response for given signal on input.
    /// </remarks>
    public class BasicNeuron : INeuron
    {
        #region Fields

        #region Error Messages

        private const string NormNotImplemented = "Specified StrengthNorm is not implemented";

        private const string InputsAndWeightsCountAreNotEqual = "Inputs have to have the same count as the weights count.";

        #endregion

        #endregion

        #region Constructors

        /// <summary>Initialises a new instance of the <see cref="BasicNeuron"/> class.
        /// This class is a basic neuron with specified number of inputs.</summary>
        /// <param name="inputCount">The number of inputs for neuron.</param>
        /// <remarks>Note: The array of weights will have exactly the same size as the input count.</remarks>
        public BasicNeuron(int inputCount)
        {
            this.Weights = new double[inputCount];
            this.PrevWeights = new double[this.Weights.Length];
            this.NeuronGuid = Guid.NewGuid();
        }

        #endregion

        #region Properties

        /// <summary>Gets a neuron Guid.</summary>
        public Guid NeuronGuid { get; protected set; }

        /// <summary>Gets an array which stores weights for each of neuron's inputs.</summary>
        public double[] Weights { get; protected set; }

        /// <summary>Gets an array which stores previous weights for each of neuron's inputs.</summary>
        public double[] PrevWeights { get; protected set; }

        #endregion

        #region Methods (public)

        #region Static methods

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
            double strength = 0;
            switch (norm)
            {
                case StrengthNorm.Euclidean:
                    strength += signals.Sum(s => s * s);
                    return Math.Sqrt(strength);

                case StrengthNorm.Manhattan:
                    strength += signals.Sum(s => Math.Abs(s));
                    return strength;

                default:
                    throw new NotImplementedException(NormNotImplemented);
            }
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

        #endregion

        /// <summary>Calculates the neuron response for given signal.</summary>
        /// <param name="inputSignals">The signals for neuron inputs.
        /// Input array and weight array lengths must match.</param>
        /// <returns>The response of neuron for specified signals.</returns>
        public virtual double Response(double[] inputSignals)
        {
            // Validate the input signals and the check if number of input Equals the number of weights.
            if (inputSignals == null || inputSignals.Length != this.Weights.Length)
            {
                throw new ArgumentException(InputsAndWeightsCountAreNotEqual);
            }

            return this.Weights.Select((t, i) => t * inputSignals[i]).Sum();
        }

        /// <summary>Calculates the memory trace strength.</summary>
        /// <param name="norm">Norm which will be used to calculate the strength (Manhattan or Euclidean).</param>
        /// <returns>Calculated memory trace strength.</returns>
        /// <remarks>
        /// Power memory trace determines how neuron is determined.
        /// The greater the power of memory trace, the more drastic will be neuron opinions.
        /// </remarks>
        public double MemoryTraceStrength(StrengthNorm norm)
        {
            return Strength(this.Weights, norm);
        }

        /// <summary>Teaches neuron how to response for given signal. It is for a "learning with a teacher".</summary>
        /// <param name="signals">The signal, for which neuron have to respond.</param>
        /// <param name="expectedOutput">Specifies what should be neuron appropriate response.</param>
        /// <param name="ratio">Learning factor.
        /// The bigger, the faster will be a learning process.
        /// But be careful, because too large will make neuron will be struggling.
        /// This factor should also be considered as the size of the penalty for giving wrong answers by neuron.</param>
        /// <param name="previousResponse">The method puts it on a given neuron response signal before learning.</param>
        /// <param name="previousError">The method puts the size of the error of the neuron before learning.</param>
        public void Learn(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError)
        {
            previousResponse = this.Response(signals);
            previousError = expectedOutput - previousResponse;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += ratio * previousError * signals[i];
            }
        }

        public void Learn(double[] signals, double[] previousWeights, double error, double sigma, double ratio, double momentum)
        {
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += ratio * sigma * signals[i] - momentum * (this.Weights[i] - previousWeights[i]);
            }
        }

        public void RandomizeWeights(Random randomGenerator, double min, double max)
        {
            var length = max - min;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] = min + length * randomGenerator.NextDouble();
            }
        }

        public void RandomizeWeights(Random randomGenerator, double min, double max, double epsilon)
        {
            var length = max - min;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] = min + length * randomGenerator.NextDouble();
                if (epsilon > Math.Abs(this.Weights[i]))
                {
                    this.Weights[i] = epsilon;
                }
            }
        }

        #endregion
    }
}