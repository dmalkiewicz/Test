using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests
{
    public class SmartNeuralNetworkTests
    {
        public static INeuralLayer<ISmartNeuron> CreateNeuralLayer()
        {
            const uint InputCount = 2u;
            const uint NeuronsCount = 2u;
            return new NeuralLayer<ISmartNeuron>(NeuronsCount, InputCount);
        }
    }
}
