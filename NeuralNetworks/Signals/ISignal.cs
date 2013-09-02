using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace NeuralNetworks.Signals
{
    [XmlInclude(typeof(Signal))]
    public interface ISignal : ISerializable, IReadonlySignal
    {
        [DataMember]
        new double Value { get; set; }
    }
}