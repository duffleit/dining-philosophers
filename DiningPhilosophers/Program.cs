using Autofac;
using DiningPhilosophers.Contexts;
using DiningPhilosophers.Mono.Options;
using DiningPhilosophers.Utils;

namespace DiningPhilosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxMealTime = 1*60*1000;
            int numberOfPhilosophers = 5, maxThinkingTime = 10, maxEatingTime = 10;
            bool containsLeftHandedPhilosopher = false;

            new OptionSet()
            {
                {"n=", v => numberOfPhilosophers = v.ParseAsIntOrSetDefaul(numberOfPhilosophers)},
                {"t1=", v => maxThinkingTime = v.ParseAsIntOrSetDefaul(maxThinkingTime)},
                {"t2=", v => maxEatingTime = v.ParseAsIntOrSetDefaul(maxEatingTime)},
                {"leftHandedPhilosopher", v =>  containsLeftHandedPhilosopher = !string.IsNullOrEmpty(v)}
            }.Parse(args);

            var rightHandedPhilosophers = containsLeftHandedPhilosopher ? numberOfPhilosophers - 1 : numberOfPhilosophers;
            var leftHandedPhilosophers = containsLeftHandedPhilosopher ? 1 : 0;

            var iocContainer = new AutofacInitializer().GetContainer();
            iocContainer.Resolve<ConfigContext>()
                .SetTime(maxEatingTime, maxThinkingTime, maxMealTime)
                .SetPhilosopherInfo(rightHandedPhilosophers, leftHandedPhilosophers);

            var application = iocContainer.Resolve<Application>();
            application.Start();
        }
    }
}
