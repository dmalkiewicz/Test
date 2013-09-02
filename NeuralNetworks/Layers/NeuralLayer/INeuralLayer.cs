using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Layers.NeuralLayer
{
    [XmlInclude(typeof(NeuralLayer<SmartNeuron>))]
    public interface INeuralLayer<T> : IList<T>, ISerializable
        where T : ISmartNeuron
    {
        [DataMember]
        Guid LayerGuid { get; set; }

        [DataMember]
        [XmlArrayItem(typeof(SmartNeuron))]
        IList<T> Layer { get; set; }

        [DataMember]
        bool HasBias { get; set; }

        [DataMember]
        uint InputCount { get; }

        [XmlIgnore]
        uint OutputCount { get; }

        [XmlIgnore]
        bool IsChanged { get; set; }

        IEnumerable<double> Response(IEnumerable<ISignal> inputSignals);

        IEnumerable<double> Response(double[] inputSignals);

        T GetNeuron(Guid neuronGuid);
    }
}
