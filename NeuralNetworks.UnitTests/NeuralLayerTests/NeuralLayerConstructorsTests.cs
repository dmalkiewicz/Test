using System;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests
{
    [TestFixture]
    public class NeuralLayerConstructorsTests
    {
        [Test]
        public void ShouldCreateInstanceOfNeuralLayer()
        {
            var layer = new NeuralLayer<ISmartNeuron>(2, 2);
            Assert.IsNotNull(layer);
        }

        [Test]
        public void ShouldCreateInstanceOfNeuralLayerUsingInputSignals()
        {
            var neuronsCount = 2u;
            var signals = new[]
                            {
                                new Signal(),
                                new Signal(),
                                new Signal()
                            };

            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, signals);
            Assert.IsNotNull(layer);
            Assert.IsFalse(layer.IsChanged);
            Assert.AreEqual(neuronsCount, layer.OutputCount);
            Assert.AreEqual(neuronsCount, layer.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionWhenInputSignalsIsNull()
        {
            ISignal[] signals = null;
            var layer = new NeuralLayer<ISmartNeuron>(2, signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentNullExceptionWhenInputSignalsIsEmpty()
        {
            var signals = new ISignal[0];

            var layer = new NeuralLayer<ISmartNeuron>(2, signals);
        }

        [Test]
        public void ShouldCreateInstanceOfNeuralLayerWithAppropriateNeuronsCount()
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount);
            Assert.IsNotNull(layer);
            Assert.AreEqual(neuronsCount, layer.Count);
            Assert.AreEqual(inputCount, layer.InputCount);
            Assert.AreEqual(neuronsCount, layer.OutputCount);
            Assert.AreEqual(neuronsCount, layer.Count);
        }

        [Test]
        public void ShouldCreateInstanceOfNeuralLayerAndNeuralLayerShouldNotBeChanged()
        {
            var layer = new NeuralLayer<ISmartNeuron>(2, 2);
            Assert.IsNotNull(layer);
            Assert.IsFalse(layer.IsChanged);
        }

        [TestCase(typeof(SimpleResponse))]
        [TestCase(typeof(SigmoidalResponse))]
        [TestCase(typeof(TanhResponse))]
        [TestCase(typeof(UnipolarResponse))]
        public void ShouldCreateInstanceOfNeuralLayerWithAppropriateNeuronsResponseType(Type responseType)
        {
            var neuronsCount = 2u;
            var inputCount = 2u;
            var response = Activator.CreateInstance(responseType) as IResponse;
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, inputCount, response);
            foreach (var neuron in layer)
            {
                Assert.IsInstanceOf(responseType, neuron.ResponseStrategy);
            }

            Assert.AreEqual(neuronsCount, layer.OutputCount);
            Assert.AreEqual(neuronsCount, layer.Count);
        }
    }
}
