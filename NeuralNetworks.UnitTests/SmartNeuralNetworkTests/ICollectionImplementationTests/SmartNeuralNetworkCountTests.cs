using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests.ICollectionImplementationTests
{
    [TestFixture]
    public class SmartNeuralNetworkCountTests
    {
        [TestCase(1u, 2u, ExpectedResult = 2)]
        [TestCase(10u, 2u, ExpectedResult = 2)]
        [TestCase(100u, 2u, ExpectedResult = 2)]
        [TestCase(uint.MinValue, 2u, ExpectedResult = 2)]
        [TestCase(uint.MaxValue, 2u, ExpectedException = typeof(OverflowException))]

        [TestCase(2u, 1u, ExpectedResult = 1)]
        [TestCase(2u, 10u, ExpectedResult = 10)]
        [TestCase(2u, 100u, ExpectedResult = 100)]
        [TestCase(2u, uint.MinValue, ExpectedResult = 0)]
        [TestCase(2u, uint.MaxValue, ExpectedException = typeof(OverflowException))]
        public int SmartNeuralNetworkCountTest(uint neuronsCount, uint layersCount)
        {
            const uint InputCount = 2u;
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(neuronsCount, layersCount, InputCount);
            Assert.IsNotNull(neuralNetwork);
            return neuralNetwork.Count;
        }
    }
}
