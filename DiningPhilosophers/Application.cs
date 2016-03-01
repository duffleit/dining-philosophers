using System;
using DiningPhilosophers.Contexts;
using DiningPhilosophers.DomainObjects;

namespace DiningPhilosophers
{
    public class Application
    {
        private readonly IRestaurant _restaurant;
        private readonly IPhilosopherStatisticContext _philosopherStatisticContext;
        private readonly ConfigContext _configProvider;

        public Application(IRestaurant restaurant, IPhilosopherStatisticContext philosopherStatisticContext, ConfigContext configProvider)
        {
            _restaurant = restaurant;
            _philosopherStatisticContext = philosopherStatisticContext;
            _configProvider = configProvider;
        }

        public void Start()
        {
            _restaurant.InviteRightHandPhilosophers(_configProvider.RightHandedPhilosophers);
            _restaurant.InviteLeftHandPhilosophers(_configProvider.LeftHandedPhilosophers);
            _restaurant.StartDinner();

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