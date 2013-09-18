namespace NeuralNetworks.Neurons.ResponseStrategies
{
    public interface IResponseStrategyFactory
    {
        IResponse CreateResponseCopy(IResponse originalResponseStrategy, INeuron neuron);
    }
}