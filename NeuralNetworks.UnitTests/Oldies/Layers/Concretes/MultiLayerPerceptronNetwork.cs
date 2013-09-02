using System;
using System.Collections.Generic;
using System.Linq;

using NeuralNetworks.Layers;
using NeuralNetworks.UnitTests.Layers.Enums;
using NeuralNetworks.UnitTests.Layers.Interfaces;
using NeuralNetworks.UnitTests.Neurons.Abstracts;
using NeuralNetworks.UnitTests.Neurons.Concretes;

namespace NeuralNetworks.UnitTests.Layers.Concretes
{
    public class MultiLayerPerceptronNetwork : IMultiLayerPerceptronNetwork
    {
        #region Fields

        private int inputCount;

        private IList<NonlinearNeuron[]> layers;

        private bool hasBias;

        #endregion

        #region Constructors

        public MultiLayerPerceptronNetwork(int inputCount, bool hasBias, IList<NonlinearNeuron[]> layers)
            : this(inputCount, hasBias)
        {
            if (layers == null)
            {
                throw new ArgumentNullException("layers");
            }

            if (layers.Count <= 0)
            {
                throw new ArgumentException("No layers", "layers");
            }

            for (var i = 0; i < layers.Count; i++)
            {
                if (layers[i].Length <= 0)
                {
                    throw new ArgumentException(string.Format("No neurons in layer {0}", i));
                }
            }

            this.layers = layers;
        }

        public MultiLayerPerceptronNetwork(int inputCount, bool hasBias, IList<int> neuronCounts)
            : this(inputCount, hasBias)
        {
            this.layers = new List<NonlinearNeuron[]>(neuronCounts.Count);
            for (var networkLayer = 0; networkLayer < neuronCounts.Count; networkLayer++)
            {
                if (neuronCounts[networkLayer] <= 0)
                {
                    throw new ArgumentException(string.Format("No neruons in layer {0}", networkLayer));
                }

                this.layers.Add(new NonlinearNeuron[neuronCounts[networkLayer]]);
            }
        }

        public MultiLayerPerceptronNetwork(int inputCount, bool hasBias, IList<int> neuronCounts, Type neuronType)
            : this(inputCount, hasBias, neuronCounts)
        {
            var biasInputs = hasBias ? 1 : 0;
            var neuronInputs = inputCount + biasInputs;
            foreach (BasicNeuron[] layer in this.layers)
            {
                for (var networkNeuron = 0; networkNeuron < layer.Length; networkNeuron++)
                {
                    layer[networkNeuron] = Activator.CreateInstance(neuronType, neuronInputs) as BasicNeuron;
                }

                neuronInputs = layer.Length + biasInputs;
            }
        }

        private MultiLayerPerceptronNetwork(int inputCount, bool hasBias)
        {
            if (inputCount <= 0)
            {
                throw new ArgumentOutOfRangeException("inputCount");
            }

            this.inputCount = inputCount;
            this.hasBias = hasBias;
        }

        #endregion

        #region Properties

        public int InputCount
        {
            get
            {
                return this.inputCount;
            }

            set
            {
                this.inputCount = value;
            }
        }

        public int OutputCount
        {
            get
            {
                return this.layers[this.layers.Count - 1].Length;
            }
        }

        public IList<NonlinearNeuron[]> Layers
        {
            get
            {
                return this.layers;
            }

            set
            {
                this.layers = value;
            }
        }

        public bool HasBias
        {
            get
            {
                return this.hasBias;
            }

            set
            {
                if (this.hasBias == value)
                {
                    return;
                }

                if (value)
                {
                    this.AddWeight();
                }
                else
                {
                    this.RemoveWeight();
                }

                this.hasBias = value;
            }
        }

        #endregion

        #region Methods (public)

        public double[] Response(double[] inputSignals, double[][] layerResponses)
        {
            if (inputSignals == null || inputSignals.Length != this.inputCount)
            {
                throw new ArgumentException("The signal array's length must be equal to the number of inputs.");
            }

            if (layerResponses != null && layerResponses.Length != this.layers.Count)
            {
                throw new ArgumentException("The layerResponses parameter should be null or contain the same number of elements as the Layers collection.");
            }

            var signals = this.AppendBias(inputSignals, this.layers.Count == 0);
            for (var networkLayer = 0; networkLayer < this.layers.Count; networkLayer++)
            {
                var layer = this.layers[networkLayer];
                var layerResponse = new double[layer.Length];
                for (var networkNeuron = 0; networkNeuron < layer.Length; networkNeuron++)
                {
                    layerResponse[networkNeuron] = layer[networkNeuron].Response(signals);
                }

                signals = this.AppendBias(layerResponse, networkLayer == this.layers.Count - 1);
                if (layerResponses != null)
                {
                    layerResponses[networkLayer] = layerResponse;
                }
            }

            signals = this.AppendBias(signals, this.layers.Count == 0);

            return signals;
        }

        public double[] Response(double[] inputSignals)
        {
            return this.Response(inputSignals, null);
        }

        public void Randomize(Random randomGenerator, double min, double max)
        {
            foreach (var n in from BasicNeuron[] layer in this.layers from n in layer select n)
            {
                n.RandomizeWeights(randomGenerator, min, max);
            }
        }

        public void Randomize(Random randomGenerator, double min, double max, double epsilon)
        {
            foreach (var n in from BasicNeuron[] layer in this.layers from n in layer select n)
            {
                n.RandomizeWeights(randomGenerator, min, max, epsilon);
            }
        }

        public void LearnSimple(
            ILearningElement teachingElement,
            double ratio,
            ref double[] previousResponse,
            ref double[] previousError,
            LearningMethod method)
        {
            if (this.layers.Count != 1)
            {
                throw new InvalidOperationException("The simple learning algorithm can be applied only to one-layer networks.");
            }

            var layer = this.layers[0];
            if (previousResponse == null)
            {
                previousResponse = new double[layer.Length];
            }

            if (previousError == null)
            {
                previousError = new double[layer.Length];
            }

            var actualInputs = this.AppendBias(teachingElement.Inputs, false);
            for (var neuronIndex = 0; neuronIndex < layer.Length; neuronIndex++)
            {
                switch (method)
                {
                    case LearningMethod.WidrowHoff:
                        layer[neuronIndex].LearnWidrowHoff(
                            actualInputs,
                            teachingElement.ExpectedOutputs[neuronIndex],
                            ratio,
                            out previousResponse[neuronIndex],
                            out previousError[neuronIndex]);
                        break;
                    case LearningMethod.Perceptron:
                        layer[neuronIndex].Learn(
                            actualInputs,
                            teachingElement.ExpectedOutputs[neuronIndex],
                            ratio,
                            out previousResponse[neuronIndex],
                            out previousError[neuronIndex]);
                        break;
                }
            }
        }

        public void LearnSimple(
            ILearningElement teachingElement,
            double ratio,
            ref double[] previousResponse,
            ref double[] previousError)
        {
            this.LearnSimple(teachingElement, ratio, ref previousResponse, ref previousError, LearningMethod.Perceptron);
        }

        public void LearnSimpleWidrowHoff(
            ILearningElement teachingElement,
            double ratio,
            ref double[] previousResponse,
            ref double[] previousError)
        {
            this.LearnSimple(teachingElement, ratio, ref previousResponse, ref previousError, LearningMethod.WidrowHoff);
        }

        // public int AddNeuronOnEachLayer(Type neuronType)
        // {
        //    int? inputNeuronIndex = null;
        //    for (var i = 0; i < this.layers.Count; i++)
        //    {
        //        var layer = this.layers[i].ToList();
        //        var neuron = Activator.CreateInstance(neuronType, this.inputCount) as NonlinearNeuron;
        //        layer.Add(neuron);
        //        if (!inputNeuronIndex.HasValue)
        //        {
        //            inputNeuronIndex = layer.IndexOf(neuron);
        //        }
        //        this.layers[i] = layer.ToArray();
        //    }
        //    return inputNeuronIndex.HasValue ? inputNeuronIndex.Value : -1;
        // }
        // public int AddNeuronOnEachLayer(NonlinearNeuron neuron)
        // {
        //    int? inputNeuronIndex = null;
        //    for (var i = 0; i < this.layers.Count; i++)
        //    {
        //        var layer = this.layers[i].ToList();
        //        layer.Add(neuron);
        //        if (!inputNeuronIndex.HasValue)
        //        {
        //            inputNeuronIndex = layer.IndexOf(neuron);
        //        }
        //        this.layers[i] = layer.ToArray();
        //    }
        //    return inputNeuronIndex.HasValue ? inputNeuronIndex.Value : -1;
        // }
        #endregion

        #region Methods (private)

        private double[] AppendBias(double[] signals, bool lastLayer)
        {
            if (this.hasBias && !lastLayer)
            {
                var result = new double[signals.Length + 1];
                Array.Copy(signals, result, signals.Length);
                result[signals.Length] = 1;
                return result;
            }

            return signals;
        }

        private void AddWeight()
        {
            foreach (var layer in this.Layers)
            {
                foreach (var neuron in layer)
                {
                    neuron.AppendBias(true);
                }
            }
        }

        private void RemoveWeight()
        {
            foreach (var layer in this.Layers)
            {
                foreach (var neuron in layer)
                {
                    neuron.AppendBias(false);
                }
            }
        }

        #endregion
    }
}
