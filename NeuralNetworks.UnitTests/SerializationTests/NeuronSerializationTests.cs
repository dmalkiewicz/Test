using System;
using System.Globalization;

using NeuralNetwork.Helpers;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SerializationTests
{
    [TestFixture]
    public class NeuronSerializationTests
    {
        #region Fields

        private const uint NeuronInputCount = 2u;

        private IResponse response;

        private ISmartNeuron smartNeuron;

        private string fileNameForSerialization;

        #endregion

        #region SetUp and TearDown

        [SetUp]
        public void SetUp()
        {
            // Prepare file name for serialization
            this.fileNameForSerialization = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            fileNameForSerialization = fileNameForSerialization
                .Replace('-', '_')
                .Replace(':', '_')
                .Replace('/', '_');

            // Initialize neuron
            this.response = new SimpleResponse();
            this.smartNeuron = new SmartNeuron(NeuronInputCount, this.response);
        }

        [TearDown]
        public void FinishTest()
        {
            System.IO.File.Delete(this.fileNameForSerialization);
            this.response = null;
            this.smartNeuron = null;
        }

        #endregion

        [Test]
        public void ShouldSerializeAndDeserializeNeuron()
        {
            Serializer.SerializeObject(this.smartNeuron, this.fileNameForSerialization);
            var deserializedSmartNeuron = Serializer.DeserializeObject(this.fileNameForSerialization);
            Assert.AreEqual(this.smartNeuron.GetType(), deserializedSmartNeuron.GetType());

            var equalsResult = this.smartNeuron.Equals(deserializedSmartNeuron);
            Assert.IsTrue(equalsResult);
        }
    }
}
