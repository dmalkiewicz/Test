using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.IListImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkIndexOfTests : SmartNeuralNetworkTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(3, ExpectedResult = -1)]
        public int ShouldGetApropriateIndexOf(int index)
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var smartNeuron = neuralNetwork[index];
            return neuralNetwork.IndexOf(smartNeuron);
        }

        [Test]
        public void ShouldGetMinus1ForNeuronAsNull()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var result = neuralNetwork.IndexOf(null);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void ShouldGetMinus1ForNotExistingNeuronInLayer()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            var layer = CreateNeuralLayer();
            var result = neuralNetwork.IndexOf(layer);
            Assert.AreEqual(-1, result);
        }
    }
}
