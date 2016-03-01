using System.Collections.Generic;
using System.Threading;

namespace DiningPhilosophers.DomainObjects
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

    public interface IForkFactory
    {
        IEnumerable<Fork> GetForks(int count);
    }

    class ForkFactory : IForkFactory
    {
        private int _id = 0;
        public IEnumerable<Fork> GetForks(int count)
        {
            for (var i = 0; i < count; i++) yield return new Fork(_id);
        }
    }
}