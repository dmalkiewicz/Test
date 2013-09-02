using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NeuralNetwork.Helpers;
using NeuralNetworks.Neurons.SmartNeuron;

namespace NeuralNetworks.Layers.NeuralLayer
{
    [DataContract]
    [Serializable]
    public partial class NeuralLayer<T> : INeuralLayer<T>
        where T : ISmartNeuron
    {
        protected NeuralLayer(SerializationInfo info, StreamingContext context)
        {
            this.LayerGuid = (Guid)info.GetValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.LayerGuid), typeof(Guid));
            this.Layer = (IList<T>)info.GetValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.Layer), typeof(IList<T>));
            this.HasBias = (bool)info.GetValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.HasBias), typeof(bool));
            this.InputCount = (uint)info.GetValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.InputCount), typeof(uint));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.LayerGuid), this.LayerGuid);
            info.AddValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.Layer), this.Layer);
            info.AddValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.HasBias), this.HasBias);
            info.AddValue(PropertyHelper.GetName<NeuralLayer<T>>(p => p.InputCount), this.InputCount);
        }
    }
}
