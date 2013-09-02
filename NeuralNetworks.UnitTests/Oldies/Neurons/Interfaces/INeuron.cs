using System;

using NeuralNetworks.UnitTests.Neurons.Enums;

namespace NeuralNetworks.UnitTests.Neurons.Interfaces
{
    /// <summary>This is an basic interface for basic linear neuron.</summary>
    public interface INeuron
    {
        /// <summary>Gets a neuron Guid.</summary>
        Guid NeuronGuid { get; }

        /// <summary>Gets an array which stores weights for each of neuron's inputs.</summary>
        double[] Weights { get; }

        /// <summary>Gets an array which stores previous weights for each of neuron's inputs.</summary>
        double[] PrevWeights { get; }

        /// <summary>Teaches neuron how to response for given signal. It is for a "learning with a teacher".</summary>
        /// <param name="signals">The signal, for which neuron have to respond.</param>
        /// <param name="expectedOutput">Specifies what should be neuron appropriate response.</param>
        /// <param name="ratio">Learning factor.
        /// The bigger, the faster will be a learning process.
        /// But be careful, because too large will make neuron will be struggling.
        /// This factor should also be considered as the size of the penalty for giving wrong answers by neuron.</param>
        /// <param name="previousResponse">The method puts it on a given neuron response signal before learning.</param>
        /// <param name="previousError">The method puts the size of the error of the neuron before learning.</param>
        void Learn(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError);

        void Learn(double[] signals, double[] previousWeights, double error, double sigma, double ratio, double momentum);

        /// <summary>Calculates the memory trace strength.</summary>
        /// <param name="norm">Norm which will be used to calculate the strength (Manhattan or Euclidean).</param>
        /// <returns>Calculated memory trace strength.</returns>
        /// <remarks>
        /// Power memory trace determines how neuron is determined.
        /// The greater the power of memory trace, the more drastic will be neuron opinions.
        /// </remarks>
        double MemoryTraceStrength(StrengthNorm norm);

        /// <summary>Randomizes the neuron weights.</summary>
        /// <param name="randomGenerator">The random generator.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        void RandomizeWeights(Random randomGenerator, double min, double max);

        /// <summary>Randomizes the weights.</summary>
        /// <param name="randomGenerator">The random generator.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="epsilon">The epsilon.</param>
        void RandomizeWeights(Random randomGenerator, double min, double max, double epsilon);

        /// <summary>Calculates the neuron response for given signal.</summary>
        /// <param name="inputSignals">The signals for neuron inputs.
        /// Input array and weight array lengths must match.</param>
        /// <returns>The response of neuron for specified signals.</returns>
        double Response(double[] inputSignals);
    }
}
