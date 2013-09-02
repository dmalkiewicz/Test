using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerContainsTests
    {
        [Test]
        public void ShouldReturnTrueForNeuronThatExistsInLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = layer[0];
            var result = layer.Contains(smartNeuron);
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseForNeuronThatNotExistsInLayer()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = new SmartNeuron(inputCount);
            var result = layer.Contains(smartNeuron);
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnFalseForNeuronAsNull()
        {
            var inputCount = 2u;
            var neuronsCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = layer[0];
            var result = layer.Contains(null);
            Assert.IsFalse(result);
        }
    }
}
