using System;
using System.Globalization;

using NeuralNetwork.Helpers;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SerializationTests
{
    [TestFixture]
    public class LayerSerializationTests
    {
        [Test]
        public void ShouldSerializeAndDeserializeNetworkLayer()
        {
            const uint NeuronCount = 5u;
            const uint InputSignalCount = 5u;
            // Initialize neural network layer
            var layer = new NeuralLayer<ISmartNeuron>(NeuronCount, InputSignalCount);

            // Prepare file name for serialization
            var fileNameForSerialization = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            fileNameForSerialization = fileNameForSerialization
                .Replace('-', '_')
                .Replace(':', '_')
                .Replace('/', '_');

            // Test
            Serializer.SerializeObject(layer, fileNameForSerialization);
            var deserializedLayers = Serializer.DeserializeObject(fileNameForSerialization) as INeuralLayer<ISmartNeuron>;
            if (deserializedLayers != null)
            {
                Assert.AreEqual(layer.GetType(), deserializedLayers.GetType());
                Assert.AreEqual(layer.Count, deserializedLayers.Count);

                Assert.IsTrue(layer.Equals(deserializedLayers));
            }

            // Delete file from hard drive
            System.IO.File.Delete(fileNameForSerialization);
        }
    }
}
