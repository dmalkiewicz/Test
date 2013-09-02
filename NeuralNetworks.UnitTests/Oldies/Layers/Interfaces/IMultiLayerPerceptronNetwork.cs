using System;
using System.Collections.Generic;
using NeuralNetworks.UnitTests.Layers.Enums;
using NeuralNetworks.UnitTests.Learning.Interfaces;
using NeuralNetworks.UnitTests.Neurons.Abstracts;

using ILearningElement = NeuralNetworks.Layers.ILearningElement;

namespace NeuralNetworks.UnitTests.Layers.Interfaces
{
    public interface IMultiLayerPerceptronNetwork
    {
        bool HasBias { get; set; }

        int InputCount { get; set; }

        int OutputCount { get; }

        IList<NonlinearNeuron[]> Layers { get; set; }

        void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError);

        void LearnSimple(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError, LearningMethod method);

        void LearnSimpleWidrowHoff(ILearningElement teachingElement, double ratio, ref double[] previousResponse, ref double[] previousError);

        void Randomize(Random randomGenerator, double min, double max);

        void Randomize(Random randomGenerator, double min, double max, double epsilon);

        double[] Response(double[] inputSignals);

        double[] Response(double[] inputSignals, double[][] layerResponses);
    }
}
