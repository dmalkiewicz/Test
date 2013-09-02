using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.NeuralNetwork
{
    public partial class SmartNeuralNetwork<TLayer> : INeuralNetwork<TLayer>
        where TLayer : INeuralLayer<ISmartNeuron>
    {
        #region Fields

        private bool hasBias;

        #endregion
        
        #region Constructors

        public SmartNeuralNetwork(uint neuronsCount, uint layersCount, IEnumerable<ISignal> signals, IResponse neuronsResponseType = default(SimpleResponse))
        {
            if (signals == null)
            {
                throw new ArgumentNullException();
            }

            var tempSignals = signals as IList<ISignal> ?? signals.ToList();
            if (!tempSignals.Any())
            {
                throw new ArgumentException();
            }

            InitializeParameters(ref neuronsResponseType);
            this.Layers = CreateLayers(neuronsCount, layersCount, neuronsResponseType, tempSignals);
            this.InitializeNeuralNetwork((uint)tempSignals.Count());
        }

        public SmartNeuralNetwork(uint neuronsCount, uint layersCount, uint inputSignalsCount, IResponse neuronsResponseType = default(SimpleResponse))
        {
            InitializeParameters(ref neuronsResponseType);
            this.Layers = CreateLayers(neuronsCount, layersCount, neuronsResponseType, inputSignalsCount);
            this.InitializeNeuralNetwork(inputSignalsCount);
        }

        #endregion

        #region Properties

        [DataMember]
        public Guid NeuralNetworkGuid { get; set; }

        [DataMember]
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
                this.IsChanged = true;
            }
        }

        [DataMember]
        public uint InputCount
        {
            get;

            private set;
        }

        [XmlIgnore]
        public uint OutputCount
        {
            get
            {
                return Convert.ToUInt32(this.Layers[0].Count);
            }
        }

        [DataMember]
        public IList<TLayer> Layers { get; private set; }

        [XmlIgnore]
        public bool IsChanged
        {
            get;
            set;
        }

        public int Count
        {
            get
            {
                return this.Layers.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.Layers.IsReadOnly;
            }
        }

        #endregion

        #region Methods (public)

        public TLayer this[int i]
        {
            get
            {
                if (i < 0 || i >= this.Layers.Count)
                {
                    return default(TLayer);
                }

                return (TLayer)this.Layers[i];
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.Layers[i] = value;
            }
        }

        public IEnumerable<double> Response(IEnumerable<ISignal> inputSignals)
        {
            var tempSignals = inputSignals as IList<ISignal> ?? inputSignals.ToList();
            if (inputSignals == null || tempSignals.Count() != this.InputCount)
            {
                throw new ArgumentException("The signal array's length must be equal to the number of inputs.");
            }

            var signals = this.AppendBias(tempSignals, this.Layers.Count == 0);
            for (var networkLayer = 0; networkLayer < this.Layers.Count; networkLayer++)
            {
                var layerResponse = this.Layers[networkLayer].Response(tempSignals);

                signals = this.AppendBias(layerResponse, networkLayer == this.Layers.Count - 1);
            }

            signals = this.AppendBias(signals, this.Layers.Count == 0);

            return signals;
        }

        public IEnumerable<double> Response(double[] inputSignals)
        {
            if (inputSignals == null || inputSignals.Length != this.InputCount)
            {
                throw new ArgumentException("The signal array's length must be equal to the number of inputs.");
            }

            var signals = this.AppendBias(inputSignals, this.Layers.Count == 0);
            for (var networkLayer = 0; networkLayer < this.Layers.Count; networkLayer++)
            {
                var layer = this.Layers[networkLayer];
                var layerResponse = new double[layer.Count];
                for (var networkNeuron = 0; networkNeuron < layer.Count; networkNeuron++)
                {
                    layerResponse[networkNeuron] = layer[networkNeuron].Response(signals);
                }

                signals = this.AppendBias(layerResponse, networkLayer == this.Layers.Count - 1);
            }

            signals = this.AppendBias(signals, this.Layers.Count == 0);

            return signals;
        }

        public IEnumerable<double> Response(double[] inputSignals, double[][] layerResponses)
        {
            if (inputSignals == null || inputSignals.Length != this.InputCount)
            {
                throw new ArgumentException("The signal array's length must be equal to the number of inputs.");
            }

            if (layerResponses != null && layerResponses.Length != this.Layers.Count)
            {
                throw new ArgumentException("The layerResponses parameter should be null or contain the same number of elements as the Layers collection.");
            }

            var signals = this.AppendBias(inputSignals, this.Layers.Count == 0);
            for (var networkLayer = 0; networkLayer < this.Layers.Count; networkLayer++)
            {
                var layer = this.Layers[networkLayer];
                var layerResponse = new double[layer.Count];
                for (var networkNeuron = 0; networkNeuron < layer.Count; networkNeuron++)
                {
                    layerResponse[networkNeuron] = layer[networkNeuron].Response(signals);
                }

                signals = this.AppendBias(layerResponse, networkLayer == this.Layers.Count - 1);
                if (layerResponses != null)
                {
                    layerResponses[networkLayer] = layerResponse;
                }
            }

            signals = this.AppendBias(signals, this.Layers.Count == 0);

            return signals;
        }

        public TLayer GetLayer(Guid layerGuid)
        {
            return this.Layers.FirstOrDefault(n => n.LayerGuid == layerGuid);
        }

        public override bool Equals(object obj)
        {
            var neuralNetwork = obj as SmartNeuralNetwork<TLayer>;
            if (neuralNetwork == null)
            {
                return false;
            }

            if (neuralNetwork.NeuralNetworkGuid != this.NeuralNetworkGuid)
            {
                return false;
            }

            if (neuralNetwork.Count != this.Layers.Count)
            {
                return false;
            }

            return !this.Layers.Where((t, i) => !t.Equals(neuralNetwork[i])).Any();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region IList<INeuralLayer<ISmartNeuron>> Members

        public int IndexOf(TLayer item)
        {
            return this.Layers.IndexOf(item);
        }

        public void Insert(int index, TLayer item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            this.Layers.Insert(index, item);
            this.IsChanged = true;
        }

        public void RemoveAt(int index)
        {
            this.Layers.RemoveAt(index);
            this.IsChanged = true;
        }

        #endregion

        #region ICollection<ISmartNeuron> Members

        public void Add(TLayer item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            this.Layers.Add(item);
            this.IsChanged = true;
        }

        public void Clear()
        {
            if (this.Layers.Count <= 0)
            {
                return;
            }

            this.Layers.Clear();
            this.IsChanged = true;
        }

        public bool Contains(TLayer item)
        {
            return this.Layers.Contains(item);
        }

        public void CopyTo(TLayer[] array, int arrayIndex)
        {
            this.Layers.CopyTo(array, arrayIndex);
        }

        public bool Remove(TLayer item)
        {
            var result = this.Layers.Remove(item);
            if (result)
            {
                this.IsChanged = true;
            }

            return result;
        }

        #endregion

        #region IEnumerable<ISmartNeuron> Members

        public IEnumerator<TLayer> GetEnumerator()
        {
            return this.Layers.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Layers.GetEnumerator();
        }

        #endregion

        #region Methods (private static)

        private static void InitializeParameters(ref IResponse neuronsResponseType)
        {
            if (neuronsResponseType == null)
            {
                neuronsResponseType = new SimpleResponse();
            }
        }

        private static IList<TLayer> CreateLayers(uint neuronsCount, uint layersCount, IResponse neuronsResponseType, IEnumerable<ISignal> signals)
        {
            var layers = new TLayer[layersCount];
            for (var j = 0; j < layersCount; j++)
            {
                layers[j] = (TLayer)(INeuralLayer<ISmartNeuron>)new NeuralLayer<ISmartNeuron>(neuronsCount, signals, neuronsResponseType);
            }

            return layers.ToList();
        }

        private static IList<TLayer> CreateLayers(uint neuronsCount, uint layersCount, IResponse neuronsResponseType, uint inputSignalsCount)
        {
            var layers = new TLayer[layersCount];
            for (var j = 0; j < layersCount; j++)
            {
                layers[j] = (TLayer)(INeuralLayer<ISmartNeuron>)new NeuralLayer<ISmartNeuron>(neuronsCount, inputSignalsCount, neuronsResponseType);
            }

            return layers.ToList();
        }

        private void InitializeNeuralNetwork(uint inputSignalsCount)
        {
            this.NeuralNetworkGuid = SequentialGuid.NewSequentialGuid();
            this.InputCount = inputSignalsCount;
            this.IsChanged = false;
        }

        #endregion

        #region Methods (private)

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

        private double[] AppendBias(IEnumerable<ISignal> signals, bool lastLayer)
        {
            if (this.hasBias && !lastLayer)
            {
                var inputSignals = signals as IList<ISignal> ?? signals.ToList();
                var result = new double[inputSignals.Count() + 1];
                Array.Copy(inputSignals.Select(s => s.Value).ToArray(), result, inputSignals.Count());
                result[inputSignals.Count()] = 1;
                return result;
            }

            return signals.Select(s => s.Value).ToArray();
        }

        private double[] AppendBias(IEnumerable<double> signals, bool lastLayer)
        {
            if (this.hasBias && !lastLayer)
            {
                var inputSignals = signals as IList<double> ?? signals.ToList();
                var result = new double[inputSignals.Count() + 1];
                Array.Copy(inputSignals.ToArray(), result, inputSignals.Count());
                result[inputSignals.Count()] = 1;
                return result;
            }

            return signals.ToArray();
        }

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

        #endregion
    }
}
