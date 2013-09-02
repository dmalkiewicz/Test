using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests
{
    [TestFixture]
    public class NeuralLayerGetNeuronTests
    {
        [Test]
        public void ShouldGetNeuronBasedOnNeuronGuid()
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            var smartNeuron1 = new SmartNeuron(inputCount, new SimpleResponse());
            layer.Add(smartNeuron1);

            var smartNeuron2 = layer.GetNeuron(smartNeuron1.Identifier);
            Assert.AreEqual(smartNeuron1, smartNeuron2);
        }

        [Test]
        public void ShouldGetNullForNotExistingNeuronGuid()
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);

            var smartNeuron1 = layer.GetNeuron(Guid.NewGuid());
            Assert.IsNull(smartNeuron1);
        }
    }
}
