using System.Collections.Generic;

using NeuralNetworks.UnitTests.Learning.Interfaces;
using NeuralNetworks.UnitTests.Neurons.Concretes;

using ILearningElement = NeuralNetworks.Layers.ILearningElement;

namespace NeuralNetworks.UnitTests.Learning.Concrete
{
    public class LearningSet : List<ILearningElement>, ILearningSet
    {
        #region Fields
        
        /// <summary>Stores the input count.</summary>
        private int inputCount;

        /// <summary>Stores the output count.</summary>
        private int outputCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Tworzy nowy zbiór uczący.
        /// </summary>
        /// <param name="inputCount">Ilość wejść sieci neuronowej, dla której
        /// przeznaczony jest ten zbiór.</param>
        /// <param name="outputCount">Ilość wyjść sieci neuronowej, dla której
        /// przeznaczony jest ten zbiór.</param>
        public LearningSet(int inputCount, int outputCount)
        {
            this.inputCount = inputCount;
            this.outputCount = outputCount;
        }

        public LearningSet(LearningSet set)
            : base(set.Count)
        {
            this.inputCount = set.inputCount;
            this.outputCount = set.outputCount;
            foreach (LearningElement elem in set)
            {
                this.Add(new LearningElement(elem));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Ilość wejść sieci neuronowej, dla której przeznaczony jest ten zbiór.
        /// Własność ta ma jedynie charakter informacyjny i wcale nie musi
        /// odzwierciedlać stanu faktycznego, sczególnie jeśli została zmieniona.
        /// Istnieje jedynie gwarancja, że po odczytaniu zbioru z pliku własność
        /// ta będzie miała prawidłową wartość.
        /// </summary>
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

        /// <summary>
        /// Ilość wyjść sieci neuronowej, dla której przeznaczony jest ten zbiór.
        /// Własność ta ma jedynie charakter informacyjny i wcale nie musi
        /// odzwierciedlać stanu faktycznego, sczególnie jeśli została zmieniona.
        /// Istnieje jedynie gwarancja, że po odczytaniu zbioru z pliku własność
        /// ta będzie miała prawidłową wartość.
        /// </summary>
        public int OutputCount
        {
            get
            {
                return this.outputCount;
            }

            set
            {
                this.outputCount = value;
            }
        }

        #endregion

        #region Methods (public)

        public void Normalize()
        {
            foreach (var elem in this)
            {
                BasicNeuron.Normalize(elem.Inputs);
            }
        }

        #endregion
    }
}
