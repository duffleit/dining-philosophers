namespace DiningPhilosophers.Contexts
{
    public class ConfigContext
    {
        public int MaxThingkingTime { get; private set; }
        public int MaxEatingTime { get; private set; }
        public int RightHandedPhilosophers { get; private set; }
        public int LeftHandedPhilosophers { get; private set; }
        public int MaxMealTime { get; private set; }

        public ConfigContext SetTime(int maxEatingTime, int maxThinkingTime, int maxMealTime)
        {
            MaxEatingTime = maxEatingTime;
            MaxThingkingTime = maxThinkingTime;
            MaxMealTime = maxMealTime;
            return this;
        }

        public ConfigContext SetPhilosopherInfo(int rightHandedPhilosophers, int leftHandedPhilosophers)
        {
            RightHandedPhilosophers = rightHandedPhilosophers;
            LeftHandedPhilosophers = leftHandedPhilosophers;
            return this;
        }
    }
}