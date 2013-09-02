using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NeuralNetworks.GraphicalClientCrap.ViewModels;
using NeuralNetworks.GraphicalClientCrap.ViewModelsInterfaces;

namespace NeuralNetworks.GraphicalClientCrap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IDisposable
    {
        #region Fields

        private CancellationTokenSource cts;

        private Regex reg = new Regex("[^0-9]");

        private Task task;

        #endregion

        #region Constructors

        public MainWindow()
        {
            this.cts = new CancellationTokenSource();
            this.ViewModel = new MainWindowViewModel(this.cts);
            this.DataContext = this.ViewModel;
            this.InitializeComponent();
            this.task = new Task(() => { }, this.cts.Token);
        }

        #endregion

        #region Properties

        public IMainWindowViewModel ViewModel { get; set; }

        #endregion

        #region Event handlers

        private async void OnStartPerformanceButtonClick(object sender, RoutedEventArgs e)
        {
            this.startButton.Content = "Cancel";
            this.useParallelCheckBox.IsEnabled = false;
            this.clearCounterButton.IsEnabled = false;
            if (this.task.Status == TaskStatus.Running)
            {
                this.cts.Cancel();
                try
                {

                    this.task.Wait(this.cts.Token);

                }
                catch (OperationCanceledException)
                {
                }

                this.cts = new CancellationTokenSource();
            }
            else
            {
                if (this.task.Status == TaskStatus.Canceled)
                {
                    this.cts = new CancellationTokenSource();
                }

                this.ViewModel.CancellationTokenSource = this.cts;
                this.task = Task.Factory.StartNew(this.ViewModel.LayerResposePerformanceTestMethod, this.cts.Token);
                await this.task;
            }

            this.clearCounterButton.IsEnabled = true;
            this.useParallelCheckBox.IsEnabled = true;
            this.startButton.Content = "Start performance";
        }

        private void OnClearCounterButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Model.LoopIterationsDone = 0;
        }

        #endregion

        #region Methods (private)

        private void NumericOnly(System.Object sender, TextCompositionEventArgs e)
        {
            e.Handled = this.reg.IsMatch(e.Text);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            // free managed resources
            if (this.task != null)
            {
                this.task.Dispose();
            }

            if (this.cts != null)
            {
                this.cts.Dispose();
            }
        }

        #endregion
    }
}
