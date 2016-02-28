using System;
using System.Threading;
using DiningPhilosophers.Entities;

namespace DiningPhilosophers
{
    public class ThreadWorker
    {
        private const int MutexWaitOneTimeout = 10000;

        private readonly int _maxThinkingTime;
        private readonly int _maxEatingTime;

        public ThreadWorker(int maxThinkingTime, int maxEatingTime)
        {
            _maxThinkingTime = maxThinkingTime;
            _maxEatingTime = maxEatingTime;
        }

        public void Eat(object philosopherContext)
        {

            var pc = philosopherContext as PhilosopherContext;
            if (pc == null) throw new ArgumentException("philosopherContext is wrong type");

            while (true)
            {
                var radomThinkingTime = (int)(new Random().NextDouble() * _maxThinkingTime);
                Thread.Sleep(radomThinkingTime);

                if(!pc.LeftFork.Mutex.WaitOne(MutexWaitOneTimeout))
                    throw new Exception("Deadlock occured");
                if(!pc.RigFork.Mutex.WaitOne(MutexWaitOneTimeout))
                    throw new Exception("Deadlock occured");

                var randomEatingTime = (int)(new Random().NextDouble() * _maxEatingTime);
                Thread.Sleep(randomEatingTime);

                Console.WriteLine(pc.Philosopher.Name + "eats");

                pc.RigFork.Mutex.ReleaseMutex();
                pc.LeftFork.Mutex.ReleaseMutex();
            }

        }
    }
}