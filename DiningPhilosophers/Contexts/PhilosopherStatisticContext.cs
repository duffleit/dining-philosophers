using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using DiningPhilosophers.DomainObjects;

namespace DiningPhilosophers.Contexts
{
    public interface IPhilosopherStatisticContext
    {
        void HadDeadlock(Philosopher philosopher);
        void Ate(Philosopher philosopher);
        bool HasDeadlock { get; }
        IReadOnlyDictionary<Philosopher, int> EatRank { get; }
        TimeSpan FirstDeadlockOccured { get; }
    }

    class ThreadSafePhilosopherStatisticContext : IPhilosopherStatisticContext
    {
        private const int Timeout = 1000;
        private readonly Mutex _deadlockMutex = new Mutex();
        private readonly Mutex _ateMutex = new Mutex();

        private readonly List<Philosopher> _deadLockedPhilosopher = new List<Philosopher>(); 
        private readonly Dictionary<Philosopher, int> _eatRank = new Dictionary<Philosopher, int>();

        public bool HasDeadlock => _deadLockedPhilosopher.Count > 0;
        public IReadOnlyDictionary<Philosopher, int> EatRank => new ReadOnlyDictionary<Philosopher, int>(_eatRank);
        public TimeSpan FirstDeadlockOccured { private set; get; }

        public void HadDeadlock(Philosopher philosopher)
        {
            _deadlockMutex.WaitOne(Timeout);
            _deadLockedPhilosopher.Add(philosopher);
            if (_deadLockedPhilosopher.Count == 1)
                FirstDeadlockOccured = DateTime.Now - Process.GetCurrentProcess().StartTime;
            _deadlockMutex.ReleaseMutex();
        }

        public void Ate(Philosopher philosopher)
        {
            _ateMutex.WaitOne(Timeout);
            if(!_eatRank.ContainsKey(philosopher)) _eatRank.Add(philosopher, 0);
            _eatRank[philosopher] = ++_eatRank[philosopher];
            _ateMutex.ReleaseMutex();
        }
    }
}