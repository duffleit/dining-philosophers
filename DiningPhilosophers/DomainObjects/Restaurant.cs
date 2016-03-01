using System.Collections.Generic;
using System.Linq;

namespace DiningPhilosophers.DomainObjects
{
    public interface IRestaurant
    {
        void InviteLeftHandPhilosophers(int numberOfPhilosophers);
        void InviteRightHandPhilosophers(int numberOfPhilosophers);
        void StartDinner();
    }

    public class Restaurant : IRestaurant
    {
        private readonly IPhilosopherFactory _philosopherFactory;
        private readonly IForkFactory _forkFactory;
        private readonly IMealWaiter _mealWaiter;
        private readonly List<Philosopher> _philosophers;

        public Restaurant(IPhilosopherFactory philosopherFactory, IForkFactory forkFactory, IMealWaiter mealWaiter)
        {
            _philosopherFactory = philosopherFactory;
            _forkFactory = forkFactory;
            _mealWaiter = mealWaiter;
            _philosophers = new List<Philosopher>();
        }

        public void InviteLeftHandPhilosophers(int numberOfPhilosophers)
        {
            _philosophers.AddRange(_philosopherFactory.CrateLeftHandPhilosophers(numberOfPhilosophers));
        }

        public void InviteRightHandPhilosophers(int numberOfPhilosophers)
        {
            _philosophers.AddRange(_philosopherFactory.CrateRightHandPhilosophers(numberOfPhilosophers));
        }

        public void StartDinner()
        {
            var forks = _forkFactory.GetForks(_philosophers.Count).ToList();
            for (var i = 0; i < _philosophers.Count; i++)
            {
                var rightFork = forks[i];
                var leftFork = forks[(i + 1) % _philosophers.Count];
                _philosophers[i].GiveForks(rightFork, leftFork);

                _mealWaiter.ServeMeal(_philosophers[i]);
            }

            _mealWaiter.WaitTillFinished();
        }
    }
}