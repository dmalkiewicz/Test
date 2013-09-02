using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NeuralNetworks.UnitTests.Layers.Concretes;
using NeuralNetworks.UnitTests.Neurons.Abstracts;
using NeuralNetworks.UnitTests.Neurons.Concretes;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.NeuralLayerTests
{
    [TestFixture]
    public class NeuralLayerResponseTests
    {
        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseByNeuralLayer(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            params double[] signalsArray)
        {
            List<Signal> signals;
            NeuralLayer<ISmartNeuron> layer;
            PrepareLayerToTest(
                smartNeuron1Weight1,
                smartNeuron1Weight2,
                smartNeuron2Weight1,
                smartNeuron2Weight2,
                smartNeuron3Weight1,
                smartNeuron3Weight2,
                signalsArray,
                out signals,
                out layer);

            var layers = PrepareMultiLayerPerceptronNetwork(
                smartNeuron1Weight1,
                smartNeuron1Weight2,
                smartNeuron2Weight1,
                smartNeuron2Weight2,
                smartNeuron3Weight1,
                smartNeuron3Weight2,
                signalsArray,
                signals,
                layer);

            var result1 = layer.Response(signals);
            var result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);
        }

        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseByNeuralLayerUsingDoubleArray(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            params double[] signalsArray)
        {
            List<Signal> signals;
            NeuralLayer<ISmartNeuron> layer;
            PrepareLayerToTest(
                smartNeuron1Weight1,
                smartNeuron1Weight2,
                smartNeuron2Weight1,
                smartNeuron2Weight2,
                smartNeuron3Weight1,
                smartNeuron3Weight2,
                signalsArray,
                out signals,
                out layer);

            var result1 = layer.Response(signals);

            var result2 = layer.Response(signals.Select(s => s.Value).ToArray());
            CollectionAssert.AreEqual(result1, result2);
        }

        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseWithAppendingBiasByNeuralLayer(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            params double[] signalsArray)
        {
            // Initialize
            const uint NeuronsCount = 0u;
            var signals = signalsArray.Select(s => new Signal { Value = s }).ToList();
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, signals);

            // Prepare neurons
            var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron1Weight1, smartNeuron1Weight2 }, signals);
            var smartNeuron2 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron2Weight1, smartNeuron2Weight2 }, signals);
            var smartNeuron3 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron3Weight1, smartNeuron3Weight2 }, signals);
            layer.Add(smartNeuron1);
            layer.Add(smartNeuron2);
            layer.Add(smartNeuron3);

            var layers = PrepareMultiLayerPerceptronNetwork(
                smartNeuron1Weight1,
                smartNeuron1Weight2,
                smartNeuron2Weight1,
                smartNeuron2Weight2,
                smartNeuron3Weight1,
                smartNeuron3Weight2,
                signalsArray,
                signals,
                layer);
            layer.HasBias = true;
            layers.HasBias = true;

            var result1 = layer.Response(signals);
            var result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);

            layer.HasBias = false;
            layers.HasBias = false;

            result1 = layer.Response(signals);
            result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForNullInputSignalsInResponseUsingListOfISignal()
        {
            // Initialize
            const uint NeuronsCount = 0u;
            var signals = new List<ISignal>(
                new[]
                {
                    new Signal()
                });
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, signals);
            signals = null;
            layer.Response(signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForDifferentInputCoutsInResponseListOfISignal()
        {
            // Initialize
            const uint NeuronsCount = 0u;
            IEnumerable<ISignal> signals = new List<ISignal>();
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, signals);
            signals = new List<ISignal>();
            layer.Response(signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForNullInputSignalsInResponseUsingDoubleArray()
        {
            // Initialize
            const uint NeuronsCount = 0u;
            var signals = new[] { 0.0 };
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, (uint)signals.Length);
            signals = null;
            layer.Response(signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForDifferentInputCoutsInResponseUsingDoubleArray()
        {
            // Initialize
            const uint NeuronsCount = 0u;
            var signals = new[] { 0.0 };
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, (uint)signals.Length);
            signals = new double[0];
            layer.Response(signals);
        }

        private static void AssignWeights(double weight0, double weight1, Neurons.Interfaces.INeuron oldNeuron)
        {
            oldNeuron.Weights[0] = weight0;
            oldNeuron.Weights[1] = weight1;
        }

        private static void PrepareLayerToTest(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            IEnumerable<double> signalsArray,
            out List<Signal> signals,
            out NeuralLayer<ISmartNeuron> layer)
        {
            // Initialize
            const uint NeuronsCount = 0u;
            signals = signalsArray.Select(s => new Signal { Value = s }).ToList();
            layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, signals);

            // Prepare neurons
            var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron1Weight1, smartNeuron1Weight2 }, signals);
            var smartNeuron2 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron2Weight1, smartNeuron2Weight2 }, signals);
            var smartNeuron3 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron3Weight1, smartNeuron3Weight2 }, signals);
            layer.Add(smartNeuron1);
            layer.Add(smartNeuron2);
            layer.Add(smartNeuron3);
        }

        private static MultiLayerPerceptronNetwork PrepareMultiLayerPerceptronNetwork(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            ICollection<double> signalsArray,
            IEnumerable<Signal> signals,
            INeuralLayer<ISmartNeuron> layer)
        {
            var layers = new MultiLayerPerceptronNetwork(signals.Count(), false, new[] { layer.Count });
            layers.Layers[0] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Count),
                                    new SimpleMigrationNeuron(signalsArray.Count),
                                    new SimpleMigrationNeuron(signalsArray.Count)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[0][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[0][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[0][2]);
            return layers;
        }
    }
}
