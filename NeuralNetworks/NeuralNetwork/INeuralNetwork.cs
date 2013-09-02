using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.NeuralNetwork
{
    [XmlInclude(typeof(SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>))]
    public interface INeuralNetwork<TLayer> : IList<TLayer>, ISerializable
        where TLayer : INeuralLayer<ISmartNeuron>
    {
        /// <summary>Gets or sets a neural network Guid.</summary>
        [DataMember]
        Guid NeuralNetworkGuid { get; set; }

        [DataMember]
        bool HasBias { get; set; }
        
        [DataMember]
        uint InputCount { get; }

        [XmlIgnore]
        uint OutputCount { get; }

        [DataMember]
        IList<TLayer> Layers { get; }

        [XmlIgnore]
        bool IsChanged { get; set; }

        IEnumerable<double> Response(IEnumerable<ISignal> inputSignals);

        IEnumerable<double> Response(double[] inputSignals);

        //int IndexOf(T item);

        //void Insert(int index, T item);

        //void Add(T item);

        //bool Contains(T item);

        //bool Remove(T item);

        //void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError, LearningMethod method);

        //void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError);

        //void LearnSimpleWidrowHoff(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError);
    }
}
