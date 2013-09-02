using NeuralNetworks.UnitTests.Neurons.Abstracts;

namespace NeuralNetworks.UnitTests.Neurons.Concretes
{
    public class UnipolarNeuron : NonlinearNeuron
    {
        #region Constructors

        public UnipolarNeuron(int inputCount)
            : base(inputCount)
        {
        }

        #endregion

        #region Methods (public)

        public override double ActivationFunction(double arg)
        {
            return arg > 0 ? 1 : 0;
        }

        #endregion
    }
}
