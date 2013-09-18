using System;
using System.Linq;
using System.Runtime.Serialization;
using NeuralNetworks.Neurons.Enums;
using NeuralNetworks.Neurons.ResponseStrategies;

namespace NeuralNetworks.Neurons
{
    [DataContract]
    public class SimpleNeuron : INeuron
    {
        #region Constructors

        /// <summary>Initialises a new instance of the <see cref="SimpleNeuron"/> class.
        /// This class is a basic neuron with specified number of inputs.</summary>
        /// <param name="inputCount">The number of inputs for neuron.</param>
        /// <param name="responseStrategy">The neuron response strategy.</param>
        /// <param name="responseStrategyFactory">The factory which will create response strategy.</param>
        /// <remarks>Note: The array of weights will have exactly the same size as the input count.</remarks>
        public SimpleNeuron(int inputCount, IResponse responseStrategy, IResponseStrategyFactory responseStrategyFactory)
        {
            responseStrategyFactory = responseStrategyFactory ?? new ResponseStrategyFactory();
            this.ResponseStrategy = responseStrategyFactory.CreateResponseCopy(responseStrategy, this);
            this.Weights = new double[inputCount];
            this.PreviousWeights = new double[this.Weights.Length];
            this.Identifier = Guid.NewGuid();
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets a neuron Guid.</summary>
        [DataMember]
        public Guid Identifier { get; set; }

        /// <summary>Gets an array which stores weights for each of neuron's inputs.</summary>
        [DataMember]
        public double[] Weights { get; private set; }

        /// <summary>Gets an array which stores previous weights for each of neuron's inputs.</summary>
        [DataMember]
        public double[] PreviousWeights { get; private set; }

        /// <summary>Gets or sets the neuron response strategy.</summary>
        [DataMember]
        public IResponse ResponseStrategy { get; set; }

        public double Value
        {
            get { throw new NotImplementedException(); } // TODO
        }

        #endregion

        #region Public static methods

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

        #endregion

        #region Methods (public)

        public void SetWeights(double[] newWeights)
        {
            this.Weights = newWeights;
        }

        /// <summary>Calculates the neuron response for given signal.</summary>
        /// <param name="inputSignals">The signals for neuron inputs.
        /// Input array and weight array lengths must match.</param>
        /// <returns>The response of neuron for specified signals.</returns>
        public double Response(double[] inputSignals)
        {
            return this.ResponseStrategy.Response(inputSignals);
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
            this.PreviousWeights = (double[])this.Weights.Clone();
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += ratio * previousError * signals[i];
            }
        }

        public void Learn(double[] signals, double[] previousWeights, double sigma, double ratio, double momentum)
        {
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += (ratio * sigma * signals[i]) - (momentum * (this.Weights[i] - previousWeights[i]));
            }
        }

        public void LearnWidrowHoff(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError)
        {
            previousResponse = SimpleResponse.Response(signals, this.Weights);
            previousError = expectedOutput - previousResponse;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                this.Weights[i] += ratio * previousError * signals[i];
            }
        }

        #endregion
    }
}