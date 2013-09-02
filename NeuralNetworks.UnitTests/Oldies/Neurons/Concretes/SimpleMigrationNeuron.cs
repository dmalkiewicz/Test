using NeuralNetworks.UnitTests.Neurons.Abstracts;

namespace NeuralNetworks.UnitTests.Neurons.Concretes
{
    public class SimpleMigrationNeuron : NonlinearNeuron
    {
        #region Constructors

        public SimpleMigrationNeuron(int inputCount)
            : base(inputCount)
        {
        }

        #endregion

        public override double ActivationFunction(double arg)
        {
            return arg;
        }
    }
}
