using System;

namespace NeuralNetworks.Neurons.ResponseStrategies
{
    public class ResponseStrategyFactory
    {
        public static IResponse CreateResponseCopy(IResponse originalResponseStrategy, INeuron neuron)
        {
            var response = (IResponse)Activator.CreateInstance(originalResponseStrategy.GetType());
            response.SetNeuron(neuron);
            return response;
        }
    }
}
