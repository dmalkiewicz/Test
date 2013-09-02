using System;
using System.Globalization;

using NeuralNetwork.Helpers;
using NeuralNetworks.Neurons.ResponseStrategies;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SerializationTests
{
    [TestFixture]
    public class ResponsesSerializationTests
    {
        private string fileNameForSerialization;

        [SetUp]
        public void SetUp()
        {
            // Prepare file name for serialization
            this.fileNameForSerialization = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            fileNameForSerialization = fileNameForSerialization
                .Replace('-', '_')
                .Replace(':', '_')
                .Replace('/', '_');
        }

        [TearDown]
        public void FinishTest()
        {
            // Delete file after serialization
            System.IO.File.Delete(this.fileNameForSerialization);
        }

        [TestCase(typeof(SimpleResponse), ExpectedResult = typeof(SimpleResponse))]
        [TestCase(typeof(SigmoidalResponse), ExpectedResult = typeof(SigmoidalResponse))]
        [TestCase(typeof(TanhResponse), ExpectedResult = typeof(TanhResponse))]
        [TestCase(typeof(UnipolarResponse), ExpectedResult = typeof(UnipolarResponse))]
        public Type ShouldSerializeAndDeserializeNeuron(Type responseType)
        {
            var response = Activator.CreateInstance(responseType) as IResponse;
            Serializer.SerializeObject(response, this.fileNameForSerialization);
            var deserializedSmartNeuron = Serializer.DeserializeObject(this.fileNameForSerialization);
            return deserializedSmartNeuron.GetType();
        }
    }
}
