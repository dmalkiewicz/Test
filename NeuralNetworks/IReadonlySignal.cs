using System;
using System.Runtime.Serialization;

namespace NeuralNetworks
{
    public interface IReadonlySignal
    {
        [DataMember]
        Guid Identifier { get; set; }

        [DataMember]
        double Value { get; }
    }
}
