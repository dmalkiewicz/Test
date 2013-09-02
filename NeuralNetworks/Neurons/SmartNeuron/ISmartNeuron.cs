using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NeuralNetworks.Neurons.Enums;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Neurons.SmartNeuron
{
    [XmlInclude(typeof(SmartNeuron))]
    public interface ISmartNeuron : INeuron, ISerializable
    {
        /// <summary>Gets or sets the neuron input signals.</summary>
        [DataMember]
        [XmlArrayItem(typeof(Signal))]
        IList<IReadonlySignal> InputSignals { get; set; }

        [DataMember]
        string Description { get; set; }

        IReadonlySignal CalculateOutputSignal();

        /// <summary>Calculates the memory trace strength.</summary>
        /// <param name="norm">Norm which will be used to calculate the strength (Manhattan or Euclidean).</param>
        /// <returns>Calculated memory trace strength.</returns>
        /// <remarks>
        /// Power memory trace determines how neuron is determined.
        /// The greater the power of memory trace, the more drastic will be neuron opinions.
        /// </remarks>
        double MemoryTraceStrength(StrengthNorm norm);

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

        void Learn(double[] signals, double[] previousWeights, double sigma, double ratio, double momentum);

        void LearnWidrowHoff(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError);

        void AppendBias(bool hasBias);
    }
}