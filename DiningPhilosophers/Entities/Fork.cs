using System.Collections.Generic;
using System.Threading;

namespace DiningPhilosophers.Entities
{
    public class Fork
    {
        public int Id { get; }
        public Mutex Mutex { get; }
        public Fork(int id)
        {
            Id = id;
            Mutex = new Mutex();
        }

        public static IEnumerable<Fork> GetListOfForks(int numberOfForks)
        {
            for (var i = 0; i < numberOfForks; i++)
            {
                yield return new Fork(i);
            }
        }
    }
}