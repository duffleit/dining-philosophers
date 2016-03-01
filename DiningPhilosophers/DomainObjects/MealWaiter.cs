using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DiningPhilosophers.Contexts;

namespace DiningPhilosophers.DomainObjects
{
    public interface IMealWaiter
    {
        void ServeMeal(Philosopher philosopher);
        void WaitTillFinished();
        void StopDinner();
    }


    class MealWaiter : IMealWaiter
    {
        private readonly IPhilosopherStatisticContext _philosopherStatisticContext;
        private readonly ConfigContext _configContext;

        public MealWaiter(IPhilosopherStatisticContext philosopherStatisticContext, ConfigContext configContext)
        {
            _philosopherStatisticContext = philosopherStatisticContext;
            _configContext = configContext;
        }

        private readonly List<Thread> _threads = new List<Thread>();


        public void ServeMeal(Philosopher philosopher)
        {
            var thread = new Thread(ServeMealSync);
            _threads.Add(thread);
            thread.Start(philosopher);

        }

        public void ServeMealSync(object philosopher) { 

            var phil = philosopher as Philosopher;
            if (phil == null) throw new ArgumentException("philosopher is wrong type");

            while (true)
            {
                if (_philosopherStatisticContext.HasDeadlock) return;

                var radomThinkingTime = (int)(new Random().NextDouble() * _configContext.MaxThingkingTime);
                Thread.Sleep(radomThinkingTime);

                if (!phil.FirstFork.Mutex.WaitOne(1000))
                {
                    _philosopherStatisticContext.HadDeadlock(phil);
                    return;
                }

                if (!phil.SecondFork.Mutex.WaitOne(1000))
                {
                    _philosopherStatisticContext.HadDeadlock(phil);
                    return;
                }

                var randomEatingTime = (int)(new Random().NextDouble() * _configContext.MaxEatingTime);
                Thread.Sleep(randomEatingTime);

                _philosopherStatisticContext.Ate(phil);

                phil.FirstFork.Mutex.ReleaseMutex();
                phil.SecondFork.Mutex.ReleaseMutex();
            }
        }

        public void WaitTillFinished()
        {
            if (!_threads.First().Join(_configContext.MaxMealTime))
            {
                StopDinner();
            }
        }

        public void StopDinner()
        {
            foreach (var thread in _threads) thread.Abort();
        }
    }
}