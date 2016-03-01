using System.Collections.Generic;

namespace DiningPhilosophers.DomainObjects
{
    public enum Handedness { right, left }
    public class Philosopher
    {
        public int Id { get; }
        public Handedness Handedness { get; }
        public string Name => "Philosopher" + Id;
        public Fork FirstFork { get; private set; }
        public Fork SecondFork { get; private set; }

        public Philosopher(int id, Handedness handedness)
        {
            Id = id;
            Handedness = handedness;
        }

        public void GiveForks(Fork rightFork, Fork leftFork)
        {
            FirstFork = (Handedness == Handedness.left) ? leftFork : rightFork;
            SecondFork = (Handedness == Handedness.left) ? rightFork : leftFork;
        }
    }

    public interface IPhilosopherFactory
    {
        IEnumerable<Philosopher> CrateLeftHandPhilosophers(int count);
        IEnumerable<Philosopher> CrateRightHandPhilosophers(int count);
    }

    public class PhilosopherFactory : IPhilosopherFactory
    {
        private int _id;

        public IEnumerable<Philosopher> CrateLeftHandPhilosophers(int count)
        {
            for (var i = 0; i < count; i++) yield return new Philosopher(_id++, Handedness.left);
        }

        public IEnumerable<Philosopher> CrateRightHandPhilosophers(int count)
        {
            for (var i = 0; i < count; i++) yield return new Philosopher(_id++, Handedness.right);
        }
    }
}