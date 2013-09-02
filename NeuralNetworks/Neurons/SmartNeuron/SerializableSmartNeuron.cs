using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NeuralNetwork.Helpers;
using NeuralNetworks.Neurons.ResponseStrategies;

namespace NeuralNetworks.Neurons.SmartNeuron
{
    [DataContract]
    [Serializable]
    public partial class SmartNeuron : ISmartNeuron
    {
        protected SmartNeuron(SerializationInfo info, StreamingContext context)
        {
            this.Identifier = (Guid)info.GetValue(PropertyHelper.GetName<SmartNeuron>(p => p.Identifier), typeof(Guid));
            this.InputSignals = (IList<IReadonlySignal>)info.GetValue(PropertyHelper.GetName<SmartNeuron>(p => p.InputSignals), typeof(IList<IReadonlySignal>));
            this.weights = (double[])info.GetValue(PropertyHelper.GetName<SmartNeuron>(p => p.Weights), typeof(double[]));
            this.PreviousWeights = (double[])info.GetValue(PropertyHelper.GetName<SmartNeuron>(p => p.PreviousWeights), typeof(double[]));
            this.ResponseStrategy = (IResponse)info.GetValue(PropertyHelper.GetName<SmartNeuron>(p => p.ResponseStrategy), typeof(IResponse));
            this.ResponseStrategy.SetNeuron(this);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(PropertyHelper.GetName<SmartNeuron>(p => p.Identifier), this.Identifier);
            info.AddValue(PropertyHelper.GetName<SmartNeuron>(p => p.InputSignals), this.InputSignals);
            info.AddValue(PropertyHelper.GetName<SmartNeuron>(p => p.Weights), this.Weights);
            info.AddValue(PropertyHelper.GetName<SmartNeuron>(p => p.PreviousWeights), this.PreviousWeights);
            info.AddValue(PropertyHelper.GetName<SmartNeuron>(p => p.ResponseStrategy), this.ResponseStrategy);
        }
    }
}
