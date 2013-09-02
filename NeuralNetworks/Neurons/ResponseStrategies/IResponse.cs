using System.Runtime.Serialization;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    public interface IResponse : ISerializable
    {
        void SetNeuron(INeuron newNeuron);

        double Response(double[] inputSignals);
    }
}