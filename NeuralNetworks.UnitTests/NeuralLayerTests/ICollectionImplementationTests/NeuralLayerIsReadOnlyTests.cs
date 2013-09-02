using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests.ICollectionImplementationTests
{
    [TestFixture]
    public class NeuralLayerIsReadOnlyTests
    {
        [Test]
        public void ShouldReturnFalseWhenCheckingTheIsReadOnlyForNeuralLayer()
        {
            var layer = new NeuralLayer<ISmartNeuron>(0, 0);
            Assert.IsFalse(layer.IsReadOnly);
        }
    }
}
