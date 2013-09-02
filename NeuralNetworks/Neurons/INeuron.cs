using System.Runtime.Serialization;
using NeuralNetworks.Neurons.ResponseStrategies;

namespace NeuralNetworks.Neurons
{
    public interface INeuron : IReadonlySignal
    {
        /// <summary>Gets an array which stores weights for each of neuron's inputs.</summary>
        [DataMember]
        double[] Weights { get; }

        /// <summary>Gets an array which stores previous weights for each of neuron's inputs.</summary>
        [DataMember]
        double[] PreviousWeights { get; }

        /// <summary>Gets or sets the neuron response strategy.</summary>
        [DataMember]
        IResponse ResponseStrategy { get; set; }

        /// <summary>Calculates the neuron response for given signal.</summary>
        /// <param name="inputSignals">The signals for neuron inputs.
        /// Input array and weight array lengths must match.</param>
        /// <returns>The response of neuron for specified signals.</returns>
        double Response(double[] inputSignals);

        void SetWeights(double[] newWeights);
    }
}