using System;
using System.Collections.Generic;
using System.Diagnostics;
using NeuralNetwork.Helpers;
using NeuralNetwork.Helpers.DynamicMemoryGuard;
using NeuralNetwork.Helpers.MemoryGuard;
using NUnit.Framework;

namespace NeuralNetworks.UnitTests.HelpersTests
{
    [TestFixture]
    // [Ignore("Long duration test")]
    public class DynamicMemoryGuardTests
    {
        private bool memoryIsLowWasRaised;

        private List<string> listToFullMemory;

        [SetUp]
        public void SetUp()
        {
            this.listToFullMemory = new List<string>();
            this.memoryIsLowWasRaised = false;
        }

        [TearDown]
        public void FinishTest()
        {
            this.listToFullMemory.Clear();
            this.listToFullMemory = null;
        }

        [TestFixtureTearDown]
        public void FinishTests()
        {
            DynamicMemoryGuard.Instance.Dispose();
        }

        [Test]
        public void ShouldBeOnlyOneInstanceOfDynamicMemoryGuard()
        {
            var firstInstance = DynamicMemoryGuard.Instance;
            var secondInstance = DynamicMemoryGuard.Instance;
            Assert.IsInstanceOf<DynamicMemoryGuard>(firstInstance);
            Assert.IsInstanceOf<DynamicMemoryGuard>(secondInstance);
            Assert.IsNotNull(firstInstance);
            Assert.IsNotNull(secondInstance);
            Assert.AreSame(firstInstance, secondInstance);
        }

        [Test]
        public void ShouldRaiseMemoryLowLevel()
        {
            // For test purposes change the memory low level
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            var customLowMemoryLevel = ramCounter.NextValue() - 10;
            DynamicMemoryGuard.Instance.GetType()
                .GetProperty(PropertyHelper.GetName<DynamicMemoryGuard>(p => p.MemoryLowLevel))
                .SetValue(DynamicMemoryGuard.Instance, customLowMemoryLevel, null);
            // Initialize
            DynamicMemoryGuard.Instance.MemoryIsLow += this.OnMemoryNotificationRaised;

            // Test
            try
            {
                while (!this.memoryIsLowWasRaised)
                {
                    this.listToFullMemory.Add(Guid.NewGuid().ToString());
                }
            }
            catch (OutOfMemoryException)
            {
                Assert.Fail("OutOfMemoryException was thrown.");
            }
        }

        [Test]
        public void ShouldRaiseCustomMemoryLevel()
        {
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            var customMemoryLevel = ramCounter.NextValue() - 5;
            DynamicMemoryGuard.Instance.MemoryLevelNotifications.Add(
                customMemoryLevel,
                delegate
                    {
                        if (this.memoryIsLowWasRaised)
                        {
                            return;
                        }

                        this.memoryIsLowWasRaised = true;
                        Assert.Pass("MemoryLowLevel was raised.");
                    });

            // Test
            try
            {
                while (!this.memoryIsLowWasRaised)
                {
                    this.listToFullMemory.Add(Guid.NewGuid().ToString());
                }
            }
            catch (OutOfMemoryException)
            {
                Assert.Fail("OutOfMemoryException was thrown.");
            }
            finally
            {
                DynamicMemoryGuard.Instance.MemoryLevelNotifications.Remove(customMemoryLevel);
            }
        }

        private void OnMemoryNotificationRaised(object sender, MemoryLevelEventArgs e)
        {
            if (!this.memoryIsLowWasRaised)
            {
                this.memoryIsLowWasRaised = true;
                Assert.Pass("MemoryLowLevel was raised.");
            }
        }
    }
}
