using System;
using DiningPhilosophers.Contexts;
using DiningPhilosophers.DomainObjects;

namespace DiningPhilosophers
{
    public class Application
    {
        private readonly IPhilosophersRestaurant _philosophersRestaurant;
        private readonly IPhilosopherStatisticContext _philosopherStatisticContext;
        private readonly ConfigContext _configProvider;

        public Application(IPhilosophersRestaurant philosophersRestaurant, IPhilosopherStatisticContext philosopherStatisticContext, ConfigContext configProvider)
        {
            _philosophersRestaurant = philosophersRestaurant;
            _philosopherStatisticContext = philosopherStatisticContext;
            _configProvider = configProvider;
        }

        public void Start()
        {
            _philosophersRestaurant.InviteRightHandPhilosophers(_configProvider.RightHandedPhilosophers);
            _philosophersRestaurant.InviteLeftHandPhilosophers(_configProvider.LeftHandedPhilosophers);
            _philosophersRestaurant.StartDinner();
            _philosophersRestaurant.StopDinner();
            PrintStatistics(_philosopherStatisticContext);
        }

        private void PrintStatistics(IPhilosopherStatisticContext philosopherStatisticContext)
        {
            Console.WriteLine("*++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("*Deadlocks: " + philosopherStatisticContext.HasDeadlock);
            Console.WriteLine("*++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Ranks:");
            foreach (var philosopherStatistic in philosopherStatisticContext.EatRank)
            {
                Console.WriteLine(philosopherStatistic.Key.Name + " eat " + philosopherStatistic.Value + " times.");
            }
            Console.ReadLine();
        }
    }
}