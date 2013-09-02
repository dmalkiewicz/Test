using System.Threading;
using NeuralNetworks.GraphicalClientCrap.Models;

namespace NeuralNetworks.GraphicalClientCrap.ViewModelsInterfaces
{
    public interface IMainWindowViewModel
    {
        MainWindowModel Model { get; set; }

        CancellationTokenSource CancellationTokenSource { get; set; }

        void LayerResposePerformanceTestMethod();
    }
}
