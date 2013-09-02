using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NeuralNetworks.UnitTests.Layers.Concretes;
using NeuralNetworks.UnitTests.Neurons.Abstracts;
using NeuralNetworks.UnitTests.Neurons.Concretes;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuralNetworkTests
{
    [TestFixture]
    public class SmartNeuralNetworkResponseTests
    {
        #region Response tests with single layer

        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseByNeuralNetworkWithOneLayer(
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
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0u, 0u, signals) { layer };

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

            var result1 = neuralNetwork.Response(signals);
            var result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);
        }

        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseByNeuralNetworkWithOneLayerUsingDoubleArray(
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
            IEnumerable<Signal> signals = signalsArray.Select(s => new Signal { Value = s }).ToList();
            var layer = new NeuralLayer<ISmartNeuron>(NeuronsCount, signals);

            // Prepare neurons
            var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron1Weight1, smartNeuron1Weight2 }, signals);
            var smartNeuron2 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron2Weight1, smartNeuron2Weight2 }, signals);
            var smartNeuron3 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron3Weight1, smartNeuron3Weight2 }, signals);
            layer.Add(smartNeuron1);
            layer.Add(smartNeuron2);
            layer.Add(smartNeuron3);
            var inputSignalsCount = Convert.ToUInt32(signalsArray.Length);
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0u, 0u, inputSignalsCount) { layer };

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

            var result1 = neuralNetwork.Response(signalsArray);
            var result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);
        }

        #endregion

        #region Response tests with many layers

        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseByNeuralNetworkWith4Layers(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            params double[] signalsArray)
        {
            // Initialize
            const uint LayersCount = 4u;
            IEnumerable<Signal> signals = signalsArray.Select(s => new Signal { Value = s }).ToList();
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0u, 0u, signals);
            for (uint i = 0; i < LayersCount; i++)
            {
                // Prepare neurons
                var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron1Weight1, smartNeuron1Weight2 }, signals);
                var smartNeuron2 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron2Weight1, smartNeuron2Weight2 }, signals);
                var smartNeuron3 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron3Weight1, smartNeuron3Weight2 }, signals);

                var layer = new NeuralLayer<ISmartNeuron>(0u, signals)
                                                      {
                                                          smartNeuron1,
                                                          smartNeuron2,
                                                          smartNeuron3
                                                      };
                neuralNetwork.Add(layer);
            }

            const int NeuronsCount = 3;
            var layers = new MultiLayerPerceptronNetwork(signals.Count(), false, new[] { NeuronsCount, NeuronsCount, NeuronsCount, NeuronsCount });
            #region layers.Layers[0]
            layers.Layers[0] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[0][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[0][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[0][2]); 
            #endregion
            #region layers.Layers[1]
            layers.Layers[1] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[1][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[1][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[1][2]);
            #endregion
            #region layers.Layers[2]
            layers.Layers[2] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[2][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[2][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[2][2]);
            #endregion
            #region layers.Layers[3]
            layers.Layers[3] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[3][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[3][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[3][2]);
            #endregion

            var result1 = neuralNetwork.Response(signals);
            var result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);
        }

        [TestCase(
            1, 1.5, // Weights of Neuron 1
            2, 1.5, // Weights of Neuron 2
            1.5, 1, // Weights of Neuron 3
            5, 4)] // Signals
        public void ShouldGetAppropriateResponseByNeuralNetworkWith4LayersUsingDoubleArray(
            double smartNeuron1Weight1,
            double smartNeuron1Weight2,
            double smartNeuron2Weight1,
            double smartNeuron2Weight2,
            double smartNeuron3Weight1,
            double smartNeuron3Weight2,
            params double[] signalsArray)
        {
            // Initialize
            const uint LayersCount = 4u;
            var signals = signalsArray.Select(s => new Signal { Value = s }).ToList();
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(0u, 0u, signals);
            for (uint i = 0; i < LayersCount; i++)
            {
                // Prepare neurons
                var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron1Weight1, smartNeuron1Weight2 }, signals);
                var smartNeuron2 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron2Weight1, smartNeuron2Weight2 }, signals);
                var smartNeuron3 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron3Weight1, smartNeuron3Weight2 }, signals);

                var layer = new NeuralLayer<ISmartNeuron>(0u, signals)
                                                      {
                                                          smartNeuron1,
                                                          smartNeuron2,
                                                          smartNeuron3
                                                      };
                neuralNetwork.Add(layer);
            }

            const int NeuronsCount = 3;
            var layers = new MultiLayerPerceptronNetwork(signals.Count(), false, new[] { NeuronsCount, NeuronsCount, NeuronsCount, NeuronsCount });
            #region layers.Layers[0]
            layers.Layers[0] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[0][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[0][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[0][2]);
            #endregion
            #region layers.Layers[1]
            layers.Layers[1] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[1][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[1][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[1][2]);
            #endregion
            #region layers.Layers[2]
            layers.Layers[2] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[2][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[2][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[2][2]);
            #endregion
            #region layers.Layers[3]
            layers.Layers[3] = new NonlinearNeuron[]
                                {
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length),
                                    new SimpleMigrationNeuron(signalsArray.Length)
                                };
            AssignWeights(smartNeuron1Weight1, smartNeuron1Weight2, layers.Layers[3][0]);
            AssignWeights(smartNeuron2Weight1, smartNeuron2Weight2, layers.Layers[3][1]);
            AssignWeights(smartNeuron3Weight1, smartNeuron3Weight2, layers.Layers[3][2]);
            #endregion

            var result1 = neuralNetwork.Response(signalsArray);
            var result2 = layers.Response(signalsArray);
            CollectionAssert.AreEqual(result1, result2);
        }

        #endregion

/*
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
            IEnumerable<Signal> signals;
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
            var neuronsCount = 0u;
            var signals = signalsArray.Select(s => new Signal() { Value = s });
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, signals);

            // Prepare neurons
            var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron1Weight1, smartNeuron1Weight2 }, new double[0], signals);
            var smartNeuron2 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron2Weight1, smartNeuron2Weight2 }, new double[0], signals);
            var smartNeuron3 = new SmartNeuron(Guid.NewGuid(), new[] { smartNeuron3Weight1, smartNeuron3Weight2 }, new double[0], signals);
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
            var neuronsCount = 0u;
            IEnumerable<ISignal> signals = new List<ISignal>(
                new[]
                {
                    new Signal()
                });
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, signals);
            var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { 0.0 }, new double[0], signals);
            signals = null;
            layer.Response(signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForDifferentInputCoutsInResponseListOfISignal()
        {
            // Initialize
            var neuronsCount = 0u;
            IEnumerable<ISignal> signals = new List<ISignal>();
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, signals);
            var smartNeuron1 = new SmartNeuron(Guid.NewGuid(), new[] { 0.0 }, new double[0], signals);
            signals = new List<ISignal>();
            layer.Response(signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForNullInputSignalsInResponseUsingDoubleArray()
        {
            // Initialize
            var neuronsCount = 0u;
            double[] signals = new[] { 0.0 };
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, (uint)signals.Length);
            var smartNeuron1 = new SmartNeuron(0u);
            signals = null;
            layer.Response(signals);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionForDifferentInputCoutsInResponseUsingDoubleArray()
        {
            // Initialize
            var neuronsCount = 0u;
            double[] signals = new[] { 0.0 };
            var layer = new NeuralLayer<ISmartNeuron>(neuronsCount, (uint)signals.Length);
            var smartNeuron1 = new SmartNeuron(0u);
            signals = new double[0];
            layer.Response(signals);
        }
*/
        private static void AssignWeights(double weight0, double weight1, Neurons.Interfaces.INeuron oldNeuron)
        {
            oldNeuron.Weights[0] = weight0;
            oldNeuron.Weights[1] = weight1;
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
