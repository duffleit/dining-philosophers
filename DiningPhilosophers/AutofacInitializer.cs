using Autofac;
using DiningPhilosophers.Contexts;
using DiningPhilosophers.DomainObjects;

namespace DiningPhilosophers
{
    public class AutofacInitializer
    {
        public IContainer GetContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Application>().AsSelf();
            containerBuilder.RegisterType<Restaurant>().As<IRestaurant>();
            containerBuilder.RegisterType<PhilosopherFactory>().As<IPhilosopherFactory>();
            containerBuilder.RegisterType<ForkFactory>().As<IForkFactory>();
            containerBuilder.RegisterType<MealWaiter>().As<IMealWaiter>();
            containerBuilder.RegisterType<ThreadSafePhilosopherStatisticContext>().As<IPhilosopherStatisticContext>().SingleInstance();
            containerBuilder.RegisterType<ConfigContext>().AsSelf().SingleInstance();
            return containerBuilder.Build();
        }
    }
}