using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.Helpers;
using NeuralNetworks.Neurons.Enums;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Neurons.SmartNeuron
{
    public partial class SmartNeuron : ISmartNeuron
    {
        #region Fields

        private const int Min = -10;

        private const double Length = 0.00001; // or use min - max

        private ISignal bias;

        #endregion

        #region Methods (public)

        public override bool Equals(object obj)
        {
            var smartNeuron = obj as ISmartNeuron;
            if (smartNeuron == null)
            {
                return false;
            }

            if (smartNeuron.Identifier != this.Identifier)
            {
                return false;
            }

            if (this.InputSignals != null)
            {
                if (smartNeuron.InputSignals == null)
                {
                    return false;
                }

                if (smartNeuron.InputSignals.Count != this.InputSignals.Count)
                {
                    return false;
                }

                if (this.InputSignals.Where((t, i) => !t.Equals(smartNeuron.InputSignals[i])).Any())
                {
                    return false;
                }
            }
            else if (smartNeuron.InputSignals != null)
            {
                return false;
            }

            if (this.Weights != null)
            {
                if (smartNeuron.Weights == null)
                {
                    return false;
                }

                if (smartNeuron.Weights.Length != this.Weights.Length)
                {
                    return false;
                }
            }

            return !this.weights.Where((t, i) => Math.Abs(smartNeuron.Weights[i] - t) > double.Epsilon).Any();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void SetWeights(double[] newWeights)
        {
            if (newWeights == null)
            {
                this.weights = null;
                this.InputSignalsWithWeights = null;
                return;
            }

            var newInputSignalsWithWeights = new Dictionary<IReadonlySignal, double>(this.InputSignalsWithWeights.Count);
            var counter = 0;
            foreach (var item in this.InputSignalsWithWeights)
            {
                newInputSignalsWithWeights.Add(item.Key, newWeights[counter++]);
                if (counter >= newWeights.Length)
                {
                    break;
                }
            }

            this.InputSignalsWithWeights = newInputSignalsWithWeights;
        }

        /// <summary>Calculates the neuron response for given signal.</summary>
        /// <param name="inputSignals">The signals for neuron inputs.
        /// Input array and weight array lengths must match.</param>
        /// <returns>The response of neuron for specified signals.</returns>
        public double Response(double[] inputSignals)
        {
            return this.ResponseStrategy.Response(inputSignals);
        }

        public IReadonlySignal CalculateOutputSignal()
        {
            this.Value = this.Response(this.InputSignals.Select(s => s.Value).ToArray());
            return this;
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
            if (signals == null || signals.Length != this.InputSignals.Count)
            {
                throw new ArgumentException();
            }

            previousResponse = this.Response(signals);
            previousError = expectedOutput - previousResponse;
            var newSignals = new Dictionary<IReadonlySignal, double>(this.InputSignalsWithWeights.Count);
            var count = 0;
            foreach (var signalKeyValuePair in this.InputSignalsWithWeights)
            {
                newSignals.Add(
                    new Signal { Identifier = signalKeyValuePair.Key.Identifier, Value = signalKeyValuePair.Key.Value },
                    signalKeyValuePair.Value + (ratio * previousError * signals[count++]));
            }

            this.PreviousWeights = this.Weights;
            this.InputSignalsWithWeights = newSignals;
        }

        public void Learn(double[] signals, double[] previousWeights, double sigma, double ratio, double momentum)
        {
            if (signals == null || signals.Length != this.InputSignals.Count)
            {
                throw new ArgumentException();
            }

            if (previousWeights == null || previousWeights.Length != this.InputSignals.Count)
            {
                throw new ArgumentException();
            }

            var newWeights = this.Weights;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                newWeights[i] += (ratio * sigma * signals[i]) - (momentum * (newWeights[i] - previousWeights[i]));
            }

            this.PreviousWeights = previousWeights;
            this.SetWeights(newWeights);
        }

        public void LearnWidrowHoff(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError)
        {
            previousResponse = SimpleResponse.Response(signals, this.Weights);
            previousError = expectedOutput - previousResponse;
            var newWeights = this.Weights;
            for (var i = 0; i < this.Weights.Length; i++)
            {
                newWeights[i] += ratio * previousError * signals[i];
            }

            this.PreviousWeights = this.Weights;
            this.SetWeights(newWeights);
        }

        public void AppendBias(bool hasBias)
        {
            if (this.bias != null && hasBias)
            {
                return;
            }

            if (this.bias == null)
            {
                this.bias = new Signal();
                this.InputSignalsWithWeights.Add(this.bias, Min + (Length * new Random().NextDouble()));
                this.weights = this.InputSignalsWithWeights.Select(s => s.Value).ToArray();
            }
            else
            {
                this.InputSignalsWithWeights.Remove(this.bias);
                this.bias = null;
                this.weights = this.InputSignalsWithWeights.Select(s => s.Value).ToArray();
            }
        }

        #endregion

        #region Methods (private)

        private static void ValidateNeededObjects(IEnumerable<IReadonlySignal> inputSignals)
        {
            if (inputSignals == null)
            {
                throw new ArgumentNullException(PropertyHelper.GetName<SmartNeuron>(p => p.InputSignals));
            }
        }

        private static Dictionary<IReadonlySignal, double> CreateInputSignalsWithWeights(IList<IReadonlySignal> inputSignals, double[] weights)
        {
            var result = new Dictionary<IReadonlySignal, double>(inputSignals.Count);
            for (var i = 0; i < weights.Length; i++)
            {
                result.Add(inputSignals[i], weights[i]);
            }

            return result;
        }

        private double[] CreateRandomWeights(uint inputCount)
        {
            var random = new Random();
            var randomWeights = new double[inputCount];
            for (var i = 0; i < randomWeights.Length; i++)
            {
                var randommmm = random.NextDouble();
                var newWeight = Min + (Length * randommmm);
                while (Math.Abs(newWeight) < double.Epsilon)
                {
                    newWeight = Min + (Length * random.NextDouble());
                }

                randomWeights[i] = newWeight;
            }

            return randomWeights;
        }

        #endregion
    }
}
