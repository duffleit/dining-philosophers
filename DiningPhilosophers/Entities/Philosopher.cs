using System.Collections.Generic;

namespace DiningPhilosophers.Entities
{
    public class Philosopher
    {
        public int Id { get; }
        public string Name => "Philosopher" + Id;

        public Philosopher(int id)
        {
            Id = id;
        }

        public static IEnumerable<Philosopher> GetListOfPhilosophers(int numberOfPhilosophers)
        {
            for (var i = 0; i < numberOfPhilosophers; i++)
            {
                yield return new Philosopher(i);
            }
        }
    }
}