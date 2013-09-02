using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

using NeuralNetwork.Helpers.MemoryGuard;

namespace NeuralNetwork.Helpers.DynamicMemoryGuard
{
    /// <summary>Checks every second if memory is going to be full and then throws an exception.</summary>
    public class DynamicMemoryGuard : IDisposable
    {
        #region Fields

        private static DynamicMemoryGuard instance;

        private readonly PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes"); 

        private Timer guard;

        #endregion

        #region Constructors

        private DynamicMemoryGuard()
        {
            const int LowMemoryLevel = 500;
            this.Initialize(LowMemoryLevel);
        }

        #endregion

        #region Events

        public delegate void EventHandler<MemoryLevelEventArgs>(object sender, MemoryLevelEventArgs e);

        public event EventHandler<MemoryLevelEventArgs> MemoryIsLow;

        #endregion

        #region Properties

        public static DynamicMemoryGuard Instance
        {
            get
            {
                return instance ?? (instance = new DynamicMemoryGuard());
            }
        }

        /// <summary>Gets the level in MBytes when MemoryIsLow <see cref="EventHandler"/> will be raised.</summary>
        public float MemoryLowLevel { get; private set; }

        /// <summary> Gets the list with memory level notifications.
        /// The <see cref="float"/> parameter specifies the number of bytes before memory will be full.
        /// The <see cref="float"/> parameter specifies when <see cref="EventHandler"/> will be raised.
        /// </summary>
        public SortedList<float, EventHandler<MemoryLevelEventArgs>> MemoryLevelNotifications { get; private set; }

        #endregion

        #region Methods (private static)

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            // free managed resources
            if (this.ramCounter != null)
            {
                this.ramCounter.Dispose();
            }

            if (this.guard != null)
            {
                this.guard.Dispose();
            }
        }

        #endregion

        #region Methods (private)

        private void Initialize(float lowMemoryLevel)
        {
            this.MemoryLowLevel = lowMemoryLevel;
            this.guard = new Timer(1000);
            this.guard.Elapsed += this.CheckingMemoryLevel;
            this.guard.Enabled = true;
            this.MemoryLevelNotifications = new SortedList<float, EventHandler<MemoryLevelEventArgs>>(new DescendedFloatComparer());
        }

        private void CheckingMemoryLevel(object sender, ElapsedEventArgs e)
        {
            var currentMemory = this.ramCounter.NextValue();
            if (currentMemory - this.MemoryLowLevel <= 0)
            {
                this.OnMemoryNotyfication(this.MemoryLowLevel);
                return;
            }

            foreach (var notificationLevel in this.MemoryLevelNotifications)
            {
                if (currentMemory - notificationLevel.Key <= 0)
                {
                    this.OnMemoryNotyfication(notificationLevel.Key, notificationLevel.Value);
                }
            }
        }

        private void OnMemoryNotyfication(float memoryLeft, EventHandler<MemoryLevelEventArgs> handler = null)
        {
            if (handler == null)
            {
                handler = this.MemoryIsLow;
            }

            if (handler != null)
            {
                handler(this, new MemoryLevelEventArgs { MemoryLevel = memoryLeft });
            }
        }

        #endregion
    }
}
