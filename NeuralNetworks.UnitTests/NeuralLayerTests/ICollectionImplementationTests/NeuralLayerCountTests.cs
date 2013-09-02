using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerCountTests
    {
        [TestCase(1u, ExpectedResult = 1)]
        [TestCase(10u, ExpectedResult = 10)]
        [TestCase(100u, ExpectedResult = 100)]
        [TestCase(uint.MinValue, ExpectedResult = 0)]
        [TestCase(uint.MaxValue, ExpectedException = typeof(OverflowException))]
        public int NeuralLayerCountTest(uint neuronsCount)
        {
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, 1);
            Assert.IsNotNull(layer);
            return layer.Count;
        }
    }
}
