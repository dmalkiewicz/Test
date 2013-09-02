using System;

using NeuralNetworks.UnitTests.Neurons.Abstracts;

namespace NeuralNetworks.UnitTests.Neurons.Concretes
{
    public class TanhNeuron : NonlinearNeuron
    {
        #region Constructors

        public TanhNeuron(int inputCount)
            : base(inputCount)
        {
        }

        #endregion

        #region Methods (public)

        public override double ActivationFunction(double arg)
        {
            return Math.Tanh(arg);
        }

        #endregion
    }
}
