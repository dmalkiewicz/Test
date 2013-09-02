using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkIsReadOnlyTests
    {
        [Test]
        public void ShouldReturnFalseWhenCheckingTheIsReadOnlyForNeuralLayer()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(2u, 3u, 2u);
            Assert.IsFalse(neuralNetwork.IsReadOnly);
        }
    }
}
