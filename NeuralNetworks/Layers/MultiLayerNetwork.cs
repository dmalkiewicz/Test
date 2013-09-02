using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NeuralNetwork.Helpers;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Layers
{
    [DataContract]
    public class MultiLayerNetwork : IMultilayerNetwork
    {
        #region Fields

        #region Error messages

        private const string NoNeuronsInLayerMessage = "No neurons in layer {0}";

        #endregion

        #endregion

        #region Contructors

        public MultiLayerNetwork(uint inputCount, bool hasBias, IList<IList<ISmartNeuron>> layers)
            : this(inputCount, hasBias)
        {
            VerifyLayers(layers);
            this.Layers = layers;
        }

        public MultiLayerNetwork(uint inputCount, bool hasBias, IList<int> neuronCounts)
            : this(inputCount, hasBias)
        {
            this.Layers = new List<IList<ISmartNeuron>>(neuronCounts.Count);
            for (var neuralLayer = 0; neuralLayer < neuronCounts.Count; neuralLayer++)
            {
                if (neuronCounts[neuralLayer] <= 0)
                {
                    throw new ArgumentException(string.Format(NoNeuronsInLayerMessage, neuralLayer));
                }

                this.Layers.Add(new ISmartNeuron[neuronCounts[neuralLayer]]);
            }
        }

        public MultiLayerNetwork(uint inputCount, bool hasBias, IList<int> neuronCounts, Type neuronType)
            : this(inputCount, hasBias, neuronCounts)
        {
            var biasInputs = hasBias ? (uint)1 : 0;
            var neuronInputs = inputCount + biasInputs;
            foreach (var layer in this.Layers)
            {
                for (var neuronNumber = 0; neuronNumber < layer.Count; neuronNumber++)
                {
                    layer[neuronNumber] = Activator.CreateInstance(neuronType, neuronInputs) as ISmartNeuron;
                }

                neuronInputs = (uint)layer.Count + biasInputs;
            }
        }

        private MultiLayerNetwork(uint inputCount, bool hasBias)
        {
            this.InputCount = inputCount;
            this.HasBias = hasBias;
        }

        #endregion

        #region Properties

        [DataMember]
        public Guid MultiLayerNetworkGuid
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [DataMember]
        public bool HasBias
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [DataMember]
        public uint InputCount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public uint OutputCount
        {
            get { throw new NotImplementedException(); }
        }

        [DataMember]
        public IList<IList<ISmartNeuron>> Layers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Methods (public)

        public double[] Response(IEnumerable<ISignal> inputSignals)
        {
            throw new NotImplementedException();
        }

        public void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError, Enums.LearningMethod method)
        {
            throw new NotImplementedException();
        }

        public void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError)
        {
            throw new NotImplementedException();
        }

        public void LearnSimpleWidrowHoff(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods (private)

        private static void VerifyLayers(IList<IList<ISmartNeuron>> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(PropertyHelper.GetName<MultiLayerNetwork>(p => p.Layers));
            }

            const string NoLayersMessage = "No layers";
            if (layers.Count <= 0)
            {
                throw new ArgumentException(NoLayersMessage, PropertyHelper.GetName<MultiLayerNetwork>(p => p.Layers));
            }

            for (var i = 0; i < layers.Count; i++)
            {
                if (layers[i].Count <= 0)
                {
                    throw new ArgumentException(string.Format(NoNeuronsInLayerMessage, i));
                }
            }
        }

        #endregion
    }
}
