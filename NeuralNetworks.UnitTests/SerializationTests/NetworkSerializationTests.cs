using System;
using System.Globalization;

using NeuralNetwork.Helpers;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.NeuralNetwork;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SerializationTests
{
    [TestFixture]
    public class NetworkSerializationTests
    {
        [Test]
        public void ShouldSerializeAndDeserializeNeuralNetwork()
        {
            const uint NeuronCount = 2u;
            const uint LayersCount = 3u;
            const uint InputSignalCount = 4u;
            // Initialize neural network
            var neuralNetwork = new SmartNeuralNetwork<INeuralLayer<ISmartNeuron>>(NeuronCount, LayersCount, InputSignalCount);

            // Prepare file name for serialization
            var fileNameForSerialization = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            fileNameForSerialization = fileNameForSerialization
                .Replace('-', '_')
                .Replace(':', '_')
                .Replace('/', '_');

            // Test
            Serializer.SerializeObject(neuralNetwork, fileNameForSerialization);
            var deserializedLayers = Serializer.DeserializeObject(fileNameForSerialization) as INeuralNetwork<INeuralLayer<ISmartNeuron>>;
            if (deserializedLayers != null)
            {
                Assert.AreEqual(neuralNetwork.GetType(), deserializedLayers.GetType());
                Assert.AreEqual(neuralNetwork.Count, deserializedLayers.Count);

                Assert.IsTrue(neuralNetwork.Equals(deserializedLayers));
            }

            // Delete file from hard drive
            System.IO.File.Delete(fileNameForSerialization);
        }
    }
}
