using System;
using System.Linq;

using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests
{
    [TestFixture]
    public class SmartNeuralNetworkConstructorsTests
    {
        private const uint LayersCount = 3u;

        private const uint NeuronsCount = 2u;

        private const uint InputSignalCount = 4u;

        [Test]
        public void ShouldCreateInstanceOfNeuralNetwork()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            Assert.IsNotNull(neuralNetwork);
        }

        [Test]
        public void ShouldCreateInstanceOfNeuralNetworkUsingInputSignals()
        {
            var signals = new[]
                            {
                                new Signal(),
                                new Signal(),
                                new Signal()
                            };

            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, signals);
            Assert.IsNotNull(neuralNetwork);
            Assert.IsFalse(neuralNetwork.IsChanged);
            Assert.AreEqual(NeuronsCount, neuralNetwork.OutputCount);
            Assert.AreEqual(LayersCount, neuralNetwork.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionWhenInputSignalsIsNull()
        {
            new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentNullExceptionWhenInputSignalsIsEmpty()
        {
            var signals = new ISignal[0];
            new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, signals);
        }

        [Test]
        public void ShouldCreateInstanceOfNeuralNetworkWithAppropriateNeuronsCountAndLayersCount()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            Assert.IsNotNull(neuralNetwork);
            Assert.AreEqual(LayersCount, neuralNetwork.Count);
            Assert.AreEqual(LayersCount, neuralNetwork.Count);
            Assert.AreEqual(InputSignalCount, neuralNetwork.InputCount);
            Assert.AreEqual(NeuronsCount, neuralNetwork.OutputCount);
        }

        [Test]
        public void ShouldCreateInstanceOfNeuralNetworkAndNeuralNetworkShouldNotBeChanged()
        {
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount);
            Assert.IsNotNull(neuralNetwork);
            Assert.IsFalse(neuralNetwork.IsChanged);
        }

        [TestCase(typeof(SimpleResponse))]
        [TestCase(typeof(SigmoidalResponse))]
        [TestCase(typeof(TanhResponse))]
        [TestCase(typeof(UnipolarResponse))]
        public void ShouldCreateInstanceOfNeuralNetworkWithAppropriateNeuronsResponseType(Type responseType)
        {
            var response = Activator.CreateInstance(responseType) as IResponse;
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronsCount, LayersCount, InputSignalCount, response);
            foreach (var neuron in neuralNetwork.SelectMany(layer => layer))
            {
                Assert.IsInstanceOf(responseType, neuron.ResponseStrategy);
            }

            Assert.AreEqual(NeuronsCount, neuralNetwork.OutputCount);
            Assert.AreEqual(LayersCount, neuralNetwork.Count);
        }
    }
}
