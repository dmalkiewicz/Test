using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using NeuralNetwork.Helpers.DynamicMemoryGuard;
using NeuralNetwork.Helpers.MemoryGuard;
using NeuralNetworks.GraphicalClientCrap.Models;
using NeuralNetworks.GraphicalClientCrap.ViewModelsInterfaces;
using NeuralNetworks.Layers.NeuralLayer;
using NeuralNetworks.Neurons.SmartNeuron;
using NeuralNetworks.Signals;

namespace NeuralNetworks.GraphicalClientCrap.ViewModels
{
    public class MainWindowViewModel : Window, IMainWindowViewModel
    {
        #region Fields

        private ISignal[] signals;

        private bool memoryHandled;

        private NeuralLayer<ISmartNeuron>[] layers;

        #endregion

        #region Constructors

        public MainWindowViewModel(CancellationTokenSource cancellationTokenSource)
        {
            this.CancellationTokenSource = cancellationTokenSource;
            DynamicMemoryGuard.Instance.MemoryIsLow += this.HandleMemory;
            // DynamicMemoryGuard.Instance.MemoryLevelNotifications.Add(1500, this.HandleMemory);
            this.Model = new MainWindowModel
            {
                LoopIterations = 10000,
                NumberOfNeurons = 10,
                NumberOfLayers = 7500,
                UseParallel = true
            };
        }

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowModel Model { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        #endregion

        public void LayerResposePerformanceTestMethod()
        {
            this.signals = new ISignal[this.Model.NumberOfNeurons];
            var random = new Random();
            for (var i = 0; !this.memoryHandled && i < this.signals.Length; i++)
            {
                this.signals[i] = new Signal { Value = random.NextDouble() };
            }

            var s1 = new Stopwatch();
            s1.Start();
            this.layers = new NeuralLayer<ISmartNeuron>[this.Model.NumberOfLayers];
            try
            {
                for (var i = 0; !this.memoryHandled && i < this.Model.NumberOfLayers; i++)
                {
                    this.layers[i] = new NeuralLayer<ISmartNeuron>(this.Model.NumberOfNeurons, this.signals);
                }

                if (this.Model.UseParallel)
                {
                    Parallel.For(0, this.layers.Length, i => this.ProcessSingleLayer(this.layers[i]));
                }
                else
                {
                    for (var i = 0; !this.CancellationTokenSource.Token.IsCancellationRequested && (i < this.layers.Length); i++)
                    {
                        this.ProcessSingleLayer(this.layers[i]);
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                this.HandleMemory(this, null);
            }

            s1.Stop();
            this.Model.LayerResposePerformanceTestMethodTime = this.memoryHandled ? 0 : s1.ElapsedMilliseconds / this.Model.LoopIterations;
        }

        /// <summary>Occurs when a property value changes.</summary>
        /// <param name="propertyName">Name of property which is changing.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ProcessSingleLayer(INeuralLayer<ISmartNeuron> layer)
        {
            uint tmp = 0;
            while (!this.CancellationTokenSource.Token.IsCancellationRequested && tmp++ < this.Model.LoopIterations)
            {
                layer.Response(this.signals);
            }

            this.Model.LoopIterationsDone = tmp;
        }

        private void HandleMemory(object sender, MemoryLevelEventArgs e)
        {
            this.memoryHandled = true;
            this.layers = null;
            GC.Collect();
            if (sender is DynamicMemoryGuard)
            {
                this.CancellationTokenSource.Cancel();
            }
        }
    }
}
