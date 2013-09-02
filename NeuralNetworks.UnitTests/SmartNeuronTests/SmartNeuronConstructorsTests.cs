using System;
using System.Collections.Generic;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.SmartNeuronTests
{
    [TestFixture]
    public class SmartNeuronConstructorsTests
    {
        [Test]
        public void SmartNeuronConstructorExceptionTests()
        {
            Assert.Throws<ArgumentNullException>(delegate { new SmartNeuron(Guid.NewGuid(), null, null); }, "1");

            Assert.Throws<ArgumentNullException>(delegate { new SmartNeuron(Guid.NewGuid(), null, new List<Signal>()); }, "2");

            Assert.Throws<ArgumentNullException>(delegate { new SmartNeuron(Guid.NewGuid(), new double[] { 0.0 }, new List<Signal>()); }, "3");
        }

        [Test]
        public void ShouldCreateInstanceOfSmartNeuronEvenIfResponseTypeIsNullWithSimpleResponse()
        {
            const uint InputCount = 2u;
            var smartNeuron = new SmartNeuron(InputCount);
            Assert.IsNotNull(smartNeuron);
            Assert.IsInstanceOf<SimpleResponse>(smartNeuron.ResponseStrategy);
        }
    }
}
