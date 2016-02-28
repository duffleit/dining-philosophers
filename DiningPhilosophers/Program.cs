using System.Linq.Expressions;
using DiningPhilosophers.Mono.Options;
using DiningPhilosophers.Utils;

namespace DiningPhilosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPhilosophers = 2, maxThinkingTime = 1000, maxEatingTime = 1000;
            bool leftHanderPhilosopher = false;

            new OptionSet()
            {
                {"n=", v => numberOfPhilosophers = v.ParseAsIntOrSetDefaul(numberOfPhilosophers)},
                {"t1=", v => maxThinkingTime = v.ParseAsIntOrSetDefaul(maxThinkingTime)},
                {"t2=", v => maxEatingTime = v.ParseAsIntOrSetDefaul(maxEatingTime)},
                {"leftHanderPhilosopher", v =>  leftHanderPhilosopher = !string.IsNullOrEmpty(v)}
            }.Parse(args);

            var dinner = new PhilosophersDinner(maxThinkingTime, maxEatingTime);
            dinner.Start(numberOfPhilosophers, leftHanderPhilosopher);          
        }
    }
}
