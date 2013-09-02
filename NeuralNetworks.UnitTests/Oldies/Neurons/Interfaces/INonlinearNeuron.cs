namespace NeuralNetworks.UnitTests.Neurons.Interfaces
{
    /// <summary>This is an interface for non linear neuron.</summary>
    public interface INonlinearNeuron
    {
        double ActivationFunction(double arg);

        void LearnWidrowHoff(double[] signals, double expectedOutput, double ratio, out double previousResponse, out double previousError);

        double Response(double[] inputSignals);
    }
}
