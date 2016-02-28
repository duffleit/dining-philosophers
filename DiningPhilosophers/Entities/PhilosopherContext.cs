namespace DiningPhilosophers.Entities
{
    public class PhilosopherContext
    {
        public Philosopher Philosopher { get; }
        public Fork LeftFork { get; }
        public Fork RigFork { get; }

        public PhilosopherContext(Philosopher philosopher, Fork leftFork, Fork rigFork)
        {
            Philosopher = philosopher;
            LeftFork = leftFork;
            RigFork = rigFork;
        }
    }
}