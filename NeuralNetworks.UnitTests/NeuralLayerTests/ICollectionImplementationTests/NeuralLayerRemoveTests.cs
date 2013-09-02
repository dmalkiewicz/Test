using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerRemoveTests
    {
        [Test]
        public void ShouldRemoveExistingNeuronFromNeuralLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = layer[0];

            Assert.IsFalse(layer.IsChanged);
            var result = layer.Remove(smartNeuron);
            Assert.IsTrue(layer.IsChanged);
            Assert.IsTrue(result);
            Assert.AreEqual(neuronsCount - 1, layer.Count);
            Assert.IsNull(layer.GetNeuron(smartNeuron.Identifier));
        }

        [Test]
        public void ShouldDoNothingForNotExistingNeuronWhileRemovingFromNeuralLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = new SmartNeuron(inputCount);

            Assert.IsFalse(layer.IsChanged);
            var result = layer.Remove(smartNeuron);
            Assert.IsFalse(layer.IsChanged);
            Assert.IsFalse(result);
            Assert.AreEqual(neuronsCount, layer.Count);
            Assert.IsNull(layer.GetNeuron(smartNeuron.Identifier));
        }

        [Test]
        public void ShouldDoNothingForNeuronAsNullWhileRemovingFromNeuralLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);

            Assert.IsFalse(layer.IsChanged);
            var result = layer.Remove(null);
            Assert.IsFalse(layer.IsChanged);
            Assert.IsFalse(result);
            Assert.AreEqual(neuronsCount, layer.Count);
            Assert.IsFalse(layer.Contains(null));
        }
    }
}
