using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NeuralNetworks.Neurons.ResponseStrategies;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.Layers.NeuralLayer
{
    public partial class NeuralLayer<T> : INeuralLayer<T>
        where T : ISmartNeuron
    {
        #region Fields

        private bool hasBias;

        #endregion

        #region Constructors

        public NeuralLayer(uint neuronsCount, IEnumerable<ISignal> signals, IResponse neuronsResponseType = default(SimpleResponse))
        {
            if (signals == null)
            {
                throw new ArgumentNullException();
            }

            var inputSignals = signals as IList<ISignal> ?? signals.ToList();
            if (!inputSignals.Any())
            {
                throw new ArgumentException();
            }

            InitializeParameters(ref neuronsResponseType);
            this.Layer = CreateNeurons(neuronsCount, neuronsResponseType, inputSignals);
            this.InitializeNeuralLayer((uint)inputSignals.Count());
        }

        public NeuralLayer(uint neuronsCount, uint inputSignalsCount, IResponse neuronsResponseType = default(SimpleResponse))
        {
            InitializeParameters(ref neuronsResponseType);
            this.Layer = CreateNeurons(neuronsCount, neuronsResponseType, inputSignalsCount);
            this.InitializeNeuralLayer(inputSignalsCount);
        }

        #endregion

        #region Properties

        [DataMember]
        public Guid LayerGuid { get; set; }

        [DataMember]
        public IList<T> Layer { get; set; }

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
                return Convert.ToUInt32(this.Layer.Count);
            }
        }

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
                return this.Layer.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.Layer.IsReadOnly;
            }
        }

        #endregion

        #region Methods (public)

        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= this.Layer.Count)
                {
                    return default(T);
                }

                return this.Layer[i];
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.Layer[i] = value;
            }
        }

        public IEnumerable<double> Response(double[] inputSignals)
        {
            if (inputSignals == null || (uint)inputSignals.Count() != this.InputCount)
            {
                throw new ArgumentException("The signal array's length must be equal to the number of inputs.");
            }

            var signals = this.AppendBias(inputSignals).ToArray();
            var layerResponse = new double[this.Layer.Count];
            for (var neuronNumber = 0; neuronNumber < this.Layer.Count; neuronNumber++)
            {
                // TODO: if it is not working then assign the signals to the neurons
                // TODO: COMMENTED JUST FOR PERFORMANCE TESTS: layerResponse[nNeuron] = layer[nNeuron].Response(signals);
                layerResponse[neuronNumber] = this.Layer[neuronNumber].Response(signals);
            }

            signals = this.AppendBias(layerResponse);

            return signals;
        }

        public IEnumerable<double> Response(IEnumerable<ISignal> inputSignals)
        {
            var tempSignals = inputSignals != null ? inputSignals.ToList() : null;
            if (inputSignals == null || (uint)tempSignals.Count() != this.InputCount)
            {
                throw new ArgumentException("The signal array's length must be equal to the number of inputs.");
            }

            var signals = this.AppendBias(tempSignals.Select(i => i.Value)).ToArray();
            var layer = this.Layer;
            var layerResponse = new double[layer.Count];
            for (var neuronNumber = 0; neuronNumber < layer.Count; neuronNumber++)
            {
                // TODO: if it is not working then assign the signals to the neurons
                layerResponse[neuronNumber] = layer[neuronNumber].Response(signals);
            }

            signals = this.AppendBias(layerResponse);

            return signals;
        }

        public T GetNeuron(Guid neuronGuid)
        {
            return this.Layer.FirstOrDefault(n => n.Identifier == neuronGuid);
        }

        public override bool Equals(object obj)
        {
            var layer = obj as NeuralLayer<T>;
            if (layer == null)
            {
                return false;
            }

            if (layer.LayerGuid != this.LayerGuid)
            {
                return false;
            }

            if (layer.Count != this.Layer.Count)
            {
                return false;
            }

            return !this.Layer.Where((t, i) => !t.Equals(layer[i])).Any();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region IList<ISmartNeuron> Members

        public int IndexOf(T item)
        {
            return this.Layer.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            this.Layer.Insert(index, item);
            this.IsChanged = true;
        }

        public void RemoveAt(int index)
        {
            this.Layer.RemoveAt(index);
            this.IsChanged = true;
        }

        #endregion

        #region ICollection<ISmartNeuron> Members

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            this.Layer.Add(item);
            this.IsChanged = true;
        }

        public void Clear()
        {
            if (this.Layer.Count <= 0)
            {
                return;
            }

            this.Layer.Clear();
            this.IsChanged = true;
        }

        public bool Contains(T item)
        {
            return this.Layer.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Layer.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var result = this.Layer.Remove(item);
            if (result)
            {
                this.IsChanged = true; 
            }

            return result;
        }

        #endregion

        #region IEnumerable<ISmartNeuron> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this.Layer.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Layer.GetEnumerator();
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

        private static IList<T> CreateNeurons(uint neuronsCount, IResponse neuronsResponseType, IEnumerable<ISignal> signals)
        {
            var layer = new T[neuronsCount];
            for (var i = 0; i < neuronsCount; i++)
            {
                layer[i] = (T)((ISmartNeuron)new SmartNeuron(signals, neuronsResponseType));
            }

            return layer.ToList();
        }

        private static IList<T> CreateNeurons(uint neuronsCount, IResponse neuronsResponseType, uint inputSignalsCount)
        {
            var layer = new List<T>(Convert.ToInt32(neuronsCount));
            for (var i = 0; i < neuronsCount; i++)
            {
                layer.Add((T)((ISmartNeuron)new SmartNeuron(inputSignalsCount, neuronsResponseType)));
            }

            return layer;
        }

        #endregion

        #region Methods (private)

        private void InitializeNeuralLayer(uint inputSignalsCount)
        {
            this.LayerGuid = Guid.NewGuid();
            this.InputCount = inputSignalsCount;
            this.IsChanged = false;
        }

        private IEnumerable<double> AppendBias(IEnumerable<double> inputSignals)
        {
            return this.AppendBias(inputSignals.ToArray());
        }

        private double[] AppendBias(double[] inputSignals)
        {
            if (this.HasBias)
            {
                var result = new double[inputSignals.Count() + 1];
                Array.Copy(inputSignals.ToArray(), result, inputSignals.Count());
                result[inputSignals.Count()] = 1;
                return result;
            }

            return inputSignals;
        }

        private void AddWeight()
        {
            foreach (var neuron in this.Layer)
            {
                neuron.AppendBias(true);
            }
        }

        private void RemoveWeight()
        {
            foreach (var neuron in this.Layer)
            {
                neuron.AppendBias(false);
            }
        }

        #endregion
    }
}
