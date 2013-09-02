using System.Linq;
using NeuralNetwork.Helpers.MemoryGuard;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.HelpersTests
{
    [TestFixture]
    public class DescendedFloatComparerTests
    {
        [TestCase(new float[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void ShouldSortListDescending(float[] expectedResult, params float[] numbersArray)
        {
            var listOfNumbers = numbersArray.ToList();
            listOfNumbers.Sort(new DescendedFloatComparer());
            Assert.AreEqual(numbersArray.Length, expectedResult.Length);
            Assert.AreEqual(numbersArray.Length, listOfNumbers.Count);
            Assert.AreEqual(expectedResult.Length, listOfNumbers.Count);
            for (int i = 0; i < numbersArray.Length; i++)
            {
                Assert.AreEqual(listOfNumbers[i], expectedResult[i]);
            }
        }
    }
}
