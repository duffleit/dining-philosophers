using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DiningPhilosophers.Entities;

namespace DiningPhilosophers
{
    public class PhilosophersDinner
    {
        private readonly int _maxThinkingTime;
        private readonly int _maxEatingTime;

        public PhilosophersDinner(int maxThinkingTime, int maxEatingTime)
        {
            _maxThinkingTime = maxThinkingTime;
            _maxEatingTime = maxEatingTime;
        }

        public void Start(int numberOfPhilosophers, bool leftHanderPhilosopher)
        {

            var forks = Fork.GetListOfForks(numberOfPhilosophers).ToArray();
            var philosophers = Philosopher.GetListOfPhilosophers(numberOfPhilosophers).ToArray();

            var threadWorker = new ThreadWorker(_maxThinkingTime, _maxEatingTime);
            var threads = new List<Thread>();

            for (var i = 0; i < numberOfPhilosophers; i++)
            {
                var rightFork = forks[i];
                var leftFork = forks[(i + 1) % numberOfPhilosophers];

                if (i == 0 && leftHanderPhilosopher)
                {
                    var fork = leftFork;
                    leftFork = rightFork;
                    rightFork = fork;
                }

                var thread = new Thread(threadWorker.Eat);
                threads.Add(thread);
                thread.Start(new PhilosopherContext(philosophers[i], leftFork, rightFork));
            }

            threads.ForEach(t => t.Join());
        }
    }
}