using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerClearTests
    {
        [Test]
        public void ShouldClearNeuralLayerWithNeuronsAndLayerShouldBeChanged()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsFalse(layer.IsChanged);
            layer.Clear();
            Assert.IsTrue(layer.IsChanged);
            Assert.AreEqual(0, layer.Count);
        }

        [Test]
        public void ShouldClearNeuralLayerWithoutNeuronsAndLayerShouldNotBeChanged()
        {
            var inputCount = 2u;
            var neuronsCount = 0u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsFalse(layer.IsChanged);
            layer.Clear();
            Assert.IsFalse(layer.IsChanged);
            Assert.AreEqual(0, layer.Count);
        }
    }
}
