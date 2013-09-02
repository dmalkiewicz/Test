using System.ComponentModel;

namespace NeuralNetworks.GraphicalClientCrap.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Occurs when a property value changes.</summary>
        /// <param name="propertyName">Name of property which is changing.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
