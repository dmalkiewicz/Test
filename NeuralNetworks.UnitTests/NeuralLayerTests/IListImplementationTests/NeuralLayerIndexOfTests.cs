using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.IListImplementationTests
{
    [TestFixture]
    public class NeuralLayerIndexOfTests
    {
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = -1)]
        public int ShouldGetApropriateIndexOf(int index)
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = layer[index];
            return layer.IndexOf(smartNeuron);
        }

        [Test]
        public void ShouldGetMinus1lForNeuronAsNull()
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var result = layer.IndexOf(null);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void ShouldGetMinus1ForNotExistingNeuronInLayer()
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron = new SmartNeuron(neuronsCount, new SimpleResponse());
            var result = layer.IndexOf(smartNeuron);
            Assert.AreEqual(-1, result);
        }
    }
}
