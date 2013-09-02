using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerAddTests
    {
        [Test]
        public void ShouldAddAppropriateNeuronToNeuralLayer()
        {
            var inputCount = 2u;
            var smartNeuron = new SmartNeuron(inputCount, new SimpleResponse());

            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsFalse(layer.IsChanged);
            layer.Add(smartNeuron);
            Assert.IsTrue(layer.IsChanged);
            Assert.AreEqual(neuronsCount + 1, layer.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotAddNeuronAsNullToNeuralLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsFalse(layer.IsChanged);
            layer.Add(null);
            Assert.IsFalse(layer.IsChanged);
            Assert.AreNotEqual(neuronsCount + 1, layer.Count);
        }
    }
}
