using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NeuralNetworks.Layers.Enums;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Layers
{
    public interface IMultilayerNetwork
    {
        /// <summary>Gets or sets a multi layer network Guid.</summary>
        [DataMember]
        Guid MultiLayerNetworkGuid { get; set; }

        [DataMember]
        bool HasBias { get; set; }

        [DataMember]
        uint InputCount { get; set; }

        uint OutputCount { get; }

        [DataMember]
        IList<IList<ISmartNeuron>> Layers { get; set; }

        double[] Response(IEnumerable<ISignal> inputSignals);

        void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError, LearningMethod method);

        void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError);

        void LearnSimpleWidrowHoff(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError);
    }
}
