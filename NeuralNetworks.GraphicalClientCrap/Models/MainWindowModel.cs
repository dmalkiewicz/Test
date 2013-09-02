
using System.ComponentModel;
using NeuralNetwork.Helpers;
namespace NeuralNetworks.GraphicalClientCrap.Models
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        #region Fields

        private uint numberOfNeurons;

        private uint numberOfLayers;

        private uint loopIterations;

        private uint loopIterationsDone;

        private double layerResposePerformanceTestMethodTime;

        private bool useParallel;

        #endregion

        #region Events (public)

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public uint NumberOfNeurons
        {
            get
            {
                return this.numberOfNeurons;
            }

            set
            {
                if (this.numberOfNeurons == value)
                {
                    return;
                }

                this.numberOfNeurons = value;
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.NumberOfNeurons));
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.NumberOfNeuronsString));
            }
        }

        public uint NumberOfLayers
        {
            get
            {
                return this.numberOfLayers;
            }

            set
            {
                if (this.numberOfLayers == value)
                {
                    return;
                }

                this.numberOfLayers = value;
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.NumberOfLayers));
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.NumberOfLayersString));
            }
        }

        public uint LoopIterations
        {
            get
            {
                return this.loopIterations;
            }

            set
            {
                if (this.loopIterations == value)
                {
                    return;
                }

                this.loopIterations = value;
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.LoopIterations));
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.LoopIterationsString));
            }
        }

        public bool UseParallel
        {
            get
            {
                return this.useParallel;
            }

            set
            {
                if (this.useParallel == value)
                {
                    return;
                }

                this.useParallel = value;
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.UseParallel));
            }
        }

        public uint LoopIterationsDone
        {
            get
            {
                return this.loopIterationsDone;
            }

            set
            {
                if (this.loopIterationsDone == value)
                {
                    return;
                }

                this.loopIterationsDone = value;
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.LoopIterationsDone));
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.LoopIterationsDoneString));
            }
        }

        public double LayerResposePerformanceTestMethodTime
        {
            get
            {
                return this.layerResposePerformanceTestMethodTime;
            }

            set
            {
                if (this.layerResposePerformanceTestMethodTime == value)
                {
                    return;
                }

                this.layerResposePerformanceTestMethodTime = value;
                this.OnPropertyChanged(PropertyHelper.GetName<MainWindowModel>(p => p.LayerResposePerformanceTestMethodTime));
            }
        }

        #region Informators

        public string NumberOfNeuronsString
        {
            get
            {
                return string.Format("Neurons count: {0}", this.numberOfNeurons);
            }
        }

        public string NumberOfLayersString
        {
            get
            {
                return string.Format("Layers count: {0}", this.numberOfLayers);
            }
        }

        public string LoopIterationsString
        {
            get
            {
                return string.Format("Loop iterations: {0} (Each layer will respose {0})", this.loopIterations);
            }
        }

        public string LoopIterationsDoneString
        {
            get
            {
                return string.Format("Loop iterations done: {0}", this.loopIterationsDone);
            }
        }

        public override string ToString()
        {
            return string.Format("Neurons count: {0} | Loop iterations: {1}", this.numberOfNeurons, this.loopIterations);
        }

        #endregion

        #endregion

        #region Methods (private)

        private void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
