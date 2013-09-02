using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NeuralNetwork.Helpers;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;

namespace NeuralNetworks.NeuralNetwork
{
    [DataContract]
    [Serializable]
    public partial class SmartNeuralNetwork<TLayer> : INeuralNetwork<TLayer>
        where TLayer : INeuralLayer<ISmartNeuron>
    {
        protected SmartNeuralNetwork(SerializationInfo info, StreamingContext context)
        {
            this.NeuralNetworkGuid = (Guid)info.GetValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.NeuralNetworkGuid), typeof(Guid));
            this.Layers = (IList<TLayer>)info.GetValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.Layers), typeof(IList<TLayer>));
            this.HasBias = (bool)info.GetValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.HasBias), typeof(bool));
            this.InputCount = (uint)info.GetValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.InputCount), typeof(uint));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.NeuralNetworkGuid), this.NeuralNetworkGuid);
            info.AddValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.Layers), this.Layers);
            info.AddValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.HasBias), this.HasBias);
            info.AddValue(PropertyHelper.GetName<SmartNeuralNetwork<TLayer>>(p => p.InputCount), this.InputCount);
        }
    }
}
